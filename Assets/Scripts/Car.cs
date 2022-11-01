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

    private void Start()
    {
        wheels = GetComponentsInChildren<Wheel>();
        rb = GetComponentInChildren<Rigidbody>();
        rb.centerOfMass = centerOfMass.localPosition;
        Cursor.lockState = CursorLockMode.Locked;
        PV = GetComponent<PhotonView>();
        lapCount = 0;
        checkpoints = new bool[GameController.instance.checkpointCount[SceneManager.GetActiveScene().buildIndex - 1]];
        Debug.Log(checkpoints.Length);
        lapText = GameObject.Find("LapText").GetComponent<TMP_Text>();
    }

    private void Update()
    {
        if (PV.IsMine)
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

    /*public float ThrottleInput { get; private set; }
    public float SteerInput { get; private set; }

    void Update() 
    {
        SteerInput = Input.GetAxis("Horizontal");
        ThrottleInput = Input.GetAxis("Vertical");
    }
    */
}
