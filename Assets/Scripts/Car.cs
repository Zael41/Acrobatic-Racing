using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System;
using TMPro;
using JetBrains.Annotations;
using UnityEngine.UI;

public class Car : MonoBehaviourPunCallbacks
{
    public enum items
    {
        Boost,
        Thunder,
        Missile,
        Cannon
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

    private int lapCount;
    public TMP_Text lapText;
    public bool[] checkpoints;

    public Sprite[] itemSprites;

    private void Start()
    {
        rb = GetComponentInChildren<Rigidbody>();
        rb.centerOfMass = centerOfMass.localPosition;
        Cursor.lockState = CursorLockMode.Locked;
        PV = GetComponent<PhotonView>();
        lapCount = 0;
        checkpoints = new bool[9]; 
        lapText = GameObject.Find("LapText").GetComponent<TMP_Text>();
    }

    private void FixedUpdate()
    {
        MoveCar();
    }

    public void MoveCar()
    {
        if (PV.IsMine)
        {
            wheelColliderLeftBack.motorTorque = Input.GetAxis("Vertical") * motorTorque;
            wheelColliderRightBack.motorTorque = Input.GetAxis("Vertical") * motorTorque;
            wheelColliderLeftFront.steerAngle = Input.GetAxis("Horizontal") * maxSteer;
            wheelColliderRightFront.steerAngle = Input.GetAxis("Horizontal") * maxSteer;
        }
    }

    private void Update()
    {
        GetWheelPosition();
    }

    public void GetWheelPosition()
    {
        if (PV.IsMine)
        {
            Vector3 position = Vector3.zero;
            Quaternion rotation = Quaternion.identity;

            wheelColliderLeftFront.GetWorldPose(out position, out rotation);
            wheelLeftFront.position = position;
            wheelLeftFront.rotation = rotation;

            wheelColliderRightFront.GetWorldPose(out position, out rotation);
            wheelRightFront.position = position;
            wheelRightFront.rotation = rotation;

            wheelColliderLeftBack.GetWorldPose(out position, out rotation);
            wheelLeftBack.position = position;
            wheelLeftBack.rotation = rotation;

            wheelColliderRightBack.GetWorldPose(out position, out rotation);
            wheelRightBack.position = position;
            wheelRightBack.rotation = rotation;
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
            checkpoints = new bool[9]; 
        }
    }

    public void GetRandomItem()
    {
        if (PV.IsMine)
        {
            items randItem = (items)UnityEngine.Random.Range(0, 3);
            Image image = GameObject.Find("Item").GetComponent<Image>();
            image.sprite = itemSprites[(int)randItem];
        }
    }
}
