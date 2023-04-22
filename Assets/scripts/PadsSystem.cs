using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PadsSystem : MonoBehaviour
{
    public GameObject leftHandPad;
    public GameObject rightHandPad;

    public Material leftHandMaterial;
    public Material rightHandMaterial;

    public TextMeshPro leftHandText;
    public TextMeshPro rightHandText;

    public GameObject[] handIndicators = new GameObject[2];
    public TextMeshPro[] handTexts = new TextMeshPro[2];
    public Material[] handMaterials = new Material[2];

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


    // Start is called before the first frame update
    void Start()
    {
       // padsArray = new GameObject[] { leftHandPad, rightHandPad };
       // materialArray = new Material[] { leftHandMaterial, rightHandMaterial };
         LeftHandPad = leftHandPad.GetComponent<Renderer>();
         RightHandPad = rightHandPad.GetComponent<Renderer>();
        scoreSystem = FindObjectOfType<ScoreSystem>();

        StartRandom();
    }

    // Update is called once per frame
    void Update()
    {
        
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
        } else {
            textArraySelected = textArray2;
            startOrder = textArray2[0];
            currentArrOrder = startOrder;
            finishOrder = startOrder; // X 
        }
        LeftHandRandomizer();
        RightHandRandomizer();

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
    public Boolean CheckOrder() {
        if (currentOrder == finishOrder)
        {
            HitSuccess();
            RoundWinFinializer();
            return true;
        }
        else if (currentOrder == currentArrOrder && currentOrder != "X")
        {
            HitSuccess();
            return true;
        }
        else {
            HitFailure();
            return false;
        }
     
    }
    void MoveOrder() { 
        for (int i = 0; i < textArraySelected.Length; i++) {
            if (currentArrOrder == textArraySelected[i] && i+1<= textArraySelected.Length)
            {
                currentArrOrder = textArraySelected[i + 1];
            } 
        } 
    }

    public void HitSuccess() {
        // 1. Add Score
        // 2. Show green light effect
        scoreSystem.AddScore(50);
        // set Next
        MoveOrder();
    }
    public void HitFailure()
    {
        // 1. take 1 life --> life-- if 0 --> lost -> RoundLostFinializer()
        // 2. Show red light Effect -->
        scoreSystem.LessLife();
        if (scoreSystem.NoLifeChecker()) { // true if 0 lives
            RoundLostFinializer();
        }
    }
    void RoundWinFinializer() { 
    
    }
    void RoundLostFinializer()
    {

    }
    
    void LeftPadHitAnimation()
    {

    }
    void RightPadHitAnimation()
    {

    }

    void LeftPadFailAnimation()
    {

    }
    void RightPadFailAnimation()
    {

    }

    void LeftPadActivator()
    {

    }

    void RightPadActivator() { 
    }

    void LeftPadDeactivator()
    {

    }
    void RightPadDeactivator()
    {
        //leftHandPad
    }
}
