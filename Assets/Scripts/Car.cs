using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System;
using TMPro;
using JetBrains.Annotations;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Cinemachine;
using System.Runtime.ConstrainedExecution;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using Photon.Pun.Demo.Asteroids;

public class Car : MonoBehaviourPunCallbacks
{
    public enum items
    {
        None,
        Boost,
        Thunder,
        Cannon,
        Missile
    }

    private PhotonView PV;

    public Transform centerOfMass;

    public WheelCollider wheelColliderLeftFront;
    public WheelCollider wheelColliderRightFront;
    public WheelCollider wheelColliderLeftBack;
    public WheelCollider wheelColliderRightBack;

    public Transform wheelLeftFront;
    public Transform wheelRightFront;
    public Transform wheelLeftBack;
    public Transform wheelRightBack;

    public float motorTorque = 100f;
    public float maxSteer = 20f;
    private Rigidbody rb;
    private Wheel[] wheels;

    public float ThrottleInput { get; private set; }
    public float SteerInput { get; private set; }

    private int lapCount;
    public TMP_Text lapText;
    public bool[] checkpoints;

    public Sprite[] itemSprites;

    public items currentItem;

    public bool disabled;

    //public int[] orderOfFinishes = new int[4] { 0, 0, 0, 0 };

    //public bool finishedRace;

    private void Start()
    {
        wheels = GetComponentsInChildren<Wheel>();
        rb = GetComponentInChildren<Rigidbody>();
        rb.centerOfMass = centerOfMass.localPosition;
        Cursor.lockState = CursorLockMode.Locked;
        PV = GetComponent<PhotonView>();
        lapCount = 0;
        checkpoints = new bool[GameController.instance.checkpointCount[SceneManager.GetActiveScene().buildIndex - 1]];
        lapText = GameObject.Find("LapText").GetComponent<TMP_Text>();
    }

    private void Update()
    {
        if (PV.IsMine && !disabled)
        {
            SteerInput = Input.GetAxis("Horizontal");
            ThrottleInput = Input.GetAxis("Vertical");
            foreach (Wheel w in wheels)
            {
                w.SteerAngle = SteerInput * maxSteer;
                w.Torque = ThrottleInput * motorTorque;
            }
        }
    }

    public void FinishLap()
    {
        if (PV.IsMine)
        {
            foreach (bool cp in checkpoints)
            {
                if (cp == false)
                {
                    return;
                }
            }
            lapCount += 1;
            lapText.text = "LAP: " + lapCount + " / 3";
            checkpoints = new bool[GameController.instance.checkpointCount[SceneManager.GetActiveScene().buildIndex - 1]];
            if (lapCount == 3)
            {
                ChangeDisable();
                PV.RPC("DeactivateCar", RpcTarget.All, PV.ViewID);
                CinemachineFreeLook CMFL = GetComponentInChildren<CinemachineFreeLook>();
                Transform focusPoint = GameObject.Find("EndCameraPoint").transform;
                CMFL.Follow = focusPoint;
                CMFL.LookAt = focusPoint;
                GameController.instance.waitingText.SetActive(true);
                GameController.instance.speedMeter.SetActive(false);
                GameController.instance.itemSlot.SetActive(false);
                TimerUI timerPV = GameObject.Find("Timer").GetComponent<TimerUI>();
                timerPV.EndTimer();
                GameObject.Find("GameController").GetComponent<PhotonView>().RPC("ChangeFinishOrder", RpcTarget.All, GameController.instance.jugador);
                int finishedCount = 0;
                PhotonView[] views = GameObject.FindObjectsOfType<PhotonView>();
                foreach (PhotonView v in views)
                {
                    if (v.gameObject.tag == ("Player") && v.GetComponentInChildren<Rigidbody>() == null)
                    {
                        finishedCount++;
                    }
                }
                if (finishedCount == PhotonNetwork.PlayerList.Length)
                {
                    PV.RPC("EndScreen", RpcTarget.All);
                }
            }
        }
    }

    public void GetRandomItem()
    {
        if (PV.IsMine)
        {
            currentItem = (items)UnityEngine.Random.Range(1, 4);
            Image image = GameObject.Find("Item").GetComponent<Image>();
            image.sprite = itemSprites[(int)currentItem - 1];
        }
    }

    [PunRPC]
    public void ThunderHit()
    {
        //Debug.Log("rayo");
        this.transform.GetChild(0).localScale = new Vector3(0.5f, 0.5f, 0.5f);
        motorTorque = 50f;
        rb.mass = 1000f;
        StartCoroutine(ThunderEnds());
    }

    IEnumerator ThunderEnds()
    {
        yield return new WaitForSeconds(5f);
        this.transform.GetChild(0).localScale = Vector3.one;
        motorTorque = 100f;
        rb.mass = 500f;
    }

    [PunRPC]
    public void ChangeDisable()
    {
        if (PV.IsMine)
        {
            disabled = !disabled;
        }
    }

    [PunRPC]
    public void DeactivateCar(int id)
    {
        if (PV.ViewID == id)
        {
            this.transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    [PunRPC]
    public void EndScreen()
    {
        GameController.instance.finishPanel.SetActive(true);
        GameController.instance.waitingText.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        GameObject.Find("FinishTime").GetComponent<TMP_Text>().text = GameObject.Find("Timer").GetComponent<TimerUI>().timeCounter.text;
        GameObject.Find("Timer").SetActive(false);
        GameObject.Find("LapCount").SetActive(false);
        for (int i = 0; i < GameController.instance.orderOfFinishes.Length; i++)
        {
            if (GameController.instance.orderOfFinishes[i] == GameController.instance.jugador)
            {
                GameObject.Find("FinishPlacement").GetComponent<TMP_Text>().text = (i+1).ToString();
                return;
            }
        }
        //Application.Quit();
    }
}
