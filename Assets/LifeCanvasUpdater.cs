using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine;

public class LifeCanvasUpdater : MonoBehaviour
{
    public static int lifeCount;


    void Update()
    {
        lifeCount = StaticData.getLife();
        gameObject.GetComponent<TMP_Text>().text = "" + lifeCount;
    }
}
