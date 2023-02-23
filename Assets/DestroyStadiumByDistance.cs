using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyStadiumByDistance : MonoBehaviour
{
    GameObject player;
    float destroyingDistance = 30f;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.Log("Player GameObject has no tag 'Player'!!");
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (player.transform.position.z - transform.position.z > destroyingDistance)
            GameObject.Destroy(gameObject);
    }
}
