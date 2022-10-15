using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System;
using TMPro;
using JetBrains.Annotations;

public class Car : MonoBehaviourPunCallbacks
{
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
}
