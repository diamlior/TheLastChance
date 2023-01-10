using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Movement : MonoBehaviour
{
    public GameObject player;
    public Rigidbody rb;
    public GameObject FailedScreen;
    public GameObject pauseButton;
    public float jumpForce = 5;
    public float speed = 100;
    public bool isGrounded = true, stopJumped = false;
    bool movingLeft = false, movingRight = false;
    float currentX, targetX, startingX;
    string sceneName;
    Transform transform;
    Animator animator;
    private LevelChangerScript levelChangeScript;
    public bool isEnabled = true;

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
        if (Input.GetKey(KeyCode.RightArrow) && !movingRight && currentX <= startingX)
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
            rb.angularVelocity = Vector3.zero;
            rb.AddForce(Vector3.right * speed);
            currentX = (float)System.Math.Round(transform.position.x);
            targetX = currentX + 1f;
            movingRight = true;
            movingLeft = false;
        }

        else if (Input.GetKey(KeyCode.LeftArrow) && !movingLeft && currentX >= startingX)
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
            rb.angularVelocity = Vector3.zero;
            rb.AddForce(Vector3.left * speed);
            currentX = (float)System.Math.Round(transform.position.x);
            targetX = currentX - 1f;
            movingLeft = true;
            movingRight = false;
        }
        else if ((movingLeft || movingRight) && System.Math.Abs(transform.position.x - targetX) < 0.05f)
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
        if (Input.GetKey(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
            stopJumped = false;
        }
        else if (!Input.GetKey(KeyCode.Space) && !isGrounded && !stopJumped)
        {
            if (rb.velocity.y > 0)
                rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            stopJumped = true;
        }
        
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
            if (collider.Equals("EndingBlock"))
            {
                string nextScene = "";
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
                Debug.Log("next scene: " + nextScene);
                levelChangeScript.FadeToLevel(nextScene);
            }
            else
            {
                Debug.Log("You are dead!");
                FailedScreen.SetActive(true);
                pauseButton.SetActive(false);
                PauseAll();
                //levelChangeScript.FadeToLevel(sceneName);
            }

        }

    }

    public void PauseAll()
    {
        GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
        foreach (GameObject obs in obstacles) {
            obs.GetComponent<Obstacle>().PauseObject();
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
        isEnabled = true;
        rb.constraints = RigidbodyConstraints.None;
    }

}
