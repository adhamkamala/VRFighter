using System.Collections;
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

    private float animatorSpeed = 0.2f;
    private bool timerRoundActive = false;
    private bool timerNewRoundActive = false;
    private string timerNewRoundText;
    private string timerRoundText;
    private float roundSpeed = 0f;
    private bool roundSpeedUp = false;
    private int animationCounter = 0;
    private int gameMode = 2;

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
        //RoundIntiatorCoroutine();
    }
    public void EndRoundWin()
    {
        Debug.Log("U Won the Round...");
        padsSystem.DeactivateBothPads();
       // animator.Play("TainerMovePadTrick2End");
        animationSystem.PlayAnimationNormal("TainerMovePadTrick2End");
        if (roundSpeedUp)
        {
            RoundSpeedUp();
        }
        TimerNewRoundIntiator();
        StopRoundTimer();
        animationCounter++;


    }
    public void EndRoundLose()
    {
        Debug.Log("U Lost the Round...");
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
        //animator.Play("TainerMovePadTrick2End");
        animationSystem.PlayAnimationNormal("TainerMovePadTrick2End");
        TimerNewRoundIntiator();
        animationCounter++;
    }
    public void EndFullRoundLose()
    {
        Debug.Log("U Lost the Full Round...");
        padsSystem.DeactivateBothPads();
        scoreSystem.ResetLife();
        ResetRoundSpeed();
        scoreSystem.ClearScore();
       // animator.Play("TainerMovePadTrick2End");
        animationSystem.PlayAnimationNormal("TainerMovePadTrick2End");
        TimerNewRoundIntiator();
        animationCounter++;
    }
    public void EndGameSystem()
    {
        padsSystem.DeactivateBothPads();
        animationSystem.ClearAnimator();
        timerRoundActive = false;
        timerNewRoundActive = false;

    }
    void RoundSpeedUp()
    {
        Debug.Log("Speeding Up Round...");
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
        padsSystem.StartRandomizePads();
        // animator.Rebind();  
        animationSystem.ClearAnimator();
        animationSystem.SetAnimatorSpeed(animatorSpeed);
       // animator.speed = animatorSpeed;
        setAnimatorText();

        //  while (!animationSystem.PlayAnimationAndWait("TainerMovePadTrick2Start"))
        //   {
        //??
        //     yield return null;
        // }
        //animator.Play("TainerMovePadTrick2Start");
        //while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f || animator.IsInTransition(0))
        //{
        //    yield return null;
        //}
        //animationSystem.PlayAnimationAndWait("TainerMovePadTrick2Start")

        yield return StartCoroutine(animationSystem.PlayAnimationAndWaitCoroutine("TainerMovePadTrick2Start"));
        XRHandController.HapticLeftSuccess();
        XRHandController.HapticRightSuccess();
        padsSystem.ActivateBothPads();
        TimerRoundIntiator();
    }
    void TimerRoundIntiator()
    {
        timerRoundActive = true;
    }
    void TimerNewRoundIntiator()
    {
        timerNewRoundActive = true;
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
       // UINetObject.SetActive(true);
    }







}
