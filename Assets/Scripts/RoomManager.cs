using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class RoomManager : MonoBehaviourPunCallbacks
{
    public int roomNumber;
    public byte playerCount = 2;
    public string levelName = "DrivingTest";

    public void CrearRoom()
    {
        Debug.Log("Creando sala nueva");

        PhotonNetwork.JoinOrCreateRoom("Sala no." + roomNumber, new RoomOptions() { MaxPlayers = playerCount }, TypedLobby.Default);

        Debug.Log("Sala creada");
    }

    public void UnirseRoom()
    {
        PhotonNetwork.JoinRandomRoom();
        Debug.Log("Unido a una sala aleatoria");
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Fallo al crear la sala");

        CrearRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Fallo al unirse a la sala, se creara una nueva");

        CrearRoom();
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel(levelName);
    }
}
