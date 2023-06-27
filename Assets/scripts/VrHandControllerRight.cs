using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VrHandControllerRight : MonoBehaviour
{
    public LayerMask layer;
    public Transform gameObj;
    public float distance = 5f;

    public Material rightHandMaterial;
    private PadsSystem padsSystem;
    private string rightPadName = "RightHandIndicator";
    private string leftPadName = "LeftHandIndicator";
    public Gamesystem gameSystem;
    public AudioManager audioManager;
    // Start is called before the first frame update
    void Start()
    {
        padsSystem = FindObjectOfType<PadsSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (gameSystem.getGameMode()==0)
        {
            if (Physics.Raycast(transform.position, transform.forward * distance, out hit, distance, layer))
            {
                audioManager.PlaySFX(audioManager.RightPunch);
                GameObject parentObject = hit.transform.gameObject;
                Material material = hit.collider.gameObject.GetComponent<Renderer>().material; /// check if color matches hand color --> sucess : erorr red light lost point
                if (material.name.Contains(rightHandMaterial.name))
                {
                    GameObject childTextObject = parentObject.transform.Find("TextHand").gameObject;
                    padsSystem.SetOrder(childTextObject.GetComponent<TextMeshPro>().text);
                    padsSystem.SetHandType(PadsSystem.HandType.RightHand);
                    if (hit.collider.gameObject.name.Contains(leftPadName))
                    {
                        padsSystem.SetPadType(PadsSystem.PadType.LeftPad);
                    }
                    else if (hit.collider.gameObject.name.Contains(rightPadName))
                    {
                        padsSystem.SetPadType(PadsSystem.PadType.RightPad);
                    }

                    padsSystem.CheckOrder();
                }
                else
                {
                    padsSystem.SetHandType(PadsSystem.HandType.RightHand);
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
        } else if (gameSystem.getGameMode() == 1)
        {
            if (Physics.Raycast(transform.position, transform.forward * distance, out hit, distance, layer))
            {
                audioManager.PlaySFX(audioManager.RightPunch);
                Material material = hit.collider.gameObject.GetComponent<Renderer>().material; /// check if color matches hand color --> sucess : erorr red light lost point
                if (material.name.Contains(rightHandMaterial.name))
                {
                    padsSystem.SetHandType(PadsSystem.HandType.RightHand);
                    if (hit.collider.gameObject.name.Contains(leftPadName))
                    {
                        padsSystem.SetPadType(PadsSystem.PadType.LeftPad);
                    }
                    else if (hit.collider.gameObject.name.Contains(rightPadName))
                    {
                        padsSystem.SetPadType(PadsSystem.PadType.RightPad);
                    }

                    padsSystem.HitSuccessMode2();
                }
                else
                {
                    padsSystem.SetHandType(PadsSystem.HandType.RightHand);
                    if (hit.collider.gameObject.name.Contains(leftPadName))
                    {
                        padsSystem.SetPadType(PadsSystem.PadType.LeftPad);
                    }
                    else if (hit.collider.gameObject.name.Contains(rightPadName))
                    {
                        padsSystem.SetPadType(PadsSystem.PadType.RightPad);
                    }
                    padsSystem.HitFailureMode2();
                }

            }
        }
      
    }
}
