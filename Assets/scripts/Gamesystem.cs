using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Gamesystem : MonoBehaviour
{
    public int gameMode = 2;
    public NormalTrainerMode normalTrainerMode;
    public NetTrainerMode netTrainerMode;
    public TestMode testMode;
    // Start is called before the first frame update
    void Start()
    {
        checkGameMode();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void checkGameMode()
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
}
