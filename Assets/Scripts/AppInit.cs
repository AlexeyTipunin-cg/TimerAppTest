using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AppInit : MonoBehaviour
{
    [SerializeField] private RectTransform _layout;
    [SerializeField] private GameObject _buttonPrefab;
    [SerializeField] private GameObject _buttonDummyPrefab;

    [SerializeField] private RectTransform _parent;
    [SerializeField] private TimerWindow _timerWindow;
    [SerializeField] private TimerStorage _timerStorage;

    [SerializeField] private Button _addTimerButton;

    private static WindowsController _windowsController;
    public static WindowsController windowsController => _windowsController;
    public static Core core;
    private float _buttonWidth;

    private int _totalTimers;


    private void Awake()
    {
        Application.runInBackground = true;
        _windowsController = new WindowsController(_timerWindow);
        core = new Core(_timerStorage);
    }

    private void Start()
    {
        _addTimerButton.onClick.AddListener(AddTimer);
        CreateButtons();
    }

    private void CreateButtons()
    {
        LayoutRebuilder.ForceRebuildLayoutImmediate(_layout);

        _buttonWidth = _buttonPrefab.GetComponent<RectTransform>().rect.width;
        var transforms = new List<RectTransform>();

        for (int i = 0; i < _layout.childCount; i++)
        {
            transforms.Add((RectTransform)_layout.GetChild(i));
        }

        for (int i = 0; i < 3; i++)
        {
            CreateButton(transforms[i]);
        }
    }

    private void AddTimer()
    {
        var dummy = Instantiate(_buttonDummyPrefab, _layout);
        LayoutRebuilder.ForceRebuildLayoutImmediate(_layout);
        _parent.sizeDelta = _layout.sizeDelta;

        CreateButton((RectTransform)dummy.transform);
    }

    private void CreateButton(RectTransform transform)
    {
        var gameObject = Instantiate(_buttonPrefab, _parent);
        var tr = (RectTransform)gameObject.transform;
        tr.anchoredPosition = new Vector3(-0.5f * _buttonWidth * _totalTimers - 0.5f * _buttonWidth, transform.anchoredPosition.y, 0);
        gameObject.GetComponent<ScreenButtonController>().SetButtonText($"Timer {_totalTimers + 1}");
        LeanTween.moveX(gameObject, transform.anchoredPosition.x, 1f).setEaseOutBounce().setDelay(0.3f);
        _totalTimers++;
    }

}
