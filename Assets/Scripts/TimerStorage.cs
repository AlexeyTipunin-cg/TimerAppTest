using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class TimerStorage : MonoBehaviour
{

    public List<AppTimer> timers = new List<AppTimer>();
    private Timer timer = new Timer(1000);

    private void Start()
    {
        StartCoroutine(UpdateInOneSec());
    }

    private IEnumerator UpdateInOneSec()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(1f);

            foreach (var item in timers)
            {
                if (item.isStopped)
                {
                    continue;
                }

                item.time -= 1;
            }
        }
    }
}

