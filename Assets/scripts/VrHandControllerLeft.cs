using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VrHandControllerLeft : MonoBehaviour
{
    public LayerMask layer;
    public Transform gameObj;
    public float distance = 5f;
    private ScoreSystem scoreSystem;
    public Material leftHandMaterial;
    // Start is called before the first frame update
    void Start()
    {
        scoreSystem = FindObjectOfType<ScoreSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        //Debug.DrawLine(gameObj.position, new Vector3(gameObj.position.x, gameObj.position.y, 10), Color.yellow);
        Debug.DrawRay(transform.position, transform.forward * distance, Color.blue);
        if (Physics.Raycast(transform.position, transform.forward * distance, out hit, distance, layer))
        {
            Debug.Log("something hit");
            Material material = hit.collider.gameObject.GetComponent<Renderer>().material; /// check if color matches hand color --> sucess : erorr red light lost point
            if (material == leftHandMaterial) { } else { }
            Debug.Log(material);
          //  Destroy(hit.transform.gameObject);
            scoreSystem.AddScore(50);
        }
    }
}
