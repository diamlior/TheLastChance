using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSlide : MonoBehaviour
{
    public GameObject player;
    float slidingDistance = 13f;
    public float speed;
    public string direction;

    private bool shouldMove = false;

    void Update()
    {
        if (!shouldMove && Mathf.Abs(transform.position.z - player.transform.position.z) < slidingDistance)
        {
            shouldMove = true;
        }

        if (shouldMove)
        {
            if (direction == "left")
            {
                transform.Translate(Vector3.down * speed * Time.deltaTime);
                if (transform.position.x <= -2.3f)
                {
                    shouldMove = false;
                }
            }
            else if (direction == "right")
            {
                transform.Translate(Vector3.down * speed * Time.deltaTime);
                if (transform.position.x >= 2.3f)
                {
                    shouldMove = false;
                }
            }
        }
    }
}
