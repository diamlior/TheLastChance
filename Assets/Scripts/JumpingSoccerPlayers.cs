using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class JumpingSoccerPlayers : MonoBehaviour
{
    public float jumpDistance = 13f;
    public float jumpHeight = 1f;

    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance < jumpDistance)
        {
            transform.position = new Vector3(transform.position.x, jumpHeight, transform.position.z);
        }
    }
}
