using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System;

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

    private void Start()
    {
        //rb = GetComponent<Rigidbody>();
        rb = GetComponentInChildren<Rigidbody>();
        rb.centerOfMass = centerOfMass.localPosition;
        Cursor.lockState = CursorLockMode.Locked;
        //PV = GetComponentInParent<PhotonView>();
        PV = GetComponent<PhotonView>();
        //if (!PV.IsMine) GetComponent<Car>().enabled = false;
    }

    private void FixedUpdate()
    {
        /*wheelColliderLeftBack.motorTorque = Input.GetAxis("Vertical") * motorTorque;
        wheelColliderRightBack.motorTorque = Input.GetAxis("Vertical") * motorTorque;
        wheelColliderLeftFront.steerAngle = Input.GetAxis("Horizontal") * maxSteer;
        wheelColliderRightFront.steerAngle = Input.GetAxis("Horizontal") * maxSteer;*/
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
        /*Vector3 position = Vector3.zero;
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
        wheelRightBack.rotation = rotation;*/
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
}
