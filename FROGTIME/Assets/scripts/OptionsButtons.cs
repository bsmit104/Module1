//Inspo from Hoosom for vol slider https://www.youtube.com/watch?v=yWCHaTwVblk&ab_channel=Hooson
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OptionsButtons : MonoBehaviour
{
    public Slider volumeSlider;
    public Toggle fullscreenToggle;

    void Start()
    {
        if (!PlayerPrefs.HasKey("Volume"))
        {
            PlayerPrefs.SetFloat("Volume", 1);
        }

        if (!PlayerPrefs.HasKey("Fullscreen"))
        {
            PlayerPrefs.SetInt("Fullscreen", 1);
        }

        LoadOptions();
    }

    public void SaveOptions()
    {
        PlayerPrefs.SetFloat("Volume", volumeSlider.value);
        PlayerPrefs.SetInt("Fullscreen", fullscreenToggle.isOn ? 1 : 0);
    }

    public void LoadOptions()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("Volume", 1.0f);
        fullscreenToggle.isOn = PlayerPrefs.GetInt("Fullscreen", 1) == 1;
    }

    public void ChangeVolume()
    {

        AudioListener.volume = volumeSlider.value;
        SaveOptions();
    }

    public void ChangeScreen()
    {
        Screen.fullScreen = fullscreenToggle.isOn;
        SaveOptions();
    }

    public void ToMenu() {
        SceneManager.LoadScene("MainMenu");
    }
}