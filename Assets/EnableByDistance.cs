using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableByDistance : MonoBehaviour
{
    GameObject[] children;
    GameObject player;
    GameObject firstFan;
    GameObject lastFan;
    float minDistance = 50f;
    bool allEnabled = true;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.Log("Player GameObject has no tag 'Player'!!");
            gameObject.SetActive(false);
        }
        firstFan = transform.GetChild(0).GetChild(0).gameObject;
        lastFan = transform.GetChild(3).GetChild(3).gameObject;
        children = new GameObject[transform.childCount];
        for(int i = 0; i < transform.childCount; i++)
        {
            children[i] = transform.GetChild(i).gameObject;
        }
    }

    void setActivationToAll(bool state)
    {
        if (state == allEnabled)
            return;
        for (int i = 0; i < transform.childCount; i++)
        {
            children[i].gameObject.SetActive(state);
        }
        allEnabled = state;
    }
 
    // Update is called once per frame
    void FixedUpdate()
    {
        float playerPos = player.transform.position.z;
        float firstFanPos = firstFan.transform.position.z;
        float lastFanPos = lastFan.transform.position.z;
        float dist = firstFanPos - playerPos;
        if (playerPos > lastFanPos)
        {
            gameObject.tag = "Dead";
            GameObject.Destroy(gameObject);
        }
            
        // Activate only if distance is less than MinDistance
        setActivationToAll(dist < minDistance);
        
    }
}
