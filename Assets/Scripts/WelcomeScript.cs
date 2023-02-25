using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WelcomeScript : MonoBehaviour
{
    public float timeBetweenCounts = 2f;
    public GameObject player;
    public GameObject Welcome, LastChance, Selected, Arrows, Space, Score, Aim, HoldSpace, GoodLuck, TutTextCanvas, pauseButton;
    Movement movement;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Started");
        movement = player.GetComponent<Movement>();
        movement.PauseAll();
        StartCoroutine(StartText());
    }

    IEnumerator StartText()
    {
        yield return new WaitForSeconds(3f);
        Welcome.SetActive(false);
        LastChance.SetActive(true);
        yield return new WaitForSeconds(timeBetweenCounts);
        LastChance.SetActive(false);
        Selected.SetActive(true);
        yield return new WaitForSeconds(timeBetweenCounts);
        Selected.SetActive(false);
        Arrows.SetActive(true);
        yield return new WaitForSeconds(timeBetweenCounts);
        Arrows.SetActive(false);
        Space.SetActive(true);
        yield return new WaitForSeconds(timeBetweenCounts);
        Space.SetActive(false);
        Score.SetActive(true);
        yield return new WaitForSeconds(timeBetweenCounts);
        Score.SetActive(false);
        Aim.SetActive(true);
        yield return new WaitForSeconds(timeBetweenCounts);
        Aim.SetActive(false);
        HoldSpace.SetActive(true);
        yield return new WaitForSeconds(timeBetweenCounts);
        HoldSpace.SetActive(false);
        GoodLuck.SetActive(true);
        yield return new WaitForSeconds(3f);
        TutTextCanvas.SetActive(false);
        pauseButton.SetActive(true);
    }
}
