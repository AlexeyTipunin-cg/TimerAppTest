
using System;

public class AppTimer
{
    private int _timeInSec;
    public Action<int> onUpdate;
    public Action onStop;
    public bool isStopped => _timeInSec <= 0;

    public int time
    {
        get
        {
            return _timeInSec;
        }

        set
        {
            _timeInSec = value;
            if (isStopped)
            {
                onStop?.Invoke();
            }
            onUpdate?.Invoke(_timeInSec);
        }
    }

    public string GetString()
    {
        return TimeSpan.FromSeconds(_timeInSec).ToString(@"hh\:mm\:ss");
    }
}

