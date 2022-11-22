using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AppInit : MonoBehaviour
{
    [SerializeField] private int timersOnStart = 3;
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
        var data = SaveManager.GetData();
        if (data.Count > 0)
        {
            CreateButtonsFromData(data);
        }
        else
        {
            CreateButtons();
        }
    }

    private void CreateButtonsFromData(List<DateTime> timerEndDates)
    {
        var transforms = new List<RectTransform>();
        for (int i = 0; i < timerEndDates.Count; i++)
        {
            var dummy = Instantiate(_buttonDummyPrefab, _layout);
            transforms.Add((RectTransform)dummy.transform);
        }

        LayoutRebuilder.ForceRebuildLayoutImmediate(_layout);

        _buttonWidth = _buttonPrefab.GetComponent<RectTransform>().rect.width;

        for (int i = 0; i < timerEndDates.Count; i++)
        {
            CreateButton(transforms[i], new AppTimer(timerEndDates[i]));
        }
    }

    private void CreateButtons()
    {
        var transforms = new List<RectTransform>();
        for (int i = 0; i < timersOnStart; i++)
        {
            var dummy = Instantiate(_buttonDummyPrefab, _layout);
            transforms.Add((RectTransform)dummy.transform);
        }

        LayoutRebuilder.ForceRebuildLayoutImmediate(_layout);

        _buttonWidth = _buttonPrefab.GetComponent<RectTransform>().rect.width;

        for (int i = 0; i < timersOnStart; i++)
        {
            CreateButton(transforms[i], new AppTimer());
        }
    }

    private void AddTimer()
    {
        var dummy = Instantiate(_buttonDummyPrefab, _layout);
        LayoutRebuilder.ForceRebuildLayoutImmediate(_layout);
        _parent.sizeDelta = _layout.sizeDelta;

        CreateButton((RectTransform)dummy.transform, new AppTimer());
    }

    private void CreateButton(RectTransform transform, AppTimer timer)
    {
        var gameObject = Instantiate(_buttonPrefab, _parent);
        var tr = (RectTransform)gameObject.transform;
        tr.anchoredPosition = new Vector3(-0.5f * _buttonWidth * _totalTimers - 0.5f * _buttonWidth, transform.anchoredPosition.y, 0);
        var screenButtonController = gameObject.GetComponent<ScreenButtonController>();


        screenButtonController.Init(timer, _totalTimers);
        screenButtonController.SetButtonText($"Timer {_totalTimers + 1}");
        LeanTween.moveX(gameObject, transform.anchoredPosition.x, 1f).setEaseOutBounce().setDelay(0.3f);
        _totalTimers++;
    }

}
