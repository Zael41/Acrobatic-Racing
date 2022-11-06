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

    //private int lapCount;
    public TMP_Text lapText;

    public Transform[] spawnPositions;
    public int nextPosition;

    public int[] checkpointCount = new int[4] { 9, 8, 9, 9};

    public GameObject waitingText;
    public GameObject speedMeter;
    public GameObject itemSlot;
    public GameObject finishPanel;

    public int[] orderOfFinishes = new int[4] { 0, 0, 0, 0 };

    public Color[] playerColors;

    //public bool finishedRace;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            QualitySettings.vSyncCount = 1;
            //lapCount = 0;
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

        PhotonNetwork.NickName = jugador.ToString();

        Vector3 spawnPoint = new Vector3(25f, 20f, 0f);
        nextPosition = jugador - 1;
        jugadorGO = PhotonNetwork.Instantiate("PlayerCar", spawnPositions[nextPosition].position, spawnPositions[nextPosition].rotation, 0);
        nextPosition++;
        this.photonView.RPC("ChangeColor", RpcTarget.All, jugador, jugadorGO.GetComponent<PhotonView>().ViewID);
        if (jugador == PhotonNetwork.CurrentRoom.MaxPlayers)
        {            
            PhotonView timerPV = GameObject.Find("Countdown").GetComponent<PhotonView>();
            timerPV.RPC("WaitingText", RpcTarget.All);
            timerPV.RPC("BeginCountdown", RpcTarget.All);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    [PunRPC]
    public void ChangeFinishOrder(int playerNumber)
    {
        for (int i = 0; i < orderOfFinishes.Length; i++)
        {
            if (orderOfFinishes[i] == 0)
            {
                orderOfFinishes[i] = playerNumber;
                return;
            }
        }
    }

    [PunRPC]
    public void ChangeColor(int playerNumber, int playerID)
    {
        foreach (Player p in PhotonNetwork.PlayerList)
        {
            if (p.ActorNumber == playerNumber)
            {
                PhotonView[] views = GameObject.FindObjectsOfType<PhotonView>();
                foreach (PhotonView v in views)
                {
                    if (v.ViewID == playerID)
                    {
                        v.gameObject.GetComponentInChildren<Renderer>().material.color = playerColors[playerNumber - 1];
                    }
                }
            }
        }
    }
}
