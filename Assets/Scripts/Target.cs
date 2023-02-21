using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public float minX = 418;
    public float maxX = 426;
    public float minY = 1.25f;
    public float maxY = 4;  
    // Start is called before the first frame update
    void Start()
    { 
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.A))
        {
            transform.Translate(-Vector3.right * 3f * Time.deltaTime);
        }

        if(Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * 3f * Time.deltaTime);
        }

        if(Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.up * 3f * Time.deltaTime);
        }

        if(Input.GetKey(KeyCode.S))
        {
            transform.Translate(-Vector3.up * 3f * Time.deltaTime);
        }

        //limit the x,y values
        Vector3 position = transform.position;

        if (position.x < minX)
        {
            position.x = minX;
        }
        else if (position.x > maxX)
        {
            position.x = maxX;
        }

        if (position.y < minY)
        {
            position.y = minY;
        }
        else if (position.y > maxY)
        {
            position.y = maxY;
        }

        transform.position = position;
    }
}
