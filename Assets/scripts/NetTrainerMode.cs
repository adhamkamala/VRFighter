using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetTrainerMode : MonoBehaviour
{
    public AnimationSystem animationSystem;
    public PadsSystem padsSystem;
    public XRHandController XRHandController;
    public RoundSystem roundSystem;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(animationSystem);
        animationSystem = GameObject.Find("PadsSystem").GetComponent<AnimationSystem>();
        padsSystem = GetComponent<PadsSystem>();
        Debug.Log(animationSystem);
    }

    public void LunchMode()
    {
        // establish connection
        // launch mode
        LaunchModeNetTrainer();
    }
    // Mode 2
    private void LaunchModeNetTrainer()
    {
        roundSystem.EndGameSystem();
        roundSystem.HideUINPC();
        roundSystem.ShowUINet();
    }
    public void HandlePadLeftAnimate(string leftPad)
    {
        setAnimationSystem();
        animationSystem.PadsNetLeftAnimate(leftPad);
    }
    public void HandlePadRightAnimate(string rightPad)
    {
        setAnimationSystem();
        animationSystem.PadsNetRightAnimate(rightPad);
    }
    public void HanldePadLeftColor(int leftPadColor) // 0 --> blue ; 1 --> red
    {
        setPadsSystem();
        padsSystem.setLeftPadColor(leftPadColor);
    }
    public void HanldePadRightColor(int rightPadColor) // 0 --> blue ; 1 --> red
    {
        setPadsSystem();
        padsSystem.setRightPadColor(rightPadColor);
    }
    public void HandlePadsOrder(int leftPadOrder, int rightPadOrder) // next version
    {
        padsSystem.setOrderPads();
    }
    public void HandleActivateBothPads()
    {
        setPadsSystem();
        padsSystem.ActivateBothPadsMode2();
    }

    private void setAnimationSystem()
    {
        if (animationSystem == null)
        {
            animationSystem = GameObject.Find("PadsSystem").GetComponent<AnimationSystem>();
        }
    }
    private void setPadsSystem()
    {
        if (padsSystem == null)
        {
            padsSystem = GameObject.Find("PadsSystem").GetComponent<PadsSystem>();
        }
    }
}
