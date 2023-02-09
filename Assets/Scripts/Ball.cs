using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Ball : MonoBehaviour
{
    // The force to apply to the ball when shooting it
    public float Force;
    // Target is for the player to direct his shot
    public Transform target;
    // forceUI is for the player to choose how strong to shoot the ball
    public Slider forceUI;
    
    GoalKeeper goal;

    public GameObject GoalKeeper;

    Vector3 StartPos;

    Vector3 GoalPos;

    private LevelChangerScript levelChangeScript;
    
    string sceneName = "";
    bool didShoot = false;
    bool didScore = false;
    public GameObject FailedScreen;
    public GameObject pauseButton;
    private void Start()
    {
        levelChangeScript = GameObject.Find("LevelChanger").GetComponent<LevelChangerScript>();
        sceneName = SceneManager.GetActiveScene().name;

        StartPos = transform.position;
        GoalPos = GoalKeeper.transform.position;
    }
    // The ball object
    //public GameObject ball;

    // Update is called once per frame
    void Update()
    {
        if (didShoot)
            return;
        if (Input.GetKey(KeyCode.Space)) //Fill slider depending on Force value
        {
            Force++;
            slider();
        }

        if (Input.GetKeyUp(KeyCode.Space)) //shoot
        {
            //shoot();
            StartCoroutine(Wait());
            
        }
    }

    void shoot()
    {
            Vector3 Shoot = (target.position - this.transform.position).normalized;
            GetComponent<Rigidbody>().AddForce(Shoot * Force + new Vector3(0, 3f, 0), ForceMode.Impulse);
    }

    public void slider()
    {
        forceUI.value = Force;
    }

    public void ResetGauge()
    {
        Force = 0;
        forceUI.value = 0;
    }

    IEnumerator Wait()
    {
        didShoot = true;
        shoot();
        yield return new WaitForSeconds(1.5f); //wait before reseting slider and force
        FindObjectOfType<GoalKeeper>().GoalMove();
        yield return new WaitForSeconds(1.5f);
        ResetGauge();

        //GetComponent<Rigidbody>().angularDrag = 40;
        yield return new WaitForSeconds(1f);

        transform.position = StartPos; //reset ball position
        GoalKeeper.transform.position = GoalPos; //reset GoalKepper Position

        FindObjectOfType<GoalKeeper>().Reset();
        FindObjectOfType<GoalKeeper>().Move = 0; //reset index
        if (!didScore)
        {
            FailedScreen.SetActive(true);
            pauseButton.SetActive(false);
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        }
            

    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name + " Triggered");
        if (other.name.Equals("Goal"))
            didScore = true;
    }


    void OnCollisionEnter(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            Debug.DrawRay(contact.point, contact.normal, Color.white);

        }
        string collider = collision.collider.name;
        if (collider != "Floor")
        {
            Debug.Log(collider + " Collid");
            if (didScore)
            {
                
                string nextScene;
                switch (sceneName)
                {
                    case "StageOne":
                        nextScene = "PenaltyScene";
                        break;
                    case "PenaltyScene":
                        nextScene = "StageOne";
                        break;
                    default:
                        nextScene = "PenaltyScene";
                        break;
                }
                levelChangeScript.FadeToLevel(nextScene);
            }
          
        }
        
    }
}