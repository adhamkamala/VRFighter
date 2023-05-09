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

    private float animatorSpeed = 0.2f;
    private bool timerRoundActive = false;
    private bool timerNewRoundActive = false;
    private string timerNewRoundText;
    private string timerRoundText;
    private float roundSpeed = 0f;

    // Start is called before the first frame update
    void Start()
    {
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


    }
    public void EndRoundLose()
    {
        Debug.Log("U Lost the Round...");
        padsSystem.DeactivateBothPads();
        RoundSpeedUp();
        animator.Play("TainerMovePadTrick2End");
        TimerNewRoundIntiator();
    }
    void RoundSpeedUp()
    {
        Debug.Log("Speeding Up Round...");
        roundSpeed = roundSpeed - 0.5f;
        timeRoundRemaining = 10f + roundSpeed;
        animatorSpeed = animatorSpeed + 0.5f;
        timeNewRoundRemaining = timeNewRoundRemaining + (roundSpeed * 1.2f);

    }
    IEnumerator RoundIntiatorCoroutine()
    {
        padsSystem.StartRandomizePads();
        animator.speed = animatorSpeed;
        animator.Play("TainerMovePadTrick2Start");
        while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
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
        // Subtract the time elapsed since the last frame from the time remaining
        timeRoundRemaining -= Time.deltaTime;

        // Update the timer text with the remaining time
        timerRoundText = Mathf.RoundToInt(timeRoundRemaining).ToString();
        //  Debug.Log("Time Remaining for Round: " + timerRoundText);
        roundTimeText.text = "Timer: " + timerRoundText;
        // If the time runs out, stop the timer and do something
        if (timeRoundRemaining <= 0.0f)
        {
            StopRoundTimer();
            OnRoundTimerEnd();
        }
    }

    private void StopRoundTimer()
    {
        // Stop the timer by setting the time remaining to 0
        timerRoundActive = false;
        timeRoundRemaining = 10f + roundSpeed;
    }

    private void OnRoundTimerEnd()
    {
        EndRoundLose();
    }
}
