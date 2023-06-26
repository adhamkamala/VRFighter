using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class RoundSystem : MonoBehaviour
{
    public PadsSystem padsSystem;
    public Animator animator;
    public float timeNewRoundRemaining = 5.0f;
    public float timeRoundRemaining = 10.0f;
    public Text roundTimeText;
    public Text newRoundTimeText;
    public Text animationSpeedText;
    public Text roundTimeTextVr;
    public Text newRoundTimeTextVr;
    public Text animationSpeedTextVr;
    public ScoreSystem scoreSystem;
    public XRHandController XRHandController;
    public AnimationSystem animationSystem;
    public GameObject UIObject;
    public GameObject UINetObject;
    public AnimationClip[] startAnimations;
    public AnimationClip[] endAnimations;

    private float animatorSpeed = 0.2f;
    private bool timerRoundActive = false;
    private bool timerNewRoundActive = false;
    private bool timerRoundActiveWasRunning = false;
    private bool timerNewRoundActiveWasRunning = false;
    private string timerNewRoundText;
    private string timerRoundText;
    private float roundSpeed = 0f;
    private bool roundSpeedUp = false;
    private string startAnimation;
    private string endAnimation;
    

    // Start is called before the first frame update
    void Start()
    {
        padsSystem.Setup();
        padsSystem.DeactivateBothPads();
    }

    // Update is called once per frame
    void Update()
    {
        if (timerNewRoundActive)
        {
            UpdateNewRoundTimer();
        }

        if (timerRoundActive)
        {
            UpdateRoundTimer();
        }
    }
    public void StartRoundTimerOnly()
    {
        TimerRoundIntiator();
    }
    public void StartRound()
    {
        StartCoroutine(RoundIntiatorCoroutine());
    }
    public void EndRoundWin()
    {
        padsSystem.DeactivateBothPads();
        animationSystem.PlayAnimationNormal(endAnimation);
        if (roundSpeedUp)
        {
            RoundSpeedUp();
        }
        TimerNewRoundIntiator();
        StopRoundTimer();


    }
    public void EndRoundLose()
    {
        padsSystem.DeactivateBothPads();
        scoreSystem.LessLife();
        if (scoreSystem.NoLifeChecker())
        { // true if 0 lives
            EndFullRoundLose();
        }
        if (roundSpeedUp)
        {
            RoundSpeedUp();
        }
        animationSystem.PlayAnimationNormal(endAnimation);
        TimerNewRoundIntiator();
    }
    private void RandomizeAnimation()
    {
        int randomCount = Random.Range(0, startAnimations.Length);
        startAnimation = startAnimations[randomCount].name;
        endAnimation = endAnimations[randomCount].name;

    }
    public void EndFullRoundLose()
    {
        padsSystem.DeactivateBothPads();
        scoreSystem.ResetLife();
        ResetRoundSpeed();
        scoreSystem.ClearScore();
        animationSystem.PlayAnimationNormal(endAnimation);
        TimerNewRoundIntiator();
    }
    public void EndGameSystem()
    {
        animationSystem.ClearAnimator();
        timerRoundActive = false;
        timerRoundActiveWasRunning = false;
        timerNewRoundActive = false;
        timerNewRoundActiveWasRunning = false;

    }
    void RoundSpeedUp()
    {
        roundSpeed = roundSpeed - 1.4f;
        timeRoundRemaining = 10f + roundSpeed;
        animatorSpeed = animatorSpeed + 0.6f;
        timeNewRoundRemaining = timeNewRoundRemaining + (roundSpeed * 0.25f);
        if (animatorSpeed >= 6.3f || timeNewRoundRemaining <=1f || timeRoundRemaining <= 1f)
        {
            ResetRoundSpeed();
        }

    }
    public void turnOnSpeedUpRound()
    {
        roundSpeedUp = true;
    }
    public void turnOffSpeedUpRound()
    {
        roundSpeedUp = false;
    }
    public void FreezeTimer()
    {
        if (timerRoundActiveWasRunning)
        {
            timerRoundActive = false;
        }

        if (timerNewRoundActiveWasRunning)
        {
            timerNewRoundActive = false;
        }
    }
    public void UnfreezeTimer()
    {
        if (timerRoundActiveWasRunning)
        {
            timerRoundActive = true;
        }
        if (timerNewRoundActiveWasRunning)
        {
            timerNewRoundActive = true;
        }
    }
    void setAnimatorText()
    {
        animationSpeedText.text = "Animation Speed: " + animatorSpeed;
        animationSpeedTextVr.text = "Animation Speed: " + animatorSpeed;
    }

    void ResetRoundSpeed()
    {
        roundSpeed = 0;
        timeRoundRemaining = 10f;
        animatorSpeed  = 0.2f;
        timeNewRoundRemaining = 5f;
    }
    IEnumerator RoundIntiatorCoroutine() // not req by imp. anim.sys. class
    {
        RandomizeAnimation();
        padsSystem.StartRandomizePads();
        animationSystem.ClearAnimator();
        animationSystem.SetAnimatorSpeed(animatorSpeed);
        setAnimatorText();
        yield return StartCoroutine(animationSystem.PlayAnimationAndWaitCoroutine(startAnimation));
        XRHandController.HapticLeftSuccess();
        XRHandController.HapticRightSuccess();
        padsSystem.ActivateBothPads();
        TimerRoundIntiator();
    }
    void TimerRoundIntiator()
    {
        timerRoundActive = true;
        timerRoundActiveWasRunning = true;
    }
    void TimerNewRoundIntiator()
    {
        timerNewRoundActive = true;
        timerNewRoundActiveWasRunning = true;
    }


    private void UpdateNewRoundTimer()
    {
        timeNewRoundRemaining -= Time.deltaTime;
        timerNewRoundText = Mathf.RoundToInt(timeNewRoundRemaining).ToString();
        newRoundTimeText.text= "New Round TImer: " + timerNewRoundText;
        newRoundTimeTextVr.text = "New Round TImer: " + timerNewRoundText;
        if (timeNewRoundRemaining <= 0.0f)
        {
            StopNewRoundTimer();
            OnNewRoundTimerEnd();
        }
    }

    private void StopNewRoundTimer()
    {
        timerNewRoundActive = false;
        timerNewRoundActiveWasRunning= false;
        timeNewRoundRemaining = 5.0f;
    }

    private void OnNewRoundTimerEnd()
    {
        StartRound();
    }

    private void UpdateRoundTimer()
    {
        timeRoundRemaining -= Time.deltaTime;
        timerRoundText = Mathf.RoundToInt(timeRoundRemaining).ToString();
        roundTimeText.text = "Timer: " + timerRoundText;
        roundTimeTextVr.text = "Timer: " + timerRoundText;
        if (timeRoundRemaining <= 0.0f)
        {
            StopRoundTimer();
            OnRoundTimerEnd();
        }
    }

    private void StopRoundTimer()
    {
        timerRoundActive = false;
        timerRoundActiveWasRunning = false;
        timeRoundRemaining = 10f + roundSpeed;
    }

    private void OnRoundTimerEnd()
    {

        EndRoundLose();
    }
    public void HideUINPC()
    {
        UIObject.SetActive(false);
    }
    public void ShowUINPC()
    {
        UIObject.SetActive(true);
    }

    public void HideUINet()
    {
        UINetObject.SetActive(false);
    }
    public void ShowUINet()
    {
        UINetObject.SetActive(true);
    }







}
