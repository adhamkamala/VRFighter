using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class MainMenu : MonoBehaviour
{
    private int gameMode;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlayNPCMode()
    {
        SceneManager.LoadScene("Prototype");
        gameMode = 0;
       // Gamesystem.Instance.setGameMode(1);
       // Gamesystem.Instance.checkGameMode();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }
    public void playMuliMode()
    {
        gameMode = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    void Awake()
    {
        DontDestroyOnLoad(this);
    }
    public int getGameMode()
    {
       return gameMode;
    }


}
