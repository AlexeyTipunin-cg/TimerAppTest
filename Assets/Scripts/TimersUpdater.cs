using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TimersUpdater : MonoBehaviour
{

    private readonly List<AppTimer> timers = new List<AppTimer>();
    private DateTime _pauseTime;
    private bool _started;

    private void Start()
    {
        _started = true;
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

    private void UpdateTimers(int elapsedSec)
    {
        foreach (var item in timers)
        {
            if (item.isStopped)
            {
                continue;
            }

            item.time-= elapsedSec;
        }
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            _pauseTime = DateTime.Now;
            //StopCoroutine(UpdateInOneSec());
            Debug.Log("App Paused");
        }
        else
        {
            if (_started)
            {
                var elapsedTime = DateTime.Now - _pauseTime;
                UpdateTimers((int)elapsedTime.TotalSeconds);
                //StartCoroutine(UpdateInOneSec());
                Debug.Log("App Started");
            }
        }
        
    }


}

