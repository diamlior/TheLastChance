using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CanvasController : MonoBehaviour
{
    public GameObject player;
    public Canvas canvas1;
    public Canvas canvas2;
    public Canvas canvas3;
    public GameObject floatingText;
    Movement player_movement;

    bool leftArrowPressed = false;
    bool rightArrowPressed = false;

    void Start()
    {
        canvas1.enabled = true;
        canvas2.enabled = false;
        canvas3.enabled = false;
        player_movement = player.GetComponent<Movement>();
    }

    void Update()
    {
        if (!player_movement.isEnabled)
            return;
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (!leftArrowPressed)
            {
                canvas1.enabled = false;
                canvas2.enabled = true;
                leftArrowPressed = true;
            }
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (leftArrowPressed && !rightArrowPressed)
            {
                canvas2.enabled = false;
                canvas3.enabled = true;
                rightArrowPressed = true;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            if (leftArrowPressed && canvas3.enabled)
            {
                canvas3.enabled = false;
                floatingText.SetActive(true);
                StartCoroutine(DeactivateAfterDelay(floatingText, 2.0f));
            }
        }
    }

    IEnumerator DeactivateAfterDelay(GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay);
        obj.SetActive(false);
    }
}
