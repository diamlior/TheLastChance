using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanMovement : MonoBehaviour
{
    // The maximum distance the fans will move up and down.
    public float maxDistance = 1.0f;

    // The speed at which the fans will move up and down.
    public float speed = 1.0f;

    private List<Transform> fanTransforms = new List<Transform>();
    private List<float> fanInitialYPositions = new List<float>();

    void Start()
    {
        // Get the transforms of all the fan objects.
        fanTransforms = new List<Transform>(transform.GetComponentsInChildren<Transform>());

        // Remove the transform of the parent object from the list.
        fanTransforms.Remove(transform);

        // Store the initial Y positions of each fan so we can use them as reference points for movement.
        foreach (Transform fanTransform in fanTransforms)
        {
            fanInitialYPositions.Add(fanTransform.position.y);
        }
    }

    void FixedUpdate()
    {
        {// Calculate the new Y positions for each fan using a sine wave.
            for (int i = 0; i < fanTransforms.Count; i++)
            {
                Transform fanTransform = fanTransforms[i];
                if (fanTransform == null)
                    continue;
                float fanInitialY = fanInitialYPositions[i];
                float newY = fanInitialY + Mathf.Sin(Time.time * speed) * maxDistance;

                // Set the fan's position to the new Y position.
                try { fanTransform.position = new Vector3(fanTransform.position.x, newY, fanTransform.position.z); }
                catch { }
                
            }
        }
        

        
        
    }
}
