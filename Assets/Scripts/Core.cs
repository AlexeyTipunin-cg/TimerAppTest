public class Core
{

    private TimersUpdater _timerStorage;
    public TimersUpdater timerStorage => _timerStorage;

    public Core(TimersUpdater timerStorage)
    {
        _timerStorage = timerStorage;
    }
}
