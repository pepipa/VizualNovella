using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenSettingsButton : MonoBehaviour
{
    public void OpenSettings()
    {
        StartCoroutine(OpenSettingsDelayed());
    }

    private IEnumerator OpenSettingsDelayed()
    {
        PlayerPrefs.SetInt("PreviousScene", SceneManager.GetActiveScene().buildIndex);

        yield return new WaitForEndOfFrame();

        SceneManager.LoadScene("Settings");
    }
}
