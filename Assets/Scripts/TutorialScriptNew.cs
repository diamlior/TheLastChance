using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialScriptNew : MonoBehaviour
{

    public GameObject CanvasLeft;
    public GameObject CanvasRight;
    public GameObject CanvasJump;
    public GameObject CanvasShoot1;
    public GameObject CanvasShoot2;
    private string state;
    private int counter = 0;

    // Start is called before the first frame update
    void Start()
    {
        state = "";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) && state.Equals("left"))
        {
            CanvasLeft.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) && state.Equals("right"))
        {
            if (counter == 1)
                CanvasRight.SetActive(false);
            counter++;
        }
        if (Input.GetKeyDown(KeyCode.Space) && state.Equals("jump"))
        {
            CanvasJump.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "LeftBlock")
        {
            CanvasLeft.SetActive(true);
            state = "left";
        }
        if (other.gameObject.name == "RightBlock")
        {
            CanvasLeft.SetActive(false);
            CanvasRight.SetActive(true);
            state = "right";
        }
        if (other.gameObject.name == "JumpBlock")
        {
            CanvasRight.SetActive(false);
            CanvasJump.SetActive(true);
            state = "jump";
        }
        if (other.gameObject.name == "ShootBlock1")
        {
            CanvasJump.SetActive(false);
            CanvasShoot1.SetActive(true);
            state = "shoot";
        }
        if (other.gameObject.name == "ShootBlock2")
        {
            CanvasShoot1.SetActive(false);
            CanvasShoot2.SetActive(true);
        }
        if (other.gameObject.name == "ShootBlockEnd")
        {
            CanvasShoot2.SetActive(false);
        }

    }
}
