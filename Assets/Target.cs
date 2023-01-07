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
    }
}
