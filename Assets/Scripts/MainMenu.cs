using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class MainMenu : MonoBehaviour
{
    public static int gameMode;
    public NetworkManager NetworkManager;
    public static string selectedMap = "CyberMap";
    public void PlayNPCMode()
    {
        SceneManager.LoadScene(selectedMap);
        gameMode = 0;
        Gamesystem.gameMode = gameMode;
    }
    public void playMuliMode()
    {
        gameMode = 1;
        Gamesystem.gameMode = gameMode;
        NetworkManager.CreateRoom();
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
