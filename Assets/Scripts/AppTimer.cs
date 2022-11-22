
using System;

public class AppTimer
{
    private int _timeInSec;
    public event Action<int> updated;
    public event Action stopped;
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
                stopped?.Invoke();
            }
            updated?.Invoke(_timeInSec);
        }
    }

    public string GetString()
    {
        //if (isStopped)
        //{
        //    return "00:00:00";
        //}

        return TimeSpan.FromSeconds(_timeInSec).ToString(@"hh\:mm\:ss");
    }
}

