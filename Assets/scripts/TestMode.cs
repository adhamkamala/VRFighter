using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMode : MonoBehaviour
{
    public AnimationSystem animationSystem;
    public ScoreSystem scoreSystem;
    public XRHandController XRHandController;
    // Start is called before the first frame update
    void Start()
    {
        // animationSystem.PadsNetAnimate("TrainerMidLeft", "TrainerMidRight");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LunchMode()
    {

    }
    void LaunchModeTest()
    {
        animationSystem.ChangeWeight();
    }
}
