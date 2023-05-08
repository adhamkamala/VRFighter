using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;


public class PadsSystem : MonoBehaviour
{
    public GameObject leftHandPad;
    public GameObject rightHandPad;

    public Material leftHandMaterial;
    public Material rightHandMaterial;
    public Material PadsDeactivateMaterial;

    public TextMeshPro leftHandText;
    public TextMeshPro rightHandText;

    public GameObject[] handIndicators = new GameObject[2];
    public TextMeshPro[] handTexts = new TextMeshPro[2];
    public Material[] handMaterials = new Material[2];
    public Animator animator;
    public Text RoundTimeText;
   

    //private GameObject[] padsArray;
    //private Material[] materialArray;
    //private TextMeshPro[] textArray;
    // private string[] textArray = new string[] { "1", "2", "X" };
    private string[] textArray1 = new string[] { "1", "2"};
    private string[] textArray2 = new string[] { "1", "X" };
    private string startOrder;
    private string finishOrder;
    private string currentOrder;
    private string currentArrOrder;
    private string[] textArraySelected;
    private Renderer LeftHandPad;
    private Renderer RightHandPad;
    private ScoreSystem scoreSystem;
    public XRHandController XRHandController;
    private Light leftHandLight;
    private Light rightHandLight;
    private int blinkCount = 0;
    public int numBlinks = 2;
    public float timeRemaining = 5.0f;
    public string timerText;
    private bool timerActive = false;
    private bool previousOrderDone = false;
    private float roundSpeed = 0f;
    private float animatorSpeed = 0.2f;

    public float timeRoundRemaining = 10.0f;
    public string timerRoundText;
    private bool timerRoundActive = false;
    public enum HandType
    {
        LeftHand,
        RightHand,
        None
    }
    private HandType currentHandType;
    public enum PadType
    {
        LeftPad,
        RightPad,
        None
    }
    private PadType currentPadType;

    // Start is called before the first frame update
    void Start()
    {
       // padsArray = new GameObject[] { leftHandPad, rightHandPad };
       // materialArray = new Material[] { leftHandMaterial, rightHandMaterial };
         LeftHandPad = leftHandPad.GetComponent<Renderer>();
         RightHandPad = rightHandPad.GetComponent<Renderer>();
         leftHandLight = LeftHandPad.GetComponent<Light>();
         rightHandLight = RightHandPad.GetComponent<Light>();
         scoreSystem = FindObjectOfType<ScoreSystem>();
        RoundIntiator();
      //  XRControlle rManager controllerManager = FindObjectOfType<XRControllerManager>();

    }

    // Update is called once per frame
    void Update()
    {
        if (timerActive)
        {
            UpdateTimer();
        }

        if (timerRoundActive)
        {
            UpdateRoundTimer();
        }
     
    }

    void StartRandom()
    {
        int randomNum = UnityEngine.Random.Range(0, 2);
        textArraySelected = randomNum == 0 ? textArray1 : textArray2;
        if (randomNum == 0)
        {
            textArraySelected = textArray1;
            startOrder = textArray1[0];
            currentArrOrder = startOrder;
            finishOrder = textArray1[textArray1.Length-1];
            previousOrderDone = false;
        } else {
            textArraySelected = textArray2;
            startOrder = textArray2[0];
            currentArrOrder = startOrder;
            finishOrder = startOrder; // X 
            previousOrderDone = true;
        }
        LeftHandRandomizer();
        RightHandRandomizer();
        Debug.Log("First set: "+currentOrder + " finishorder:" + finishOrder);
    }
    
    void LeftHandRandomizer()
    {
        //LeftHandPad = leftHandPad.GetComponent<Renderer>();
        // random material --> which hand
        // random text --> [1,2,X]
        LeftHandPad.material = handMaterials[UnityEngine.Random.Range(0, handMaterials.Length)];
        leftHandText.SetText(textArraySelected[UnityEngine.Random.Range(0, textArraySelected.Length)]);
    }
    void RightHandRandomizer()
    {
        RightHandPad.material = handMaterials[UnityEngine.Random.Range(0, handMaterials.Length)];
        //rightHandText.SetText(textArraySelected[UnityEngine.Random.Range(0, textArraySelected.Length)]);
        foreach (var item in textArraySelected)
        {
            if (item!=leftHandText.text)
            {
                rightHandText.SetText(item);
            }
        }
    }

