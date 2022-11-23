using System.Collections;
using System.Collections.Generic;
using System.Timers;
using System.Linq;
using UnityEngine;
using System;

public class TimerStorage : MonoBehaviour
{

    private readonly List<AppTimer> timers = new List<AppTimer>();

    private void Start()
    {
        StartCoroutine(UpdateInOneSec());
    }

    public void AddTimer(AppTimer timer)
    {
        timers.Add(timer);
        SaveTimers();
    }

    public void SaveTimers()
    {
        var timerDates = timers.Select(t => t.finishTime).ToList();
        SaveManager.Save(timerDates);
    }

    private IEnumerator UpdateInOneSec()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);

            foreach (var item in timers)
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

