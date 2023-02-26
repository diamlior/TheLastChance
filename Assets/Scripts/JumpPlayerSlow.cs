using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPlayerSlow : MonoBehaviour
{
    public float jumpDistance = 13f;
    public float jumpHeight = 1f;
    public float jumpSpeed = 2f; // Default jump speed

    private Transform player;
    private bool isJumping = false;
    private Vector3 originalPosition;
    private float startTime;
    private float journeyLength;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        originalPosition = transform.position;
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance < jumpDistance && !isJumping)
        {
            StartJump();
        }

        if (isJumping)
        {
            float distCovered = (Time.time - startTime) * jumpSpeed;
            float fracJourney = distCovered / journeyLength;
            transform.position = Vector3.Lerp(originalPosition, new Vector3(originalPosition.x, jumpHeight, originalPosition.z), fracJourney);

            if (fracJourney >= 1f)
            {
                EndJump();
            }
        }
    }

    private void StartJump()
    {
        isJumping = true;
        startTime = Time.time;
        journeyLength = Vector3.Distance(originalPosition, new Vector3(originalPosition.x, jumpHeight, originalPosition.z));
    }

    private void EndJump()
    {
        isJumping = false;
        originalPosition = transform.position;
    }
}






