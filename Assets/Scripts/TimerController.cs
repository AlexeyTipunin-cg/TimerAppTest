using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class TimerController : MonoBehaviour
{
    [SerializeField] private Button _btn;
    [SerializeField] private Image _image;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Color _onFinishColor = new Color(0, 1, 0.410291f, 1);
    private Color white = Color.white;
    private AppTimer _timer;


    public AppTimer timer => _timer;
    public string getTimerString => _timer.GetString();
    public int time => _timer.time;

    private void OnDestroy()
    {
        _timer.onStop -= Stop;
    }

    public void Init(AppTimer timer)
    {
        _timer = timer;
        AppInit.core.timerStorage.AddTimer(_timer);
        _timer.onStop += Stop;
        _btn.onClick.AddListener(OnButtonClick);
    }

    public void SetButtonText(string text)
    {
        _text.text = text;
    }

    public void LaunchTimer(int sec)
    {
        _timer.finishTime = DateTime.Now + TimeSpan.FromSeconds(sec);
        _timer.time = sec;
        AppInit.core.timerStorage.SaveTimers();
    }

    public void OnStartButtonPress()
    {
        _image.color = white;
    }

    private void Stop()
    {
        _image.color = _onFinishColor;
    }


    private void OnButtonClick()
    {
        _image.color = white;
        AppInit.windowsController?.OpenTimerWindow(this);
    }
}
