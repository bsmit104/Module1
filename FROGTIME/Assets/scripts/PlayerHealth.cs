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
    public GameObject gameOver;
    void Start()
    {
        currentHealth = maxHealth;
        if (healthBar == null) {
            GameObject.Find("healthBar");
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
        Time.timeScale = 1f;
        died = false;
        ChangeHealth(90);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void DeadPause() {
        gameOver.SetActive(true);
        Time.timeScale = 0f;
        died = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
