using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public GameObject player;
    public Rigidbody rb;
    public float jumpForce = 5;
    public float movementForce = 2;
    bool isGrounded = true;
    Renderer renderer;
    // Start is called before the first frame update
    void Start()
    {
        renderer = player.GetComponent<Renderer>();

    }
    void OnCollisionStay()
    {
        isGrounded = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow))
            player.transform.position += Vector3.right * Time.deltaTime * movementForce;
        else if (Input.GetKey(KeyCode.LeftArrow))
            player.transform.position += Vector3.left * Time.deltaTime * movementForce;
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }

    }

    void OnCollisionEnter(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            Debug.DrawRay(contact.point, contact.normal, Color.white);

        }
        if (collision.collider.name != "Floor")
        {
            renderer.material.color = new Color(255, 255, 255);
        }

    }
}
