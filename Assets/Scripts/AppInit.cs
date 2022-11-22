using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AppInit : MonoBehaviour
{
    [SerializeField] private VerticalLayoutGroup _layout;
    [SerializeField] private GameObject _prefab;
    [SerializeField] private GameObject _parent;
    [SerializeField] private TimerWindow _timerWindow;
    [SerializeField] private TimerStorage _timerStorage;

    private static WindowsController _windowsController;
    public static WindowsController windowsController => _windowsController;

    public static Core core;

    public TimerStorage GetTimerStorage()
    {
        return _timerStorage;
    }



    private void Awake()
    {
        Application.runInBackground = true;
        _windowsController = new WindowsController(_timerWindow);
        core = new Core(_timerStorage);
    }

    private void Start()
    {
        CreateButtons();
    }

    private void CreateButtons()
    {
        LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)_layout.transform);

        float width = _prefab.GetComponent<RectTransform>().rect.width;
        var transforms = new List<RectTransform>();

        for (int i = 0; i < _layout.transform.childCount; i++)
        {
            transforms.Add((RectTransform)_layout.transform.GetChild(i));
        }

        for (int i = 0; i < 3; i++)
        {
            var gameObject = Instantiate(_prefab, _parent.transform);
            var tr = (RectTransform)gameObject.transform;
            tr.anchoredPosition = new Vector3(-0.5f * width * i - 0.5f * width, transforms[i].anchoredPosition.y, 0);
            LeanTween.moveX(gameObject, transforms[i].anchoredPosition.x, 1f).setEaseOutBounce();
        }
    }
}
