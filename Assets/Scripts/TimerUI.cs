using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;

public class TimerUI : MonoBehaviourPunCallbacks
{
    public TMP_Text timeCounter;

    private TimeSpan timePlaying;
    private bool timerGoing;

    private float elapsedTime;

    // Start is called before the first frame update
    void Start()
    {
        timeCounter.text = "00:00.00";
        timerGoing = false;
        //BeginTimer();
    }

    [PunRPC]
    public void BeginTimer()
    {
        timerGoing = true;
        elapsedTime = 0f;

        StartCoroutine(UpdateTimer());
    }

    public string EndTimer()
    {
        timerGoing = false;
        return timeCounter.text;
    }

    private IEnumerator UpdateTimer()
    {
        while (timerGoing)
        {
            elapsedTime += Time.deltaTime;
            timePlaying = TimeSpan.FromSeconds(elapsedTime);
            string timePlayingStr = timePlaying.ToString("mm':'ss':'ff");
            timeCounter.text = timePlayingStr;
            yield return null;
        }
    }

    public float getElapsed()
    {
        return elapsedTime;
    }
}
