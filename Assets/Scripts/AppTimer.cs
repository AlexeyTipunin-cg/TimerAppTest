
using System;

public class AppTimer
{
    private int _time;
    public event Action<int> updated;
    public event Action stopped;
    public bool isStopped => _time <= 0;

    public int time
    {
        get
        {
            return _time;
        }

        set
        {
            _time = value;
            if (isStopped)
            {
                stopped?.Invoke();
            }
            updated?.Invoke(_time);
        }
    }

    public string GetString()
    {
        if (isStopped)
        {
            return "00:00:00";
        }
        return _time.ToString();
    }
}

