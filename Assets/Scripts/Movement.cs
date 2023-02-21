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
    Transform transform;
    Animator animator;
    private LevelChangerScript levelChangeScript;
    public bool isEnabled = true;

    //Penalty Vairables
    public float Force;
    public Transform target;
    public Slider forceUI;
    public GameObject Gauage;
    public bool didScore = false;
    Vector3 StartPos;

    Vector3 GoalPos;

    bool didShoot = false;

    //public Canvas goalMsgCanvas;

    // Start is called before the first frame update
    void Start()
    {

        transform = player.GetComponent<Transform>();
        startingX = transform.position.x;
        currentX = (float)System.Math.Round(transform.position.x);
        sceneName = SceneManager.GetActiveScene().name;
        levelChangeScript = GameObject.Find("LevelChanger").GetComponent<LevelChangerScript>();
        animator = player.GetComponent<Animator>();
    }
    void OnCollisionStay()
    {
        isGrounded = true;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        animator.enabled = isEnabled;
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
    IEnumerator PauseForThreeSeconds()
    {
        yield return new WaitForSeconds(3f);
        Debug.Log("Two seconds have passed!");
    }
    IEnumerator Wait()
    {
        didShoot = true;
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
            if (didScore)
            {
                Debug.Log("OnCollision did score is true");
                //PauseAll();
                SceneSwitcher();
            }
            if (collider.Equals("EndingBlock"))
            {
                SceneSwitcher();
            }
            else
            {
                if (!isPenaltyMode)
                {
                    Debug.Log("You are dead!");
                    FailedScreen.SetActive(true);
                    pauseButton.SetActive(false);
                    PauseAll();
                    rb.constraints = RigidbodyConstraints.None;
                    rb.AddForce(new Vector3(1, 1, -1) * 100);
                    //levelChangeScript.FadeToLevel(sceneName);
                }
                else
                {
                    PauseForThreeSeconds();
                    if (!didScore)
                    {
                        Debug.Log("You are dead!");
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
                didScore = true;
            }
        if (other.gameObject.name == "Borders")
        {
            Debug.Log("You are dead!");
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
            case "StageOne":
                nextScene = "PenaltyScene";
                break;
            case "PenaltyScene":
                nextScene = "StageTwo";
                break;
            case "StageTwo":
                nextScene = "PenaltySceneTwo";
                break;
            case "PenaltySceneTwo":
                nextScene = "StageTwo";
                break;
            default:
                nextScene = "PenaltyScene";
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
