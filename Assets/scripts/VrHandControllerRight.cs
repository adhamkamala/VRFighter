using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VrHandControllerRight : MonoBehaviour
{
    public LayerMask layer;
    public Transform gameObj;
    public float distance = 5f;

    public Material rightHandMaterial;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        //Debug.DrawLine(gameObj.position, new Vector3(gameObj.position.x, gameObj.position.y, 10), Color.yellow);
        Debug.DrawRay(transform.position, transform.forward * distance, Color.red);
        if (Physics.Raycast(transform.position, transform.forward * distance, out hit, distance, layer))
        {
            Debug.Log("something hit");
            Material material = hit.collider.gameObject.GetComponent<Renderer>().material; /// check if color matches hand color --> sucess : erorr red light lost point
            if (material == rightHandMaterial)
            {
                //CheckOrder()
            }
            else
            {
                //HitFaliure()
            }
            Debug.Log(material);
            //Destroy(hit.transform.gameObject);
           
        }
    }
}
