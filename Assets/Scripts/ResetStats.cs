using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResetStats : MonoBehaviour
{
    public TMP_Text highscore;
    // Start is called before the first frame update
    void Start()
    {
        StaticData.resetStats();
        highscore.text = "Highscore: " + StaticData.getHighscore();
    }

    
}
