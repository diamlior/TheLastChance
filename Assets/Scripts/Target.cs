using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(-Vector3.right * 3f * Time.deltaTime);
        }

        if(Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(Vector3.right * 3f * Time.deltaTime);
        }

        if(Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(Vector3.up * 3f * Time.deltaTime);
        }

        if(Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(-Vector3.up * 3f * Time.deltaTime);
        }

        //limit the x,y values
        if(transform.position.x < 420)
            transform.position = new Vector3(420, transform.position.y, transform.position.z);
        if(transform.position.x > 424)
            transform.position = new Vector3(424, transform.position.y, transform.position.z);
        if(transform.position.y > 4)
            transform.position = new Vector3(transform.position.y, 4, transform.position.z);
        if(transform.position.y < 1)
            transform.position = new Vector3(transform.position.y, 1, transform.position.z);
    }
}
