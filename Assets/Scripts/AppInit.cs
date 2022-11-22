using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AppInit : MonoBehaviour
{
    [SerializeField] private VerticalLayoutGroup _layout;
    [SerializeField] private GameObject _buttonPrefab;
    [SerializeField] private GameObject _buttonDummyPrefab;

    [SerializeField] private GameObject _parent;
    [SerializeField] private TimerWindow _timerWindow;
    [SerializeField] private TimerStorage _timerStorage;

    private static WindowsController _windowsController;
    public static WindowsController windowsController => _windowsController;
    public static Core core;
    private float _buttonWidth;




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

        float width = _buttonPrefab.GetComponent<RectTransform>().rect.width;
        var transforms = new List<RectTransform>();

        for (int i = 0; i < _layout.transform.childCount; i++)
        {
            transforms.Add((RectTransform)_layout.transform.GetChild(i));
        }

        for (int i = 0; i < 3; i++)
        {
            CreateButton(i, transforms[i]);
        }
    }

    private void CreateButton(int i, RectTransform transform)
    {
        var gameObject = Instantiate(_buttonPrefab, _parent.transform);
        var tr = (RectTransform)gameObject.transform;
        tr.anchoredPosition = new Vector3(-0.5f * _buttonWidth * i - 0.5f * _buttonWidth, transform.anchoredPosition.y, 0);
        gameObject.GetComponent<ScreenButtonController>().SetButtonText($"Timer {i + 1}");
        LeanTween.moveX(gameObject, transform.anchoredPosition.x, 1f).setEaseOutBounce().setDelay(0.3f);
    }

}
