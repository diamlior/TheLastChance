using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CollactableControl : MonoBehaviour
{
    public static int coinCount;
    public GameObject coinCountDisplay;

    void Start()
    {
        coinCount = 0;
    }

    void Update()
    {
        coinCountDisplay.GetComponent<TMP_Text>().text = "" + coinCount;
    }
}