using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarRPC : MonoBehaviourPunCallbacks
{
    private Car car;

    private PhotonView PV;

    private void Start()
    {
        PV = GetComponent<PhotonView>();
        car = GetComponentInChildren<Car>();
    }

    private void FixedUpdate()
    {
        if (PV.IsMine) this.photonView.RPC("MoveCar", RpcTarget.All);
    }

    private void Update()
    {
        if (PV.IsMine) this.photonView.RPC("GetWheelPosition", RpcTarget.All);
    }

    [PunRPC]
    public void GetWheelPosition()
    {
        car.GetWheelPosition();
    }

    [PunRPC]
    public void MoveCar()
    {
        car.MoveCar();
    }
}
