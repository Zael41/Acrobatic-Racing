using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using TMPro;
using UnityEngine;

public class Countdown : MonoBehaviourPunCallbacks
{
    public TMP_Text timeCounter;

    private bool timerGoing;

    public float elapsedTime;

    public int initialTime;

    PhotonView[] views;

    // Start is called before the first frame update
    void Start()
    {
        timeCounter.text = "";
        //timerGoing = false;
        //PhotonView timerPV = GetComponent<PhotonView>();
        //timerPV.RPC("BeginCountdown", RpcTarget.All);
    }

    [PunRPC]
    public void BeginCountdown()
    {
        timerGoing = true;
        elapsedTime = initialTime;
        StartCoroutine(UpdateTimer());
    }

    public void EndTimer()
    {
        timerGoing = false;
        timeCounter.gameObject.SetActive(false);
        views = GameObject.FindObjectsOfType<PhotonView>();
        foreach (PhotonView v in views)
        {
            if (v.gameObject.tag == ("Player"))
            {
                v.RPC("ChangeDisable", RpcTarget.All);
            }
        }
        PhotonView timerPV = GameObject.Find("Timer").GetComponent<PhotonView>();
        if (GetComponent<PhotonView>().IsMine) timerPV.RPC("BeginTimer", RpcTarget.All);
    }

    private IEnumerator UpdateTimer()
    {
        while (timerGoing)
        {
            elapsedTime -= 1 * Time.deltaTime;
            string timePlayingStr = elapsedTime.ToString("0");
            timeCounter.text = timePlayingStr;
            if (elapsedTime <= 0)
            {
                EndTimer();
            }
            yield return null;
        }
    }

    public float getElapsed()
    {
        return elapsedTime;
    }

    [PunRPC]
    public void WaitingText()
    {
        GameObject.Find("WaitingText").SetActive(false);
    }
}
