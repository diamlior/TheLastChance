using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CountDown : MonoBehaviour
{
    public float timeBetweenCounts = 1.5f;
    public GameObject one, two, three, go, player, optional, PreviewCanvas;
    Movement movement;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Started");
        movement = player.GetComponent<Movement>();
        movement.PauseAll();
        StartCoroutine(StartCountDown());

    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator StartCountDown()
    {
        two.SetActive(false);
        one.SetActive(false);
        go.SetActive(false);
        yield return new WaitForSeconds(timeBetweenCounts);
        
        three.SetActive(false);
        two.SetActive(true);
        yield return new WaitForSeconds(timeBetweenCounts);
        two.SetActive(false);
        one.SetActive(true);
        yield return new WaitForSeconds(timeBetweenCounts);
        one.SetActive(false);
        go.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        go.SetActive(false);
        PreviewCanvas.SetActive(false);
        movement.ReturnAll();
        if(!(optional == null))
        {
            optional.SetActive(true);
        }
    }
}
