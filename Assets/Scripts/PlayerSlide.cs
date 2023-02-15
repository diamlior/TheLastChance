using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSlide : MonoBehaviour
{
    public GameObject player;
    public GameObject soccerPlayer;
    public float speed;
    public string direction;

    private bool shouldMove = false;

    void Update()
    {
        if (!shouldMove && soccerPlayer.transform.position.z < player.transform.position.z)
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
