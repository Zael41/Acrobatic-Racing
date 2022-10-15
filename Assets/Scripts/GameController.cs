using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameController : MonoBehaviourPunCallbacks
{
    public static GameController instance;

    Player[] jugadores;

    public int jugador;

    public GameObject jugadorGO;
    public GameObject cameraGO;

    private int lapCount;
    public TMP_Text lapText;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            QualitySettings.vSyncCount = 1;
            lapCount = 0;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Start()
    {
        jugadores = PhotonNetwork.PlayerList;

        jugador = jugadores.Length;

        Debug.Log("Se unio el jugador" + jugador);

        PhotonNetwork.NickName = jugador.ToString();

        Vector3 spawnPoint = new Vector3(25f, 20f, 0f);
        jugadorGO = PhotonNetwork.Instantiate("Player2", spawnPoint, Quaternion.identity, 0);
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

    public void FinishLap()
    {
        lapCount += 1;
        lapText.text = "LAP: " + lapCount + " / 3";
    }
}
