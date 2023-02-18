using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingCrowd : MonoBehaviour
{
    public GameObject[] fans;
    public float[] jumpForces;
    
    private Rigidbody[] fanRigidbodies;
    
    void Start()
    {
        fanRigidbodies = new Rigidbody[fans.Length];
        for (int i = 0; i < fans.Length; i++)
        {
            fanRigidbodies[i] = fans[i].GetComponent<Rigidbody>();
            fanRigidbodies[i].AddForce(Vector3.up * jumpForces[i], ForceMode.Impulse);
        }
    }
}
