using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject settingsMenu;
    public GameObject playerMenu;
    public GameObject levelMenu;

    // Start is called before the first frame update
    void Start()
    {
        settingsMenu.SetActive(false);
        playerMenu.SetActive(false);
        levelMenu.SetActive(false);
    }

    public void StartGame()
    {
        playerMenu.SetActive(true);
        mainMenu.SetActive(false);
        levelMenu.SetActive(false);
        settingsMenu.SetActive(false);
    }

    public void ReturnToMainMenu()
    {
        mainMenu.SetActive(true);
        levelMenu.SetActive(false);
        settingsMenu.SetActive(false);
        playerMenu.SetActive(false);
    }

    public void SettingsMenu()
    {
        mainMenu.SetActive(false);
        levelMenu.SetActive(false);
        settingsMenu.SetActive(true);
        playerMenu.SetActive(false);
    }

    public void ChoosePlayers(int players)
    {
        GameObject.Find("RoomManager").GetComponent<RoomManager>().playerCount = (byte)players;
        mainMenu.SetActive(false);
        levelMenu.SetActive(true);
        settingsMenu.SetActive(false);
        playerMenu.SetActive(false);
    }

    public void ChooseLevel(string level)
    {
        RoomManager RM = GameObject.Find("RoomManager").GetComponent<RoomManager>();
        RM.levelName = level;
        RM.CrearRoom();
    }

    public void CloseGame()
    {
        Application.Quit();
    }
}
