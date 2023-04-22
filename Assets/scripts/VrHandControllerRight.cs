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

    // Start is called before the first frame update
    void Start()
    {
        padsSystem = FindObjectOfType<PadsSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Debug.DrawRay(transform.position, transform.forward * distance, Color.blue);
        if (Physics.Raycast(transform.position, transform.forward * distance, out hit, distance, layer))
        {
            GameObject parentObject = hit.transform.gameObject;
            Material material = hit.collider.gameObject.GetComponent<Renderer>().material; /// check if color matches hand color --> sucess : erorr red light lost point
            if (material.name.Contains(rightHandMaterial.name))
            {
                GameObject childTextObject = parentObject.transform.Find("TextHand").gameObject;
                Debug.Log(childTextObject.GetComponent<TextMeshPro>().text);
                padsSystem.SetOrder(childTextObject.GetComponent<TextMeshPro>().text);
                padsSystem.CheckOrder();
            }
            else
            {
                padsSystem.HitFailure();
            }

        }
    }
}
