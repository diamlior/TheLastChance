using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelChangerScript : MonoBehaviour
{
    public Animator animator;
    string nextScene = "";
    public void FadeToLevel (string level)
    {
        nextScene = level;
        if (!nextScene.Equals(""))
            SceneManager.LoadScene(nextScene);
        //animator.SetTrigger("FadeOut");
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadScene(nextScene);
    }
}