    public void SetOrder(string order) {
        currentOrder = order;
    }
    public void SetHandType(HandType handType) {

        currentHandType = handType;
    }
    public void SetPadType(PadType padType)
    {

        currentPadType = padType;
    }
    public Boolean CheckOrder() {
        Debug.Log(currentOrder + " " + finishOrder);
        if (currentOrder == finishOrder && previousOrderDone)
        {
            HitSuccess();
            RoundWinFinializer();
            return true;
        }
        else if (currentOrder == currentArrOrder && currentOrder != "X")
        {
            HitSuccess();
            previousOrderDone = true;
            return true;
        }
        else {
            HitFailure();
            return false;
        }
     
    }
    void MoveOrder() {

        for (int i = 0; i < textArraySelected.Length; i++) {
            if (currentArrOrder == textArraySelected[i] && i+1 < textArraySelected.Length)
            {
                currentArrOrder = textArraySelected[i + 1];
                break;
            } 
        } 
    }

    public void HitSuccess() {
        // 1. Add Score
        // 2. Show green light effect
        // set Next
        if (currentHandType == HandType.LeftHand)        // which hand -->  LeftPadHitAnimation(); + LeftPadDeactivator or  RightPadHitAnimation(); + RightPadDeactivator
        {
            XRHandController.HapticLeftSuccess();
        }
        else if (currentHandType == HandType.RightHand)
        {
            XRHandController.HapticRightSuccess();
        }
        if (currentPadType == PadType.LeftPad)
        {
            LeftPadHitAnimation();
            LeftPadDeactivator();
        } else if (currentPadType == PadType.RightPad) {
            RightPadHitAnimation();
            RightPadDeactivator();
        }
        else
        {
            //
        }
        scoreSystem.AddScore(50);
        MoveOrder();
    }
    public void HitFailure()
    {
        // 1. take 1 life --> life-- if 0 --> lost -> RoundLostFinializer()
        // 2. Show red light Effect -->
        // which hand --> LeftPadFailAnimation or RightPadFailAnimation
        scoreSystem.LessLife();

        if (currentPadType == PadType.LeftPad)       // which hand --> LeftPadFailAnimation or RightPadFailAnimation
        {
            LeftPadFailAnimation();
           LeftPadTempDeactivator();
        }
        else if (currentPadType == PadType.RightPad)
        {
            RightPadFailAnimation();
            RightPadTempDeactivator();
        }

        if (scoreSystem.NoLifeChecker()) { // true if 0 lives
            RoundLostFinializer();
        }
        if (currentHandType == HandType.LeftHand)        // which hand -->  LeftPadHitAnimation(); + LeftPadDeactivator or  RightPadHitAnimation(); + RightPadDeactivator
        {
            XRHandController.HapticLeftFail();
        }
        else if (currentHandType == HandType.RightHand)
        {
            XRHandController.HapticRightFail();
        }
    
    }
    void RoundWinFinializer() { 
        Debug.Log("U Won the Round");
        animator.Play("TainerMovePadTrick2End");
        RightPadDeactivator();
        LeftPadDeactivator();
        RoundSpeedUp();
       // animator.speed = 1f;
        TimerIntiator();
        StopRoundTimer();

    }
    void RoundLostFinializer() // 1 life less if tound time out??
    {
        Debug.Log("U Lost the Round");
        RightPadDeactivator();
        LeftPadDeactivator();
        RoundSpeedUp();
        animator.Play("TainerMovePadTrick2End");
        TimerIntiator();
    }

   void RoundSpeedUp()
    {
        roundSpeed = roundSpeed - 0.5f;
        timeRoundRemaining = 10f + roundSpeed;
        animatorSpeed = animatorSpeed + 0.5f;
        timeRemaining = timeRemaining + (roundSpeed * 1.2f);

    }

    void RoundIntiator()
    {
        RightPadActivator();
        LeftPadActivator();
        StartRandom();
        animator.speed = animatorSpeed;
        animator.Play("TainerMovePadTrick2Start");
        TimerRoundIntiator();
    }
    
    void TimerIntiator() {
        timerActive = true;
    }
    void TimerRoundIntiator()
    {
        timerRoundActive = true;
    }



    private void UpdateTimer()
    {
        // Subtract the time elapsed since the last frame from the time remaining
        timeRemaining -= Time.deltaTime;

        // Update the timer text with the remaining time
        timerText = Mathf.RoundToInt(timeRemaining).ToString();
     //   Debug.Log("New Round begins in: " + timerText);
        // If the time runs out, stop the timer and do something
        if (timeRemaining <= 0.0f)
        {
            StopTimer();
            DoSomething();
        }
    }

    private void StopTimer()
    {
        // Stop the timer by setting the time remaining to 0
        timerActive = false;
        timeRemaining = 5.0f;
    }

    private void DoSomething()
    {
        // Do something when the timer runs out
      RoundIntiator();
    }

