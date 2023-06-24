using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class MainMenu : MonoBehaviour
{
    public static int gameMode;
    public NetworkManager NetworkManager;
    private static bool hasInstance = false;
    private string selectedMap = "CyberMap";

    // Start is called before the first frame update
    void Start()
    {
        //if (hasInstance)
        //{
        //    Destroy(gameObject);
        //    return;
        //}
        //hasInstance = true;
        //DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlayNPCMode()
    {
        SceneManager.LoadScene(selectedMap);
        gameMode = 0;
        Gamesystem.gameMode = gameMode;
       // Gamesystem.Instance.setGameMode(1);
       // Gamesystem.Instance.checkGameMode();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }
    public void playMuliMode()
    {
        gameMode = 1;
        Gamesystem.gameMode = gameMode;
        //NetworkManager.ConnectToServer();
        NetworkManager.CreateRoom();
       // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
   

    }
    void Awake()
    {
     //   DontDestroyOnLoad(this);
    }

    public void PlayCyberMap()
    {
        selectedMap = "CyberMap";
    }

    public void PlayClawMap()
    {
        selectedMap = "ClawMap";
    }

    public void PlayTerrainMap()
    {
        selectedMap = "TerrainMap";
    }
    public void PlayForrestMap()
    {
        selectedMap = "ForrestMap";
    }

    public int getGameMode()
    {
       return gameMode;
    }


}
