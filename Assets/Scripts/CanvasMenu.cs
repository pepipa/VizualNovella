using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasMenu : MonoBehaviour
{
    private const string DialogueProgressKey = "CurrentState";
    private const string SceneIndexKey = "SavedSceneIndex";
    private const string previousScene = "Settings";
    public CanvasGroup fadePanel;
    public float fadeDuration = 1.5f;


    void Start()
    {
        bool hasSave = PlayerPrefs.HasKey(DialogueProgressKey) && !string.IsNullOrEmpty(PlayerPrefs.GetString(DialogueProgressKey));
    }

    public void ContinueGame()
    {
        int savedScene = PlayerPrefs.GetInt(SceneIndexKey, 2);
        StartCoroutine(FadeOutAndLoadScene(savedScene));
    }

    public void NewGame()
    {
        PlayerPrefs.DeleteKey(DialogueProgressKey);
        PlayerPrefs.DeleteKey(SceneIndexKey);
        StartCoroutine(FadeOutAndLoadScene(2));
    }

    public void Setting()
    {
        SceneManager.LoadScene(previousScene);
    }

    public void ExitGame()
    {
        Debug.Log("Выход из игры");
        Application.Quit();
    }

    private IEnumerator FadeOutAndLoadScene(int sceneIndex)
    {
        float timer = 0f;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float progress = timer / fadeDuration;

            if (fadePanel != null)
                fadePanel.alpha = progress;



            yield return null;
        }

        if (fadePanel != null) fadePanel.alpha = 1f;

        GameObject music = GameObject.FindGameObjectWithTag("Music");
        if (music != null)
        {
            Destroy(music);
        }

        SceneManager.LoadScene(sceneIndex);
    }

}
