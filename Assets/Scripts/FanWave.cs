using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanWave : MonoBehaviour
{
    // The maximum distance the fans will move up and down.
    public float maxDistance = 1.0f;

    // The speed at which the fans will move up and down.
    public float speed = 1.0f;

    // An array to store references to all of the child fan objects.
    private Transform[] fanObjects;

    void Start()
    {
        // Get references to all of the child fan objects.
        int numFans = transform.childCount;
        fanObjects = new Transform[numFans];
        for (int i = 0; i < numFans; i++)
        {
            fanObjects[i] = transform.GetChild(i);
        }
    }

    void Update()
    {
        // Loop through all of the child fan objects and move them in a wave-like motion.
        foreach (Transform fan in fanObjects)
        {
            // Calculate the new Y position for the fan using a sine wave.
            float newY = fan.localPosition.y + Mathf.Sin(Time.time * speed) * maxDistance;

            // Set the fan's local position to the new Y position.
            fan.localPosition = new Vector3(fan.localPosition.x, newY, fan.localPosition.z);
        }
    }
}
