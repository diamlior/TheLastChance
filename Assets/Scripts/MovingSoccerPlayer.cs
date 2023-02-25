using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingSoccerPlayer : MonoBehaviour
{
    public float speed = 5;
    public float maxRight;
    public float maxLeft;
    bool directionLeft = true;
    

    // Update is called once per frame
    void Update()
    {
        if (directionLeft)
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
            if (transform.localPosition.x < maxLeft)
                directionLeft = false;
        }
        else
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
            if (transform.localPosition.x > maxRight)
                directionLeft = true;
        }
    }
}
