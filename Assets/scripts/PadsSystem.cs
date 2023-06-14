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
    public Material PadsDeactivateMaterial; 
    public TextMeshPro leftHandText; 
    public TextMeshPro rightHandText; 
    public Material[] handMaterials = new Material[2]; 
    public RoundSystem roundSystem;
    public XRHandController XRHandController;
    public int numBlinks = 2;

    private string[] padsComb1 = new string[] { "1", "2"}; 
    private string[] padsComb2 = new string[] { "1", "X" };
    private string startOrder;
    private string finishOrder; 
    private string currentOrder; 
    private string currentArrOrder; 
    private string[] textArraySelected;
    private Renderer leftHandPadRend;
    private Renderer rightHandPadRend;
    private ScoreSystem scoreSystem;
    private Light leftHandLight;
    private Light rightHandLight;
    private int blinkCount = 0;
    private bool previousOrderDone = false;
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

    public void Setup()
    {
        leftHandPadRend = leftHandPad.GetComponent<Renderer>();
        rightHandPadRend = rightHandPad.GetComponent<Renderer>();
        leftHandLight = leftHandPadRend.GetComponent<Light>();
        rightHandLight = rightHandPadRend.GetComponent<Light>();
        scoreSystem = FindObjectOfType<ScoreSystem>();
    }

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(this);
    }
    private void Awake()
    {
 
    }

    // Update is called once per frame
    void Update()
    {
    }

   public void StartRandomizePads()
    {
        int randomNum = UnityEngine.Random.Range(0, 2);
        textArraySelected = randomNum == 0 ? padsComb1 : padsComb2;
        if (randomNum == 0)
        {
            textArraySelected = padsComb1;
            startOrder = padsComb1[0];
            currentArrOrder = startOrder;
            finishOrder = padsComb1[padsComb1.Length-1];
            previousOrderDone = false;
        } else {
            textArraySelected = padsComb2;
            startOrder = padsComb2[0];
            currentArrOrder = startOrder;
            finishOrder = startOrder; // X 
            previousOrderDone = true;
        }
        LeftHandRandomizer();
        RightHandRandomizer();
    }
    
    void LeftHandRandomizer()
    {
        //LeftHandPad = leftHandPad.GetComponent<Renderer>();
        // random material --> which hand
        // random text --> [1,2,X]
      
        leftHandText.SetText(textArraySelected[UnityEngine.Random.Range(0, textArraySelected.Length)]);
    }
    void RightHandRandomizer()
    {
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
            roundSystem.EndRoundWin();
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
    public void HitSuccessMode2()
    {
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
        }
        else if (currentPadType == PadType.RightPad)
        {
            RightPadHitAnimation();
            RightPadDeactivator();
        }
        else
        {
            //
        }
    }
    public void HitFailureMode2()
    {
        // 1. take 1 life --> life-- if 0 --> lost -> RoundLostFinializer()
        // 2. Show red light Effect -->
        // which hand --> LeftPadFailAnimation or RightPadFailAnimation

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
        if (currentHandType == HandType.LeftHand)        // which hand -->  LeftPadHitAnimation(); + LeftPadDeactivator or  RightPadHitAnimation(); + RightPadDeactivator
        {
            XRHandController.HapticLeftFail();
        }
        else if (currentHandType == HandType.RightHand)
        {
            XRHandController.HapticRightFail();
        }

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
            roundSystem.EndFullRoundLose();
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
            leftHandLight.intensity = 2000f;
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
        leftHandPadRend.material = handMaterials[UnityEngine.Random.Range(0, handMaterials.Length)];
    }
    void RightPadActivator() {
        rightHandPad.layer = LayerMask.NameToLayer("enemylayer");
        rightHandPadRend.material = handMaterials[UnityEngine.Random.Range(0, handMaterials.Length)];
    }
    void LeftPadActivatorMode2()
    {
        leftHandPad.layer = LayerMask.NameToLayer("enemylayer");
    }
    void RightPadActivatorMode2()
    {
        rightHandPad.layer = LayerMask.NameToLayer("enemylayer");
    }
    void LeftPadDeactivator()
    {
        leftHandPad.layer = LayerMask.NameToLayer("Default");
        leftHandPadRend.material = PadsDeactivateMaterial;
    }
    void RightPadDeactivator()
    {
        //rightHandPad
        rightHandPad.layer = LayerMask.NameToLayer("Default");
        rightHandPadRend.material = PadsDeactivateMaterial;
    }

    void LeftPadTempDeactivator()
    {
        StartCoroutine(LeftPadTempDeactivatorTimer());
    }
    void RightPadTempDeactivator()
    {
        StartCoroutine (RightPadTempDeactivatorTimer());
    }
    
    public void ActivateBothPads() // activate and Randomize Colors
    {
        RightPadActivator();
        LeftPadActivator();
    }

    public void ActivateBothPadsMode2() // activate and Randomize Colors
    {
        RightPadActivatorMode2();
        LeftPadActivatorMode2();
    }
    public void DeactivateBothPads()
    {
        RightPadDeactivator();
        LeftPadDeactivator();
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


    // Mode 2

    public void setLeftPadColor(int colorNumber) // 0 --> blue ; 1 --> red
    {
        if (colorNumber == 0 || colorNumber == 1)
        {
            leftHandPadRend.material = handMaterials[colorNumber];
        }
    }
    public void setRightPadColor(int colorNumber)
    {
        if (colorNumber == 0 || colorNumber == 1)
        {
            rightHandPadRend.material = handMaterials[colorNumber];
        }
    }

    public void setOrderPads() // --> Net Order!!! fürs erste..
    {
        StartRandomizePads();
    }

    
    // --> Net Animation
}
