using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform obstacle;
    public GameObject obstacle_go;
    public float speed = 1;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        obstacle.position += Vector3.back * Time.deltaTime * speed;
    }
}
