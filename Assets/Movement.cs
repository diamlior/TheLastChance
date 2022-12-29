using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Movement : MonoBehaviour
{
    public GameObject player;
    public Rigidbody rb;
    public float jumpForce = 5;
    public float speed = 100;
    bool isGrounded = true;
    bool inMovement = false;
    float currentX, targetX;
    Renderer renderers;
    // Start is called before the first frame update
    void Start()
    {
        renderers = player.GetComponent<Renderer>();

    }
    void OnCollisionStay()
    {
        isGrounded = true;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.RightArrow) && !inMovement)
        {
            rb.AddForce(Vector3.right * speed);
            currentX = (float)System.Math.Round(player.GetComponent<Transform>().position.x);
            targetX = currentX + 1f;
            inMovement = true;
        }

        else if (Input.GetKey(KeyCode.LeftArrow) && !inMovement)
        {
            rb.AddForce(Vector3.left * speed);
            currentX = (float)System.Math.Round(player.GetComponent<Transform>().position.x);
            targetX = currentX - 1f;
            inMovement = true;
        }
        else if (inMovement && System.Math.Abs(player.GetComponent<Transform>().position.x - targetX) < 0.05f) {
            inMovement = false;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
        currentX = (float)System.Math.Round(player.GetComponent<Transform>().position.x);
    }

    void OnCollisionEnter(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            Debug.DrawRay(contact.point, contact.normal, Color.white);

        }
        if (collision.collider.name != "Floor")
        {
            renderers.material.color = new Color(0, 0, 0);
        }

    }
}
