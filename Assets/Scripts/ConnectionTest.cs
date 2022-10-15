using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class ConnectionTest : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.GameVersion = "0.1";

        PhotonNetwork.ConnectUsingSettings();
        Debug.Log("Conectando al server master");
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        Debug.Log("Conectado al server maestro");
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("Fallo al conectar");
    }
}
