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

    private GameObject[] padsArray;
    private Material[] materialArray;
    //private TextMeshPro[] textArray;
   // private string[] textArray = new string[] { "1", "2", "X" };
    private string[] textArray1 = new string[] { "1", "2"};
    private string[] textArray2 = new string[] { "1", "X" };
    private string[] textArraySelected;
    private Renderer LeftHandPad;
    private Renderer RightHandPad;


    // Start is called before the first frame update
    void Start()
    {
        padsArray = new GameObject[] { leftHandPad, rightHandPad };
        materialArray = new Material[] { leftHandMaterial, rightHandMaterial };
       // textArray = new TextMeshPro[] { leftHandText, rightHandText };
         LeftHandPad = leftHandPad.GetComponent<Renderer>();
         RightHandPad = rightHandPad.GetComponent<Renderer>();

        // Set the new material
        //    LeftHandPad.material = leftHandMaterial;
        //   RightHandPad.material = leftHandMaterial;

        //   leftHandText.SetText("1");
        //    rightHandText.SetText("X");

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
}
