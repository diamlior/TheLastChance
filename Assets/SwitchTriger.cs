using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchTriger : MonoBehaviour
{
    public GameObject ObjectToActivate;
    public GameObject Deactivate1;
    public GameObject Deactivate2;
    private bool LeftCLick;
    private bool RightCLick;
    private bool SpaceCLick;
    // Start is called before the first frame update
    void Start()
    {
        LeftCLick = false;
        RightCLick = false;
        SpaceCLick = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) && !LeftCLick){
            ObjectToActivate.SetActive(true);
            Deactivate1.SetActive(false);
            Deactivate2.SetActive(false);
            LeftCLick = true;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && !RightCLick && LeftCLick){
            ObjectToActivate.SetActive(true);
            Deactivate1.SetActive(false);
            Deactivate2.SetActive(false);
            RightCLick = true;
        }
        else if(Input.GetKeyDown(KeyCode.Space) && !SpaceCLick && RightCLick && LeftCLick){
                ObjectToActivate.SetActive(true);
                Deactivate1.SetActive(false);
                Deactivate2.SetActive(false);
                SpaceCLick = true;
            }
        else{}
    }
}
