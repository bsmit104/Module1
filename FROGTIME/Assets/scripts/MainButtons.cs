using UnityEngine;
using UnityEngine.SceneManagement;

public class MainButtons : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Gameplay"); // Replace "GameScene" with the actual scene name for your game.
    }

    public void OpenOptions()
    {
        SceneManager.LoadScene("Options"); // Replace "OptionsScene" with the scene name for your options menu.
    }

    public void OpenCredits()
    {
        SceneManager.LoadScene("Credits"); // Replace "CreditsScene" with the scene name for your credits menu.
    }
}