using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Movement : MonoBehaviour
{
    public GameObject player;
    public Rigidbody rb;
    public GameObject FailedScreen;
    public GameObject pauseButton;
    public float jumpForce = 5;
    public float speed = 100;
    public bool isGrounded = true, stopJumped = false, isPenaltyMode = false, isShoot = false;
    bool movingLeft = false, movingRight = false;
    float currentX, targetX, startingX;
    
    string sceneName;
    Animator animator;
    private LevelChangerScript levelChangeScript;
    public bool isEnabled = true;

    //Penalty Vairables
    public float Force;
    public Transform target;
    public Slider forceUI;
    public GameObject Gauage;
    public GameObject GoalCanvas;
    GameObject goalObject;
    public bool didScore = false;
    Boolean backToBase;
    Vector3 startPos;

    Vector3 GoalPos;


    //public Canvas goalMsgCanvas;

    // Start is called before the first frame update
    void Start()
    {
        goalObject = GameObject.FindGameObjectWithTag("Goal");
        backToBase = false;
        startingX = transform.position.x;
        currentX = (float)System.Math.Round(transform.position.x);
        sceneName = SceneManager.GetActiveScene().name;
        levelChangeScript = GameObject.Find("LevelChanger").GetComponent<LevelChangerScript>();
        animator = player.GetComponent<Animator>();
        startPos = transform.position;
    }
    void OnCollisionStay()
    {
        isGrounded = true;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        animator.enabled = isEnabled;
        /*
        if (didScore)
        {
            float transitionSpeed = 20f;
            transform.position = Vector3.Lerp(transform.position, startPos, Time.deltaTime * transitionSpeed);
            if (Vector3.Distance(transform.position,startPos) < 0.5f)
            {
                backToBase = true;
                transform.position = startPos;
                SceneSwitcher();
            }
                
        }
        */
        if (!isEnabled)
            return;
        if (Input.GetKey(KeyCode.RightArrow) && !movingRight && currentX <= startingX && !isShoot)
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
            rb.angularVelocity = Vector3.zero;
            rb.AddForce(Vector3.right * speed);
            currentX = (float)System.Math.Round(transform.position.x);
            targetX = currentX + 1f;
            movingRight = true;
            movingLeft = false;
        }

        else if (Input.GetKey(KeyCode.LeftArrow) && !movingLeft && currentX >= startingX && !isShoot)
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
            rb.angularVelocity = Vector3.zero;
            rb.AddForce(Vector3.left * speed);
            currentX = (float)System.Math.Round(transform.position.x);
            targetX = currentX - 1f;
            movingLeft = true;
            movingRight = false;
        }
        else if ((movingLeft || movingRight) && System.Math.Abs(transform.position.x - targetX) < 0.05f && !isShoot)
        {
            movingRight = false;
            movingLeft = false;
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
            rb.angularVelocity = Vector3.zero;
            currentX = (float)System.Math.Round(transform.position.x);
            float y = transform.position.y;
            float z = transform.position.z;
            player.GetComponent<Transform>().position = new Vector3(currentX, y, z);
        }

        else if (Input.GetKeyUp(KeyCode.Space) && isPenaltyMode && !isShoot) //shoot
        {
            //shoot();
            StartCoroutine(Wait());
        }
        else if (Input.GetKey(KeyCode.Space) && isGrounded && !isShoot)
        {
            if (!isPenaltyMode)
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                isGrounded = false;
                stopJumped = false;
            }
            else
            {
                Force++;
                slider();
            }
        }       
        
        else if (!Input.GetKey(KeyCode.Space) && !isGrounded && !stopJumped && !isShoot)
        {
            if (rb.velocity.y > 0)
                rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            stopJumped = true;
        }
        
    }
    void shoot()
    {
        isShoot = true;
        Vector3 Shoot = (target.position - this.transform.position).normalized;
        GetComponent<Rigidbody>().AddForce(Shoot * Force + new Vector3(0, 3f, 0), ForceMode.Impulse);
        
    }
    IEnumerator PauseTwoSecExecuteGoalSwitch()
    {
        yield return new WaitForSeconds(2f);
        Debug.Log("Two seconds have passed!");

        SceneSwitcher();
    }
    IEnumerator Wait()
    {
        shoot();
        yield return new WaitForSeconds(1.5f);
        yield return new WaitForSeconds(1f);

        if (!didScore)
        {
            FailedScreen.SetActive(true);
            pauseButton.SetActive(false);
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        }
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
    
    void OnCollisionEnter(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            Debug.DrawRay(contact.point, contact.normal, Color.white);

        }
        string collider = collision.collider.name;
        if (collider != "Floor")
        {
            //if (didScore && backToBase)
                if (didScore)
                {
                Debug.Log("OnCollision did score is true");
                
            }
            else if (collider.Equals("EndingBlock") && backToBase)
            {
                SceneSwitcher();
            }
            else
            {
                if (!isPenaltyMode && !didScore)
                {
                    Debug.Log("You are dead 1st!");
                    FailedScreen.SetActive(true);
                    pauseButton.SetActive(false);
                    PauseAll();
                    rb.constraints = RigidbodyConstraints.None;
                    rb.AddForce(new Vector3(1, 1, -1) * 100);
                    //levelChangeScript.FadeToLevel(sceneName);
                }
                else
                {
                    //PauseForThreeSeconds();
                    if (!didScore)
                    {
                        Debug.Log("You are dead! 2nd");
                        FailedScreen.SetActive(true);
                        pauseButton.SetActive(false);
                        PauseAll();
                        rb.constraints = RigidbodyConstraints.None;
                        rb.AddForce(new Vector3(1, 1, -1) * 100);
                    }
                }

            }

        }

    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "EndRunBlock")
        {
            PenaltyMode();
        }
        if (other.gameObject.name == "GoalBlock")
        {
            Debug.Log(other.name + " Triggered");
            GoalCanvas.SetActive(true);
            PauseAllNotTotal();
            StartCoroutine(PauseTwoSecExecuteGoalSwitch());
            didScore = true;
            //goalObject.SetActive(false);
        }
        if (other.gameObject.name == "BordersDown")
        {
            Debug.Log("You are dead! down border");
            FailedScreen.SetActive(true);
            pauseButton.SetActive(false);
            PauseAll();
            rb.constraints = RigidbodyConstraints.None;
            rb.AddForce(new Vector3(1, 1, -1) * 100);
        }
        
    }

    void SceneSwitcher()
    {
        string nextScene = "";
        switch (sceneName)
        {
            case "Tutorial":
                nextScene = "StageOne";
                break;
            case "StageOne":
                nextScene = "StageTwo";
                break;
            case "StageTwo":
                nextScene = "MainMenu";
                break;
            default:
                nextScene = "MainMenu";
                break;
        }
        Debug.Log("next scene: " + nextScene);
        levelChangeScript.FadeToLevel(nextScene);
    }
    

    public void PenaltyMode()
    {
        isPenaltyMode = true;
        Gauage.SetActive(true);
        
    }

    public void PauseAll()
    {
        GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
        try
        {
            foreach (GameObject obs in obstacles)
            {
                Obstacle obstacle = obs.GetComponent<Obstacle>();
                if (obstacle != null)
                    obstacle.PauseObject();
                else
                    Debug.Log(obs.name);
            }
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }

        isEnabled = false;

        rb.constraints = RigidbodyConstraints.FreezePosition;
    }

    public void PauseAllNotTotal()
    {
        GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
        try
        {
            foreach (GameObject obs in obstacles)
            {
                Obstacle obstacle = obs.GetComponent<Obstacle>();
                if (obstacle != null)
                    obstacle.PauseObject();
                else
                    Debug.Log(obs.name);
            }
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }

        isEnabled = false;
        //rb.constraints = RigidbodyConstraints.FreezePosition;
    }
    public void ReturnAll()
    {

        GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
        foreach (GameObject obs in obstacles)
        {
            obs.GetComponent<Obstacle>().ReturnObject();
        }
        rb.constraints = RigidbodyConstraints.None;
        isEnabled = true;
    }
    
}
