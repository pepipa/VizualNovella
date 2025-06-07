using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    public Slider volumeSlider;
    public Slider sfxSlider;

    private void Start()
    {
        if (VolumeSettings.Instance != null)
        {
            float savedVolume = VolumeSettings.Instance.GetVolume();
            volumeSlider.value = savedVolume;
            volumeSlider.onValueChanged.AddListener(OnMusicVolumeChanged);
        }
        else
        {
            Debug.LogWarning("VolumeSettings.Instance is null!");
        }
        if (SFXManager.Instance != null)
        {
            float savedSFX = SFXManager.Instance.GetSFXVolume();
            sfxSlider.value = savedSFX;
            sfxSlider.onValueChanged.AddListener(SFXManager.Instance.UpdateSFXVolume);
        }
        else
        {
            Debug.LogWarning("SFXManager.Instance is null!");
        }
    }
    private void OnMusicVolumeChanged(float value)
    {
        VolumeSettings.Instance?.UpdateVolume(value);
    }

    public void BackToPreviousScene()
    {
        int previousScene = PlayerPrefs.GetInt("PreviousScene", -1);
        if (previousScene != -1)
        {
            SceneManager.LoadScene(previousScene);
        }
        else
        {
            Debug.LogWarning("Предыдущая сцена не найдена в PlayerPrefs.");
        }
    }

    
    public void ExitToMainMenu()
    {
        GameObject music = GameObject.FindGameObjectWithTag("Music");
        if (music != null)
        {
            Destroy(music);
        }

        SceneManager.LoadScene("MainMenu"); 
    }
}
