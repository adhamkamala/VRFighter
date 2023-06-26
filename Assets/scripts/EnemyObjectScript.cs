using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyObjectScript : MonoBehaviour
{
    public float speed = 5f;
    private Vector3 velocity;
    void Start()
    {
        velocity = new Vector3 (0, 0, -speed);
    }
    void Update()
    {
        transform.position += velocity * Time.deltaTime;
    }
}
