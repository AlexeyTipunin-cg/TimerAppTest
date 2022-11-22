using UnityEngine;

public class WindowsController
{
    private TimerWindow _timerWindow;
    private CanvasGroup _canvasGroup;
    private float _fadeTime = 0.3f;

    public WindowsController(TimerWindow window)
    {
        _timerWindow = window;
        _canvasGroup = window.GetComponent<CanvasGroup>();
    }
    public void OpenTimerWindow(ScreenButtonController controller)
    {

        _canvasGroup.alpha = 0;
        _timerWindow.gameObject.SetActive(true);
        _timerWindow.Init(controller);
        LeanTween.alphaCanvas(_canvasGroup, 1, _fadeTime);

    }

    public void CloseTimerWindow()
    {
        LeanTween.alphaCanvas(_timerWindow.GetComponent<CanvasGroup>(), 0, _fadeTime).setOnComplete(CloseInternal);
    }

    private void CloseInternal()
    {
        _timerWindow.gameObject.SetActive(false);
        LeanTween.alphaCanvas(_canvasGroup, 1, _fadeTime);
    }
}

