using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WelcomeScript : MonoBehaviour
{
    public float timeBetweenCounts = 2f;
    public GameObject player, BackButton;
    public GameObject Welcome, LastChance, Selected, Arrows, Space, Score, Aim, HoldSpace, GoodLuck, TutTextCanvas, pauseButton;
    Movement movement;

    private int textCounter = 0;
    GameObject[] texts = new GameObject[9];

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Started");
        movement = player.GetComponent<Movement>();
        movement.PauseAll();
        //StartCoroutine(StartText());
        texts[0] = Welcome;
        texts[1] = LastChance;
        texts[2] = Selected;
        texts[3] = Arrows;
        texts[4] = Space;
        texts[5] = Score;
        texts[6] = Aim;
        texts[7] = HoldSpace;
        texts[8] = GoodLuck;
    }

    
public void NextText()
    {
        if(textCounter == 8)
        {
            if (texts[textCounter] != null)
            {
                TutTextCanvas.SetActive(false);
                pauseButton.SetActive(true);
                movement.ReturnAll();
            }
        }
        else if(texts[textCounter] != null)
        {
            texts[textCounter].SetActive(false);
            textCounter++;
            texts[textCounter].SetActive(true);       
            if (textCounter != 0)
            {
                BackButton.SetActive(true);
            }
        }
        Debug.Log("" + textCounter);
    }

    public void BackText()
    {
        if (texts[textCounter] != null)
        {            
            texts[textCounter].SetActive(false);
            textCounter--;
            texts[textCounter].SetActive(true);
            if (textCounter == 0)
            {
                BackButton.SetActive(false);
            }
        }
        Debug.Log("" + textCounter);
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
        movement.ReturnAll();
    }
}
