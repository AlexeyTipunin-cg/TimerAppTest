using System;

public class AppTimer
{
    private int _timeInSec;
    public DateTime finishTime;
    public Action<int> onUpdate;
    public Action onStop;
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
            _timeInSec = value;
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

