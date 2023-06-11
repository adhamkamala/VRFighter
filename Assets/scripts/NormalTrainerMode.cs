using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalTrainerMode : MonoBehaviour
{
    public RoundSystem roundSystem;
    // Start is called before the first frame update
    void Start()
    {
   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LunchMode()
    {
        roundSystem.ShowUI();
        roundSystem.StartRound();
    }
}
