using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyObject;
    public Transform[] pointsLocation;
    public float beat = (60 / 130) * 2;
        private float timer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > beat) {
        GameObject enemyObjectTmp =Instantiate(enemyObject, pointsLocation[Random.Range(0, 2)]);
            enemyObjectTmp.transform.localPosition = Vector3.zero;
        timer -= beat;
        }
        timer += Time.deltaTime;
    }
}
