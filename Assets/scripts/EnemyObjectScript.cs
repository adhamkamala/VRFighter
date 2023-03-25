using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyObjectScript : MonoBehaviour
{
    public float speed = 5f;
    private Vector3 velocity;
    // Start is called before the first frame update
    void Start()
    {
        velocity = new Vector3 (0, 0, -speed);
    }

    // Update is called once per frame
    void Update()
    {
        // transform.position += Time.deltaTime * transform.forward * speed;
        //transform.Translate(Vector3.fwd * speed * Time.deltaTime);
        transform.position += velocity * Time.deltaTime;
    }
}
