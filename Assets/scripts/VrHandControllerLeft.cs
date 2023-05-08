using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VrHandControllerLeft : MonoBehaviour
{
    public LayerMask layer;
    public Transform gameObj;
    public float distance = 5f;
    public Material leftHandMaterial;
    private PadsSystem padsSystem;
    private string rightPadName = "RightHandIndicator";
    private string leftPadName = "LeftHandIndicator";
    // Start is called before the first frame update
    void Start()
    {
        padsSystem = FindObjectOfType<PadsSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        //Debug.DrawLine(gameObj.position, new Vector3(gameObj.position.x, gameObj.position.y, 10), Color.yellow);
        Debug.DrawRay(transform.position, transform.forward * distance, Color.red);
        if (Physics.Raycast(transform.position, transform.forward * distance, out hit, distance, layer))
        {
            GameObject parentObject = hit.transform.gameObject;
            Material material = hit.collider.gameObject.GetComponent<Renderer>().material; /// check if color matches hand color --> sucess : erorr red light lost point
            if (material.name.Contains(leftHandMaterial.name) ) {
                GameObject childTextObject = parentObject.transform.Find("TextHand").gameObject;
                Debug.Log(childTextObject.GetComponent<TextMeshPro>().text);
                padsSystem.SetOrder(childTextObject.GetComponent<TextMeshPro>().text);
                padsSystem.SetHandType(PadsSystem.HandType.LeftHand);
                if (hit.collider.gameObject.name.Contains(leftPadName))
                {
                    padsSystem.SetPadType(PadsSystem.PadType.LeftPad);
                } else if (hit.collider.gameObject.name.Contains(rightPadName))
                {
                    padsSystem.SetPadType(PadsSystem.PadType.RightPad);
                }
                padsSystem.CheckOrder();
            
            } else {
                padsSystem.SetHandType(PadsSystem.HandType.LeftHand);
                if (hit.collider.gameObject.name.Contains(leftPadName))
                {
                    padsSystem.SetPadType(PadsSystem.PadType.LeftPad);
                }
                else if (hit.collider.gameObject.name.Contains(rightPadName))
                {
                    padsSystem.SetPadType(PadsSystem.PadType.RightPad);
                }
                padsSystem.HitFailure();
            }
        }
    }
}
