using UnityEngine;
using TMPro;
using System.Collections;

public class ScoreManager : MonoBehaviour
{
    public int score = 3;
    public TextMeshProUGUI scoreText;

    public GameObject health;
    private PlayerHealth playerHealth;

    public GameObject win;

    bool alreadystarted = true;

    void Start()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
        if (scoreText == null)
        {
            Debug.Log("null");
        }
        else
        {
            UpdateScoreText();
        }

        playerHealth = health.GetComponent<PlayerHealth>();

        if (playerHealth == null)
        {
            Debug.LogError("PlayerHealth component not found on the player GameObject.");
        }

        //////////////coroutine//////////// 
        StartCoroutine(DecreaseScoreCoroutine());
    }

    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.Space))
        // {
        //     IncreaseScore(10);
        // if (score <= 0 && alreadystarted)
        // {
        //     StartCoroutine(StartKill());
        //     alreadystarted = false;
        // }
        // else if (score > 0 && !alreadystarted)
        // {
        //     StopCoroutine(StartKill());
        //     alreadystarted = true;
        // }
        // // }
        if (score <= 0) 
        {
            if (alreadystarted) {
                StartCoroutine(StartKill());
            }
            alreadystarted = false;
        }
        else {
            alreadystarted = true;
            StopCoroutine(StartKill());
        }
    }

    ////////////coroutine///////////////
    IEnumerator DecreaseScoreCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f); // 20 seconds is a good speed but less is for gameplay.

            // Decrease the score by 1.
            //Debug.Log("lost a coin");
            DecreaseScore(1);
        }
    }
    /////////////////////////////////////
    void UpdateScoreText()
    {
        Debug.Log("goood");
        scoreText.text = "Eat or die. 20 to win    Hunger: " + score.ToString();
    }

    public void stopCoroutineAfterDie()
    {
        StopCoroutine(StartKill());
    }

    public void IncreaseScore(int amount)
    {
        score += amount;
        UpdateScoreText();
        if (score >= 20) {
            WinPause();
        }
    }

    void WinPause() {
        win.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void DecreaseScore(int amount)
    {
        score -= amount;
        UpdateScoreText();
    }

    public void SetScore(int newScore)
    {
        score = newScore;
        UpdateScoreText();
    }

///coroutine///
    IEnumerator StartKill()
    {
        while (score <= 0)
        {
            yield return new WaitForSeconds(1f); // 20 seconds is a good speed but less is for gameplay.

            // Decrease the score by 1.
            //Debug.Log("lost a coin");
            if (playerHealth != null)
            {
                playerHealth.ChangeHealth(-10);
            }
            else
            {
                Debug.Log("PlayerHealth is null");
            }
        }
    }
}