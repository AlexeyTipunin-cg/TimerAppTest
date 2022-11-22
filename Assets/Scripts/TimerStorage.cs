using System.Collections;
using System.Collections.Generic;
using System.Timers;
using System.Linq;
using UnityEngine;

public class TimerStorage : MonoBehaviour
{
    private Data data = new Data();
    public class Data
    {
        public List<AppTimer> timers = new List<AppTimer>();
    }

    private Timer timer = new Timer(1000);

    private void Start()
    {
        StartCoroutine(UpdateInOneSec());
    }

    public void AddTimer(AppTimer timer)
    {
        data.timers.Add(timer);
        SaveTimers();
    }

    public void SaveTimers()
    {
        var i = data.timers.Select(t => t.finishTime).ToList();
        SaveManager.Save(i);
    }

    private IEnumerator UpdateInOneSec()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);

            foreach (var item in data.timers)
            {
                if (item.isStopped)
                {
                    continue;
                }

                item.time--;
            }
        }
    }
}

