using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VrHandController : MonoBehaviour
{ 
    public LayerMask layer;
    public Transform gameObj;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Debug.DrawLine(gameObj.position, new Vector3(gameObj.position.x, gameObj.position.y, 10), Color.yellow);
        Debug.DrawRay(transform.position, transform.forward * 10f, Color.red);
        if (Physics.Raycast(transform.position, transform.forward * 10f, out hit, 10, layer))
        {
            Debug.Log("something hit");
            Destroy(hit.transform.gameObject);
        }
    }
}