    private void UpdateRoundTimer()
    {
        // Subtract the time elapsed since the last frame from the time remaining
        timeRoundRemaining -= Time.deltaTime;

        // Update the timer text with the remaining time
        timerRoundText = Mathf.RoundToInt(timeRoundRemaining).ToString();
      //  Debug.Log("Time Remaining for Round: " + timerRoundText);
        RoundTimeText.text = "Timer: "+timerRoundText;
        // If the time runs out, stop the timer and do something
        if (timeRoundRemaining <= 0.0f)
        {
            StopRoundTimer();
            DoRoundSomething();
        }
    }

    private void StopRoundTimer()
    {
        // Stop the timer by setting the time remaining to 0
        timerRoundActive = false;
        timeRoundRemaining = 10f + roundSpeed;
    }

    private void DoRoundSomething()
    {
        // Do something when the timer runs out
        // RoundIntiator();
        RoundLostFinializer();
    }

    void LeftPadHitAnimation()
    {
        leftHandLight.color = Color.green;
        StartCoroutine(BlinkCoroutineLeftPadHit());
    }
    private IEnumerator BlinkCoroutineLeftPadHit()
    {
        while (blinkCount < numBlinks)
        {
            leftHandLight.intensity = 200f;
            yield return new WaitForSeconds(0.1f);
            leftHandLight.intensity = 0;
            yield return new WaitForSeconds(0.1f);
            blinkCount++;
        }
        leftHandLight.intensity = 0;
        blinkCount = 0;
    }
    void RightPadHitAnimation()
    {
        rightHandLight.color = Color.green;
        StartCoroutine(BlinkCoroutineRightPadHit());
    }
    private IEnumerator BlinkCoroutineRightPadHit()
    {
        while (blinkCount < numBlinks)
        {
            rightHandLight.intensity = 200f;
            yield return new WaitForSeconds(0.1f);
            rightHandLight.intensity = 0;
            yield return new WaitForSeconds(0.1f);
            blinkCount++;
        }
        leftHandLight.intensity = 0;
        blinkCount = 0;
    }
    void LeftPadFailAnimation()
    {
        leftHandLight.color = Color.red;
        StartCoroutine(BlinkCoroutineLeftPadFail());
    }
    private IEnumerator BlinkCoroutineLeftPadFail()
    {
        while (blinkCount < numBlinks)
        {
            leftHandLight.intensity = 200f;
            yield return new WaitForSeconds(0.1f);
            leftHandLight.intensity = 0;
            yield return new WaitForSeconds(0.1f);
            blinkCount++;
        }
        leftHandLight.intensity = 0;
        blinkCount = 0;
    }
    void RightPadFailAnimation()
    {
        rightHandLight.color = Color.red;
        StartCoroutine(BlinkCoroutineRightPadFail());
    }
    private IEnumerator BlinkCoroutineRightPadFail()
    {
        while (blinkCount < numBlinks)
        {
            rightHandLight.intensity = 200f;
            yield return new WaitForSeconds(0.1f);
            rightHandLight.intensity = 0;
            yield return new WaitForSeconds(0.1f);
            blinkCount++;
        }
        leftHandLight.intensity = 0;
        blinkCount = 0;
    }

    void LeftPadActivator()
    {
        leftHandPad.layer = LayerMask.NameToLayer("enemylayer");
    }

    void RightPadActivator() {
        rightHandPad.layer = LayerMask.NameToLayer("enemylayer");
    }

    void LeftPadDeactivator()
    {
        leftHandPad.layer = LayerMask.NameToLayer("Default");
        LeftHandPad.material = PadsDeactivateMaterial;
    }
    void RightPadDeactivator()
    {
        //rightHandPad
        rightHandPad.layer = LayerMask.NameToLayer("Default");
        RightHandPad.material = PadsDeactivateMaterial;
    }

    void LeftPadTempDeactivator()
    {
        StartCoroutine(LeftPadTempDeactivatorTimer());
    }
    void RightPadTempDeactivator()
    {
        StartCoroutine (RightPadTempDeactivatorTimer());
    }
    private IEnumerator LeftPadTempDeactivatorTimer()
    {
        leftHandPad.layer = LayerMask.NameToLayer("Default");
        yield return new WaitForSeconds(1f);
        leftHandPad.layer = LayerMask.NameToLayer("enemylayer");
    }
    private IEnumerator RightPadTempDeactivatorTimer()
    {

        rightHandPad.layer = LayerMask.NameToLayer("Default");
        yield return new WaitForSeconds(1f);
        rightHandPad.layer = LayerMask.NameToLayer("enemylayer");
    }
}
