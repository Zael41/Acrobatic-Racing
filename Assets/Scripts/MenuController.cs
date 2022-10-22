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

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        playerMenu.SetActive(true);
        mainMenu.SetActive(false);
    }
}
