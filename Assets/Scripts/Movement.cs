using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Movement : MonoBehaviour
{
    public GameObject player;
    public Rigidbody rb;
    
    public float jumpForce = 5;
    public float speed = 100;
    public bool isGrounded = true, stopJumped = false;
    bool movingLeft = false, movingRight = false;
    float currentX, targetX, startingX;
    string sceneName;
    Renderer renderers;
    Transform transform;
    private LevelChangerScript levelChangeScript;
    // Start is called before the first frame update
    void Start()
    {
        renderers = player.GetComponent<Renderer>();
        transform = player.GetComponent<Transform>();
        startingX = transform.position.x;
        sceneName = SceneManager.GetActiveScene().name;
        levelChangeScript = GameObject.Find("LevelChanger").GetComponent<LevelChangerScript>();
    }
    void OnCollisionStay()
    {
        isGrounded = true;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
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
        else if ((movingLeft || movingRight) && System.Math.Abs(transform.position.x - targetX) < 0.05f) {
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
                    case "SampleScene":
                        nextScene = "PenaltyScene";
                        break;
                    case "PenaltyScene":
                        nextScene = "SampleScene";
                        break;
                    default:
                        nextScene = "PenaltyScene";
                        break;
                }
                levelChangeScript.FadeToLevel(nextScene);
            }
            else
            {
                Debug.Log("You are dead!");
                levelChangeScript.FadeToLevel(sceneName);
            }

        }

    }
}
