using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;
using TMPro;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class GameController : MonoBehaviourPunCallbacks
{
    public static GameController instance;

    Player[] jugadores;

    public int jugador;

    public GameObject jugadorGO;
    public GameObject cameraGO;

    private int lapCount;
    public TMP_Text lapText;

    public Transform[] spawnPositions;
    public int nextPosition;

    public int[] checkpointCount = new int[4] { 9, 8, 9, 9};

    public GameObject waitingText;
    public GameObject speedMeter;
    public GameObject itemSlot;

    //public bool finishedRace;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            QualitySettings.vSyncCount = 1;
            lapCount = 0;
            //nextPosition = 0;
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
        nextPosition = jugador - 1;
        jugadorGO = PhotonNetwork.Instantiate("Player2", spawnPositions[nextPosition].position, spawnPositions[nextPosition].rotation, 0);
        nextPosition++;
        if (jugador == PhotonNetwork.CurrentRoom.MaxPlayers)
        {            
            PhotonView timerPV = GameObject.Find("Countdown").GetComponent<PhotonView>();
            timerPV.RPC("WaitingText", RpcTarget.All);
            timerPV.RPC("BeginCountdown", RpcTarget.All);
        }
        Debug.Log("Jugadores maximos =" + PhotonNetwork.CurrentRoom.MaxPlayers);
    }
}
