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

    // Start is called before the first frame update
    void Start()
    {
        Renderer LeftHandPad = leftHandPad.GetComponent<Renderer>();
        Renderer RightHandPad = rightHandPad.GetComponent<Renderer>();

        // Set the new material
        LeftHandPad.material = leftHandMaterial;
        RightHandPad.material = leftHandMaterial;

        leftHandText.SetText("1");
        rightHandText.SetText("X");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void startRandom()
    {

    }
}
