using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaiseText : MonoBehaviour
{
    Vector3 initialLoc;
    private void Start()
    {
        initialLoc = transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + Vector3.up * Time.deltaTime * 3f;

        if (Vector3.Distance(initialLoc, transform.position) > 11f)
            GameObject.Destroy(gameObject);
    }
}
