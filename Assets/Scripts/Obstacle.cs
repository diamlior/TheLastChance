﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform obstacle;
    public GameObject obstacle_go;
    public float speed = 1;
    public bool isPaused = false;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!isPaused)
            obstacle.position += Vector3.back * Time.deltaTime * speed;
    }

    public void PauseObject()
    {
        isPaused = true;
    }

    public void ReturnObject()
    {
        isPaused = false;
    }
}
