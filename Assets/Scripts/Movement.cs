using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Movement : MonoBehaviour
{
    public GameObject player;
    public Rigidbody rb;
    public GameObject FailedScreen, DefeatScreen, VictoryScreen;
    public GameObject pauseButton;
    public AudioSource coinFX, goalFX;
    public float jumpForce = 5;
    public float speed = 100;
    private bool isGrounded = true, stopJumped = false, isPenaltyMode = false, isShoot = false;
    bool movingLeft = false, movingRight = false;
    float currentX, targetX, startingX;

    string sceneName;
    Animator animator;
    private LevelChangerScript levelChangeScript;
    public bool isEnabled = true;

    //Penalty Vairables
    public float Force;
    public Transform target;
    public Slider forceUI;
    public GameObject Gauage;
    public GameObject GoalCanvas;
    public GameObject GoalAndExtraLifeCanvas;
    GameObject goalObject;
    public bool didScore = false;
    bool shouldStartPenaltyMode = false;
    Boolean backToBase;
    Vector3 startPos;
    int initialLife;
    int initialCoins;
    Vector3 GoalPos;
    bool failed;
    private AudioSource DeathAudio, GainingStarAudio;


    //public Canvas goalMsgCanvas;

    // Start is called before the first frame update
    void Start()
    {
        initialCoins = StaticData.getCoins();
        goalObject = GameObject.FindGameObjectWithTag("Goal");
        backToBase = false;
        startingX = transform.position.x;
        currentX = (float)System.Math.Round(transform.position.x);
        sceneName = SceneManager.GetActiveScene().name;
        levelChangeScript = GameObject.Find("LevelChanger").GetComponent<LevelChangerScript>();
        animator = player.GetComponent<Animator>();
        startPos = transform.position;
        initialLife = StaticData.getLife();
        failed = false;
        DeathAudio = GameObject.Find("DeathAudio").GetComponent<AudioSource>();
        GainingStarAudio = GameObject.Find("GainingStarAudio").GetComponent<AudioSource>();

    }
    void OnCollisionStay()
    {
        isGrounded = true;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        animator.enabled = isEnabled;
        /*
        if (didScore)
        {
            float transitionSpeed = 20f;
            transform.position = Vector3.Lerp(transform.position, startPos, Time.deltaTime * transitionSpeed);
            if (Vector3.Distance(transform.position,startPos) < 0.5f)
            {
                backToBase = true;
                transform.position = startPos;
                SceneSwitcher();
            }
                
        }
        */
        if (!isEnabled)
            return;
        if (isGrounded && shouldStartPenaltyMode && !Input.GetKey(KeyCode.Space))
        {
            shouldStartPenaltyMode = false;
            isPenaltyMode = true;
            PenaltyMode();
        }
        if (Input.GetKey(KeyCode.RightArrow) && !movingRight && currentX <= startingX && !isShoot)
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
            rb.angularVelocity = Vector3.zero;
            rb.AddForce(Vector3.right * speed);
            currentX = (float)System.Math.Round(transform.position.x);
            targetX = currentX + 1f;
            movingRight = true;
            movingLeft = false;
        }

        else if (Input.GetKey(KeyCode.LeftArrow) && !movingLeft && currentX >= startingX && !isShoot)
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
            rb.angularVelocity = Vector3.zero;
            rb.AddForce(Vector3.left * speed);
            currentX = (float)System.Math.Round(transform.position.x);
            targetX = currentX - 1f;
            movingLeft = true;
            movingRight = false;
        }
        else if ((movingLeft || movingRight) && System.Math.Abs(transform.position.x - targetX) < 0.05f && !isShoot)
        {
            movingRight = false;
            movingLeft = false;
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
            rb.angularVelocity = Vector3.zero;
            currentX = (float)System.Math.Round(transform.position.x);
            float y = transform.position.y;
            float z = transform.position.z;
            player.GetComponent<Transform>().position = new Vector3(currentX, y, z);
        }

        else if (Input.GetKeyUp(KeyCode.Space) && isPenaltyMode && !isShoot) //shoot
        {
            shoot();
            //StartCoroutine(Wait());
        }


        else if (Input.GetKey(KeyCode.Space) && isGrounded && !isShoot)
        {
            if (!isPenaltyMode)
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                isGrounded = false;
                stopJumped = false;
            }
            else
            {
                Force++;
                slider();
            }
        }       
        
    }
    void shoot()
    {
        isShoot = true;
        Vector3 Shoot = (target.position - this.transform.position).normalized;
        GetComponent<Rigidbody>().AddForce(Shoot * Force + new Vector3(0, 3f, 0), ForceMode.Impulse);
        
    }
    IEnumerator PauseTwoSecExecuteGoalSwitch()
    {
        yield return new WaitForSeconds(2f);
        Debug.Log("Two seconds have passed!");

        SceneSwitcher();
    }

    IEnumerator Wait()
    {
        shoot();
        yield return new WaitForSeconds(1.5f);
        yield return new WaitForSeconds(1f);

        if (!didScore)
        {
            FailedScreen.SetActive(true);
            pauseButton.SetActive(false);
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        }
    }

    public void slider()
    {
        forceUI.value = Force;
    }

    public void ResetGauge()
    {
        Force = 0;
        forceUI.value = 0;
    }      
    
    void OnCollisionEnter(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            Debug.DrawRay(contact.point, contact.normal, Color.white);

        }
        string collider = collision.collider.name;
        if (collider != "Floor")
        {
            //if (didScore && backToBase)
            if (didScore)
            {
                Debug.Log("OnCollision did score is true");
                
            }
            else if (collider.Equals("EndingBlock") && backToBase)
            {
                SceneSwitcher();
            }
            else
            {
                if (!isPenaltyMode && !didScore)
                {
                    failure();
                }
                else
                {
                    if (!didScore)
                    {
                        failure();
                    }
                }

            }

        }

    }
    /*
    void failureOld()
    {
        if (!failed) // This is needed because failure can be triggered several times for the same round.
        {
            failed = true;
            Debug.Log("You are dead!");
            StaticData.setLife(initialLife - 1);
            string textOnFailedScreen = "";
            FailedScreen.SetActive(true);

            if (StaticData.getLife() == 0)
            {
                textOnFailedScreen += "YOU'RE OUT!";

                FailedScreen.transform.GetChild(0).gameObject.SetActive(false);
                int highscore = StaticData.getHighscore();
                int coins = StaticData.getCoins();
                if (coins > highscore)
                {
                    Debug.Log("Got into highscore");
                    textOnFailedScreen += "\nWell done! New Highscore!";
                    StaticData.setHighscore(coins);
                    Debug.Log(textOnFailedScreen);
                }
                Debug.Log(textOnFailedScreen);
            }
            else
            {
                int life = StaticData.getLife();
                
                if (life > 1)
                    textOnFailedScreen = String.Format("FAILED!\nYou still have {0} tries left!", StaticData.getLife());
                else
                    textOnFailedScreen = "FAILED!\nYou still have 1 try left!";
                
                resetCoins();
            }
            Debug.Log(textOnFailedScreen);
            
            TMP_Text failText = FailedScreen.transform.GetChild(3).GetComponent<TMP_Text>();
            failText.text = textOnFailedScreen;
            
            pauseButton.SetActive(false);
            PauseAll();
            rb.constraints = RigidbodyConstraints.None;
            rb.AddForce(new Vector3(1, 1, -1) * 100);
        }
    }
    */

    void failure()
    {
        if (!failed) // This is needed because failure can be triggered several times for the same round.
        {
            failed = true;
            Debug.Log("You are dead!");
            StaticData.setLife(initialLife - 1);

            if (StaticData.getLife() == 0)
            {
                DefeatScreen.SetActive(true);
                DeathAudio.Play();
                showStars(DefeatScreen);
                int highscore = StaticData.getHighscore();
                int coins = StaticData.getCoins();
                if (coins > highscore)
                {
                    Debug.Log("Got into highscore");
                    StaticData.setHighscore(coins);
                }
            }
            else
            {
                FailedScreen.SetActive(true);
                DeathAudio.Play();
                showStars(FailedScreen);
                int life = StaticData.getLife();
                //resetCoins();
            }
            pauseButton.SetActive(false);
            PauseAll();
            rb.constraints = RigidbodyConstraints.None;
            rb.AddForce(new Vector3(1, 1, -1) * 100);
        }
    }

    IEnumerator Wait05Second()
    {
        yield return new WaitForSeconds(0.5f);
    }
    private void showStars(GameObject canvas)
    {
        Transform Star1 = canvas.transform.GetChild(0).transform.Find("Full Star");
        Transform Star2 = canvas.transform.GetChild(0).transform.Find("Full Star (1)");
        Transform Star3 = canvas.transform.GetChild(0).transform.Find("Full Star (2)");
        switch (sceneName)
        {
            case "Tutorial":
                StartCoroutine(showStarsTutorial(canvas, Star1, Star2, Star3));
                break;
            case "StageOne":
                StartCoroutine(showStarsStageOne(canvas, Star1, Star2, Star3));
                break;
            case "StageTwo":
                StartCoroutine(showStarsStageTwo(canvas, Star1, Star2, Star3));
                break;
            case "StageThree":
                StartCoroutine(showStarsStageThree(canvas, Star1, Star2, Star3));
                break;
            case "StageFour":
                StartCoroutine(showStarsStageFour(canvas, Star1, Star2, Star3));
                break;
            default:
                break;
        }
    }
    IEnumerator showStarsTutorial(GameObject canvas, Transform Star1, Transform Star2, Transform Star3)
    {

        yield return new WaitForSeconds(1f);
        yield return new WaitForSeconds(0.5f);
        Star1.gameObject.SetActive(true);
        GainingStarAudio.Play();
        yield return new WaitForSeconds(0.5f);
        Star2.gameObject.SetActive(true);
        GainingStarAudio.Play();
        yield return new WaitForSeconds(0.5f);
        Star3.gameObject.SetActive(true);
        GainingStarAudio.Play();
    }
    IEnumerator showStarsStageOne(GameObject canvas, Transform Star1, Transform Star2, Transform Star3)
    {
        // max 28 coins
        yield return new WaitForSeconds(1f);
        int currentCoins = StaticData.getCoins();
        if (currentCoins >= 7 && currentCoins < 14)
        {
            yield return new WaitForSeconds(0.5f);
            Star1.gameObject.SetActive(true);
            GainingStarAudio.Play();
        }
        else if (currentCoins >= 14 && currentCoins < 21)
        {
            Star1.gameObject.SetActive(true);
            GainingStarAudio.Play();
            yield return new WaitForSeconds(0.5f);
            Star2.gameObject.SetActive(true);
            GainingStarAudio.Play();
        }
        else if (currentCoins >= 21)
        {
            Star1.gameObject.SetActive(true);
            GainingStarAudio.Play();
            yield return new WaitForSeconds(0.5f);
            Star2.gameObject.SetActive(true);
            GainingStarAudio.Play();
            yield return new WaitForSeconds(0.5f);
            Star3.gameObject.SetActive(true);
            GainingStarAudio.Play();
        }
    }

    IEnumerator showStarsStageTwo(GameObject canvas, Transform Star1, Transform Star2, Transform Star3)
    {
        // max 38 coins
        yield return new WaitForSeconds(1f);
        int currentCoins = StaticData.getCoins();
        if (currentCoins >= 9 && currentCoins < 18)
        {
            yield return new WaitForSeconds(0.5f);
            Star1.gameObject.SetActive(true);
            GainingStarAudio.Play();
        }
        else if (currentCoins >= 18 && currentCoins < 27)
        {
            Star1.gameObject.SetActive(true);
            GainingStarAudio.Play();
            yield return new WaitForSeconds(0.5f);
            Star2.gameObject.SetActive(true);
            GainingStarAudio.Play();
        }
        else if (currentCoins >= 27)
        {
            Star1.gameObject.SetActive(true);
            GainingStarAudio.Play();
            yield return new WaitForSeconds(0.5f);
            Star2.gameObject.SetActive(true);
            GainingStarAudio.Play();
            yield return new WaitForSeconds(0.5f);
            Star3.gameObject.SetActive(true);
            GainingStarAudio.Play();
        }
    }
    IEnumerator showStarsStageThree(GameObject canvas, Transform Star1, Transform Star2, Transform Star3)
    {
        yield return new WaitForSeconds(1f);
        int currentCoins = StaticData.getCoins();
        if (currentCoins >= 9 && currentCoins < 18)
        {
            yield return new WaitForSeconds(0.5f);
            Star1.gameObject.SetActive(true);
            GainingStarAudio.Play();
        }
        else if (currentCoins >= 18 && currentCoins < 27)
        {
            Star1.gameObject.SetActive(true);
            GainingStarAudio.Play();
            yield return new WaitForSeconds(0.5f);
            Star2.gameObject.SetActive(true);
            GainingStarAudio.Play();
        }
        else if (currentCoins >= 27)
        {
            Star1.gameObject.SetActive(true);
            GainingStarAudio.Play();
            yield return new WaitForSeconds(0.5f);
            Star2.gameObject.SetActive(true);
            GainingStarAudio.Play();
            yield return new WaitForSeconds(0.5f);
            Star3.gameObject.SetActive(true);
            GainingStarAudio.Play();
        }

    }
    IEnumerator showStarsStageFour(GameObject canvas, Transform Star1, Transform Star2, Transform Star3)
    {
        yield return new WaitForSeconds(1f);
        int currentCoins = StaticData.getCoins();
        if (currentCoins >= 9 && currentCoins < 18)
        {
            yield return new WaitForSeconds(0.5f);
            Star1.gameObject.SetActive(true);
            GainingStarAudio.Play();
        }
        else if (currentCoins >= 18 && currentCoins < 27)
        {
            Star1.gameObject.SetActive(true);
            GainingStarAudio.Play();
            yield return new WaitForSeconds(0.5f);
            Star2.gameObject.SetActive(true);
            GainingStarAudio.Play();
        }
        else if (currentCoins >= 27)
        {
            Star1.gameObject.SetActive(true);
            GainingStarAudio.Play();
            yield return new WaitForSeconds(0.5f);
            Star2.gameObject.SetActive(true);
            GainingStarAudio.Play();
            yield return new WaitForSeconds(0.5f);
            Star3.gameObject.SetActive(true);
            GainingStarAudio.Play();
        }

    }

    public void resetCoins()
    {
        StaticData.setCoins(initialCoins);
    }
    void OnTriggerEnter(Collider other)
    {
        if (didScore)
            return;
        else if (other.gameObject.name == "EndRunBlock")
        {
            shouldStartPenaltyMode = true;
        }
        if (other.gameObject.name == "GoalBlock" || other.gameObject.name == "GoalTarget")
        {
            
            if (other.gameObject.name == "GoalTarget")
            {
                other.gameObject.SetActive(false);
                Debug.Log("Hit Target");
                //GoalAndExtraLifeCanvas.SetActive(true);
                goalFX.Play();
                VictoryScreen.SetActive(true);
                showStars(VictoryScreen);
                StaticData.setLife(initialLife + 1);
                didScore = true;
            }
            else
            {
                Debug.Log("Hit Goal");
                goalFX.Play();
                //GoalCanvas.SetActive(true);
                VictoryScreen.SetActive(true);
                showStars(VictoryScreen);

                didScore = true;
            }
            Debug.Log(other.name + " Triggered");
            PauseAllNotTotal();
            //StartCoroutine(PauseTwoSecExecuteGoalSwitch());
            
            //goalObject.SetActive(false);
        }
        else if (other.gameObject.name == "BordersDown")
        {
            failure();
        }

    }

    void SceneSwitcher()
    {
        string nextScene = "";;
        switch (sceneName)
        {
            case "Tutorial":
                nextScene = "MainMenu";
                break;
            case "StageOne":
                nextScene = "StageTwo";
                break;
            case "StageTwo":
                nextScene = "MainMenu";
                break;
            case "StageThree":
                nextScene = "StageFour";
                break;
            case "StageFour":
                nextScene = "MainMenu";
                break;
            default:
                nextScene = "MainMenu";
                break;
        }
        Debug.Log("next scene: " + nextScene);
        levelChangeScript.FadeToLevel(nextScene);
    }
    

    public void PenaltyMode()
    {
        Gauage.SetActive(true);
    }

    public void PauseAll()
    {
        GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
        try
        {
            foreach (GameObject obs in obstacles)
            {
                Obstacle obstacle = obs.GetComponent<Obstacle>();
                if (obstacle != null)
                    obstacle.PauseObject();
                else
                    Debug.Log(obs.name);
            }
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }

        isEnabled = false;

        rb.constraints = RigidbodyConstraints.FreezePosition;
    }

    public void PauseAllNotTotal()
    {
        GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
        try
        {
            foreach (GameObject obs in obstacles)
            {
                Obstacle obstacle = obs.GetComponent<Obstacle>();
                if (obstacle != null)
                    obstacle.PauseObject();
                else
                    Debug.Log(obs.name);
            }
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }

        isEnabled = false;
        //rb.constraints = RigidbodyConstraints.FreezePosition;
    }
    public void ReturnAll()
    {

        GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
        foreach (GameObject obs in obstacles)
        {
            obs.GetComponent<Obstacle>().ReturnObject();
        }
        rb.constraints = RigidbodyConstraints.None;
        isEnabled = true;
    }    
}
