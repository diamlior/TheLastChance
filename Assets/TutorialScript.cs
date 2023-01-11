using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialScript : MonoBehaviour
{
    public GameObject left, right, space, text;
    string state = "left";
    // Start is called before the first frame update
    void Start()
    {
        left.SetActive(true);
        right.SetActive(false);
        space.SetActive(false);
        text.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftArrow) && state.Equals("left"))
        {
            state = "right";
            left.SetActive(false);
            right.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) && state.Equals("right"))
        {
            state = "space";
            right.SetActive(false);
            space.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Space) && state.Equals("space"))
        {
            state = "";
            space.SetActive(false);
            text.SetActive(true);
        }
    }
}
