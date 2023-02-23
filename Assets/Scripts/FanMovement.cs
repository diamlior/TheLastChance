using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanMovement : MonoBehaviour
{
    // The maximum distance the fans will move up and down.
    public float maxDistance = 0.3f;

    // The speed at which the fans will move up and down.
    public float speed = 5f;
    float initialY;


    void Start()
    {
        initialY = transform.position.y;
    }

    void FixedUpdate()
    {
        float newY = initialY + Mathf.Sin(Time.time * speed) * maxDistance;
        try { transform.position = new Vector3(transform.position.x, newY, transform.position.z); }
        catch { }
    }
}
