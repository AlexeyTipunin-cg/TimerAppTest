public class Core
{

    private TimerStorage _timerStorage;
    public TimerStorage timerStorage => _timerStorage;

    public Core(TimerStorage timerStorage)
    {
        _timerStorage = timerStorage;
    }
}
