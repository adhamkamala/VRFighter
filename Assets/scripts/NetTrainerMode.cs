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
        roundSystem.HideUI();
    }

    // Update is called once per frame
    void Update()
    {
        
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
        // AWAITS INPUT FROM USER
        // SET PADS COLOR
        // RANDOMIZE ORDER
        //LAUNCH ANIMATION
        //
        //padsSystem.setOrderPads();

        //roundSystem.StartRoundTimerOnly();
        //StartCoroutine(ModeSecondCoroutine());

        //  animationSystem.SetAnimatorSpeed(animatorSpeed); --> also input from user
        // animation is a bit complex --> should there be an order? if yes then ui should have that
    }
    //IEnumerator ModeSecondCoroutine() // not req by imp. anim.sys. class
    //{
    //    animationSystem.ClearAnimator();
    //    animationSystem.SetAnimatorSpeed(animatorSpeed);
    //    setAnimatorText();
    //    yield return StartCoroutine(animationSystem.PlayAnimationAndWaitCoroutine("XXXXXX"));
    //    XRHandController.HapticLeftSuccess();
    //    XRHandController.HapticRightSuccess();
    //    TimerRoundIntiator();
    //}



    public void HandlePadLeftAnimate(string leftPad)
    {
        animationSystem.PadsNetLeftAnimate(leftPad);
    }
    public void HandlePadRightAnimate(string rightPad)
    {
        animationSystem.PadsNetRightAnimate(rightPad);
    }
    public void HanldePadLeftColor(int leftPadColor) // 0 --> blue ; 1 --> red
    {
        padsSystem.setLeftPadColor(leftPadColor);
    }
    public void HanldePadRightColor(int rightPadColor) // 0 --> blue ; 1 --> red
    {
        padsSystem.setRightPadColor(rightPadColor);
    }
    public void HandlePadsOrder(int leftPadOrder, int rightPadOrder) // next version
    {
        padsSystem.setOrderPads();
    }
    public void HandleActivateBothPads()
    {
        padsSystem.ActivateBothPadsMode2();
    }
}
