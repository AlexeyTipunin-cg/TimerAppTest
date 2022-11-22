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

    private ScreenButtonController _controller;
    private TimeSpan _timeToAdd;

    void Start()
    {
        _startBtn.onClick.AddListener(StartTimer);
        _addBtn.onTimeChange += AddTime;
        _decreaseBtn.onTimeChange += DecreaseTime;
    }

    public void Init(ScreenButtonController controller)
    {
        _controller = controller;
        _timerText.text = controller.getTimerString;
    }



    private void StartTimer()
    {
        _controller.LaunchTimer((int)_timeToAdd.TotalSeconds);
        AppInit.windowsController.CloseTimerWindow();
    }

    private void AddTime(float value)
    {
        TimeSpan time = GetTimeSpine(value);
        Debug.Log(_timeToAdd);

        _timeToAdd += time;
        _timeToAdd = _timeToAdd.Duration();

        _timerText.text = _timeToAdd.ToString(@"hh\:mm\:ss");
    }

    private void DecreaseTime(float value)
    {
        TimeSpan time = GetTimeSpine(value);

        _timeToAdd -= time;
        _timeToAdd = _timeToAdd.Duration();
        Debug.Log(_timeToAdd);

        _timerText.text = _timeToAdd.Duration().ToString(@"hh\:mm\:ss");
    }

    private TimeSpan GetTimeSpine(float value)
    {
        TimeSpan time;
        if (value == 0)
        {
            time = TimeSpan.FromSeconds(1);
        }
        else
        {
            time = TimeSpan.FromSeconds(Mathf.Pow(value, 1.5f));
        }

        return time;
    }

    private (int hours, int min, int sec) ConvertTimeFromSeconds(float sec)
    {
        int h = Mathf.FloorToInt(sec / 3600);
        sec -= h * 3600;
        int min = Mathf.FloorToInt(sec / 60);
        sec -= min * 60;
        return (h, min, (int)sec);
    }
}
