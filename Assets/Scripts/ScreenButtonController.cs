using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ScreenButtonController : MonoBehaviour
{
    [SerializeField] private Button _btn;
    [SerializeField] private Image _image;
    [SerializeField] private TMP_Text _text;
    private AppTimer _timer;

    public AppTimer timer => _timer;
    public string getTimerString => _timer.GetString();
    public int time => _timer.time;

    public void OnUdapteAdd(Action<int> action)
    {
        _timer.onUpdate += action;
    }

    public void RemoveSubscription(Action<int> action)
    {
        _timer.onUpdate -= action;
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

    private void Stop()
    {
        _image.color = new Color(0, 1, 0.410291f, 1);
    }


    private void OnButtonClick()
    {
        _image.color = Color.white;
        AppInit.windowsController?.OpenTimerWindow(this);
    }
}
