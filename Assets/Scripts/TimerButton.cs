using UnityEngine;
using UnityEngine.UI;

public class TimerButton : MonoBehaviour
{
    [SerializeField] private Button _btn;

    // Start is called before the first frame update
    void Start()
    {
        _btn.onClick.AddListener(OnButtonClick);
    }



    private void OnButtonClick()
    {
        Buttons.windowsController?.OpenTimerWindow();
    }


}
