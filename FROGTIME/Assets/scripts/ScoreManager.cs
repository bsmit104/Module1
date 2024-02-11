using UnityEngine;
using TMPro;
using System.Collections;

public class ScoreManager : MonoBehaviour
{
    public int score = 0;
    public TextMeshProUGUI scoreText;

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

        //////////////coroutine////////////
        StartCoroutine(DecreaseScoreCoroutine());
    }

    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.Space))
        // {
        //     IncreaseScore(10);
        // }
    }

////////////coroutine///////////////
    IEnumerator DecreaseScoreCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(20f); // Wait for 20 seconds.

            // Decrease the score by 1.
            //Debug.Log("lost a coin");
            DecreaseScore(1);
        }
    }
/////////////////////////////////////
    void UpdateScoreText()
    {
        Debug.Log("goood");
        scoreText.text = "Score: " + score.ToString();
    }

    public void IncreaseScore(int amount)
    {
        score += amount;
        UpdateScoreText();
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
}