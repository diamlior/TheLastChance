using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalKeeperNew : MonoBehaviour
{
    public float moveSpeed = 2.0f;
    public float minX = 419.3f;
    public float maxX = 424.75f;

    private bool movingRight = true;

    void Update()
    {
        Vector3 position = transform.position;

        if (movingRight)
        {
            position.x += moveSpeed * Time.deltaTime;
        }
        else
        {
            position.x -= moveSpeed * Time.deltaTime;
        }

        if (position.x > maxX)
        {
            movingRight = false;
        }
        else if (position.x < minX)
        {
            movingRight = true;
        }

        transform.position = position;
    }
}
