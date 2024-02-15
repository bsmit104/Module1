//Inspo from lecture
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private Slider healthBar;
    [SerializeField] private int maxHealth = 90;
    public static bool died = false;
    private int currentHealth;
    public GameObject playerObject;
    public GameObject gameOver;

    public GameObject score;

    public GameObject win;
    private ScoreManager scoreManager;
    void Start()
    {
        currentHealth = maxHealth;
        if (healthBar == null) {
            GameObject.Find("healthBar");
        }

        scoreManager = score.GetComponent<ScoreManager>();

        if (scoreManager == null)
        {
            Debug.Log("PlayerHealth null");
        }
    }

    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.Space))
        // {
        //     ChangeHealth(-5);
        // }
    }

    public void ChangeHealth(int amount) {
        currentHealth += amount;
        if (currentHealth <= 0) {
            DeadPause();
        }
        if (currentHealth >= maxHealth) {
            currentHealth = maxHealth;
        }
        healthBar.value = currentHealth;
    }

     public void BackAlive() {
        gameOver.SetActive(false);
        win.SetActive(false);
        Time.timeScale = 1f;
        died = false;
        ChangeHealth(90);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        scoreManager.SetScore(3);
        scoreManager.stopCoroutineAfterDie();

        if (playerObject != null)
        {
            playerObject.transform.position = new Vector3(640.9f, 14.021f, 65.394f);
        }
        else {
            Debug.Log("playerObject is null");
        }
    }

    void DeadPause() {
        gameOver.SetActive(true);
        Time.timeScale = 0f;
        died = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
