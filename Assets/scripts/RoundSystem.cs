using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Tilemaps.TilemapRenderer;
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

    private float animatorSpeed = 0.2f;
    private bool timerRoundActive = false;
    private bool timerNewRoundActive = false;
    private string timerNewRoundText;
    private string timerRoundText;
    private float roundSpeed = 0f;
    private int animationCounter = 0;

    // Start is called before the first frame update
    void Start()
    {
        setAnimatorText();
        padsSystem.Setup();
        padsSystem.DeactivateBothPads();
        StartRound();
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

    public void StartRound()
    {
        StartCoroutine(RoundIntiatorCoroutine());
    }
    public void EndRoundWin()
    {
        Debug.Log("U Won the Round...");
        padsSystem.DeactivateBothPads();
        animator.Play("TainerMovePadTrick2End");
        RoundSpeedUp();
        TimerNewRoundIntiator();
        StopRoundTimer();
        animationCounter++;


    }
    public void EndRoundLose()
    {
        Debug.Log("U Lost the Round...");
        padsSystem.DeactivateBothPads();
        RoundSpeedUp();
        animator.Play("TainerMovePadTrick2End");
        TimerNewRoundIntiator();
        animationCounter++;
    }
    void RoundSpeedUp()
    {
        Debug.Log("Speeding Up Round...");
        roundSpeed = roundSpeed - 0.5f;
        timeRoundRemaining = 10f + roundSpeed;
        animatorSpeed = animatorSpeed + 0.5f;
        timeNewRoundRemaining = timeNewRoundRemaining + (roundSpeed * 1.2f);
        setAnimatorText();
        if (animatorSpeed >= 3.5f || timeNewRoundRemaining <=2)
        {
            ResetRoundSpeed();
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
    IEnumerator RoundIntiatorCoroutine()
    {
        padsSystem.StartRandomizePads();
        animator.Rebind();  
        animator.speed = animatorSpeed;
        animator.Play("TainerMovePadTrick2Start");
        while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f || animator.IsInTransition(0))
        {
            yield return null;
        }
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
}
