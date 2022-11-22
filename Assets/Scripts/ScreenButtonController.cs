using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScreenButtonController : MonoBehaviour
{
    [SerializeField] private Button _btn;
    [SerializeField] private Image _image;
    [SerializeField] private TMP_Text _text;
    private AppTimer _timerData;

    public string getTimerString => _timerData.GetString();
    public int time => _timerData.time;

    private void Start()
    {
        _timerData = new AppTimer();
        AppInit.core.timerStorage.timers.Add(_timerData);
        //_timerData.updated += UpdateTimeText;
        _timerData.stopped += Stop;
        _btn.onClick.AddListener(OnButtonClick);

    }

    public void SetButtonText(string text)
    {
        _text.text = text;
    }

    public void LaunchTimer(int sec)
    {
        _timerData.time = sec;
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
