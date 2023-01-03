using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBall : MonoBehaviour
{
    // The force to apply to the ball when shooting it
    public float force = 500f;

    // The ball object
    public GameObject Ball;

    // Update is called once per frame
    void Update()
    {
        // Check if the player pressed the forward arrow key
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Debug.Log("Update called");
            // Get the length of time the player held down the forward arrow key
            float holdTime = Input.GetAxis("Fire1");

            // Scale the force based on how long the player held down the forward arrow key
            float scaledForce = force * holdTime;

            // Add force to the ball in the forward direction
            Ball.GetComponent<Rigidbody>().AddForce(transform.forward * scaledForce);
        }
    }
}