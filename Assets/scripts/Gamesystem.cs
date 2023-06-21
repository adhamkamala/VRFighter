using Photon.Pun.Demo.PunBasics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gamesystem : MonoBehaviour
{
    public static int gameMode;
    public NormalTrainerMode normalTrainerMode;
    public NetTrainerMode netTrainerMode;
    public TestMode testMode;
    // Start is called before the first frame update
    private MainMenu mainMenu;
    void Start()
    {
        // checkGameMode();
        // mainMenu = GameObject.Find("MainMenu").GetComponent<MainMenu>();
        gameMode = MainMenu.gameMode;
        Debug.Log(gameMode);
         checkGameMode();
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }

    public void checkGameMode()
    {
        //each lunchmode
        if (gameMode == 0) // Normal NPC trainer //NormalTrainerMode
        {
            // StartRound();
            normalTrainerMode.LunchMode();
        }
        else if (gameMode == 1) // PUN NET Trainer // NetTrainerMode
        {
            netTrainerMode.LunchMode();
          //  LaunchModeNetTrainer();
        }
        else if (gameMode == 2) // Test Mode // TestMode
        {
            testMode.LunchMode();
          //  LaunchModeTest();
        }
    }
    public void setGameMode(int i) {
        gameMode = i;
    }
    public int getGameMode()
    {
        return gameMode;
    }
    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    private void Awake()
    {
   
    }
}
