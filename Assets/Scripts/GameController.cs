using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameController : MonoBehaviourPunCallbacks
{
    Player[] jugadores;

    public int jugador;

    public GameObject jugadorGO;
    public GameObject cameraGO;

    public void Start()
    {
        jugadores = PhotonNetwork.PlayerList;

        jugador = jugadores.Length;

        Debug.Log("Se unio el jugador" + jugador);

        PhotonNetwork.NickName = jugador.ToString();

        Vector3 spawnPoint = new Vector3(25f, 20f, 0f);
        jugadorGO = PhotonNetwork.Instantiate("Player2", spawnPoint, Quaternion.identity, 0);
        /*cameraGO = PhotonNetwork.Instantiate("CameraPrefab", spawnPoint, Quaternion.identity, 0);
        GameObject taxi = jugadorGO.transform.Find("Taxi").gameObject;
        Transform focusPoint = taxi.transform.Find("CameraFocus");
        CinemachineFreeLook CMFL = cameraGO.GetComponentInChildren<CinemachineFreeLook>();

        CMFL.Follow = focusPoint;
        CMFL.LookAt = focusPoint;*/
    }

    /*public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        jugadores = PhotonNetwork.PlayerList;

        jugador = jugadores.Length;

        Debug.Log("Se unio el jugador" + jugador);

        PhotonNetwork.NickName = jugador.ToString();
    }

    public override void OnJoinedRoom()
    {
        jugadores = PhotonNetwork.PlayerList;

        jugador = jugadores.Length;

        Debug.Log("Se unio el jugador" + jugador);

        PhotonNetwork.NickName = jugador.ToString();

        jugadorGO = PhotonNetwork.Instantiate("Player1", transform.position, Quaternion.identity, 0);
    }*/
}
