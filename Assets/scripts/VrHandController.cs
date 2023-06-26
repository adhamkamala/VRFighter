using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VrHandController : MonoBehaviour
{ 
    public LayerMask layer;
    public Transform gameObj;
    public float distance = 5f;
    private ScoreSystem scoreSystem;

    // Start is called before the first frame update
    void Start()
    {
        scoreSystem = FindObjectOfType<ScoreSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward * distance, out hit, distance, layer))
        {
            Debug.Log("something hit");
            Destroy(hit.transform.gameObject);
            scoreSystem.AddScore(50);
        }
    }
}
