using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class TimerWindow : MonoBehaviour
{
    [SerializeField] private ChangeButton _addBtn;
    [SerializeField] private ChangeButton _decreaseBtn;
    [SerializeField] private Button _startBtn;
    [SerializeField] private TMP_Text _timerText;
    [SerializeField] private TMP_Text _startButtonText;

    private TimerController _controller;
    private int _timeToAdd;

    void Start()
    {
        _startBtn.onClick.AddListener(StartTimer);
        _addBtn.onTimeChange += AddTime;
        _decreaseBtn.onTimeChange += DecreaseTime;
    }

    private void OnDestroy()
    {
        _addBtn.onTimeChange -= AddTime;
        _decreaseBtn.onTimeChange -= DecreaseTime;

        if (_controller != null && _controller.timer != null)
        {
            _controller.timer.onUpdate -= UpdateText;
            _controller.timer.onStop -= OnTimerStop;
        }
    }

    private void OnDisable()
    {
        if (_controller != null && _controller.timer != null)
        {
            _controller.timer.onUpdate -= UpdateText;
            _controller.timer.onStop -= OnTimerStop;
        }
    }

    public void Init(TimerController controller)
    {
        _controller = controller;
        _timerText.text = controller.getTimerString;
        _timeToAdd = controller.time;
        _controller.timer.onUpdate += UpdateText;
        _controller.timer.onStop += OnTimerStop;

        if (!_controller.timer.isStopped)
        {
            _addBtn.gameObject.SetActive(false);
            _decreaseBtn.gameObject.SetActive(false);
            _startButtonText.text = "Close";
        }
        else
        {
            _addBtn.gameObject.SetActive(true);
            _decreaseBtn.gameObject.SetActive(true);
            _startButtonText.text = "Start";
        }
    }

    private void OnTimerStop()
    {
        _addBtn.gameObject.SetActive(true);
        _decreaseBtn.gameObject.SetActive(true);
    }

    private void UpdateText(int value)
    {
        _timerText.text = _controller.getTimerString;
    }

    private void StartTimer()
    {
        if (_controller.timer.isStopped && _timeToAdd != 0)
        {
            _controller.LaunchTimer(_timeToAdd);
        }

        AppInit.windowsController.CloseTimerWindow();
    }

    private void AddTime(float value)
    {
        int time = _timeToAdd + GetTimeSpine(value);
        SetTime(time);
    }

    private void DecreaseTime(float value)
    {
        int time = _timeToAdd - GetTimeSpine(value);
        SetTime(time);
    }

    private void SetTime(int value)
    {
        _timeToAdd = Mathf.Clamp(value, 0, 24 * 3600);
        var ts = TimeSpan.FromSeconds(_timeToAdd);
        if (ts.Days == 1)
        {
            _timerText.text = "24:00:00";
        }
        else
        {
            _timerText.text = ts.ToString(@"hh\:mm\:ss");
        }
    }


    private int GetTimeSpine(float value)
    {
        int time;
        if (value == 0)
        {
            time = 1;
        }
        else
        {
            time = (int)Mathf.Pow(10, value);
        }

        return time;
    }
}
