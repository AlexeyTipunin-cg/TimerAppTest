using System;
using UnityEngine;

public class AppTimer
{
    public DateTime finishTime;
    public Action<int> onUpdate;
    public Action onStop;
    private int _timeInSec;

    public bool isStopped => _timeInSec <= 0;

    public AppTimer()
    {

    }

    public AppTimer(DateTime date)
    {
        finishTime = date;
        int secInterval = (int)(date - DateTime.Now).TotalSeconds;
        _timeInSec = secInterval < 0 ? 0 : secInterval;
    }

    public int time
    {
        get
        {
            return _timeInSec;
        }

        set
        {
            _timeInSec = Mathf.Max(0, value);
            if (isStopped)
            {
                finishTime = default;
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

