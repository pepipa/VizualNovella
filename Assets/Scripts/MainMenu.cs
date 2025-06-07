using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button continueButton;
    [SerializeField] private Button newGameButton;
    private const string DialogueProgressKey = "CurrentState";
    private const string SceneIndexKey = "SavedSceneIndex";
    private const string settingsSceneName = "SettingsMenu";
    public CanvasGroup fadePanel;
    public float fadeDuration = 1.5f;

    void Start()
    {
        bool hasSave = PlayerPrefs.HasKey(DialogueProgressKey) && !string.IsNullOrEmpty(PlayerPrefs.GetString(DialogueProgressKey));
        continueButton.gameObject.SetActive(hasSave);
        newGameButton.gameObject.SetActive(true);
        GameObject chapterMusic = GameObject.Find("Music");
        if (chapterMusic != null)
        {
            Destroy(chapterMusic);
        }
    }

    public void ContinueGame()
    {
        int savedScene = PlayerPrefs.GetInt(SceneIndexKey, 3);
        StartCoroutine(FadeOutAndLoadScene(savedScene));
    }

    public void NewGame()
    {
        PlayerPrefs.DeleteKey(DialogueProgressKey);
        PlayerPrefs.DeleteKey(SceneIndexKey);
        StartCoroutine(FadeOutAndLoadScene(3));
    }

    public void Setting()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        PlayerPrefs.SetInt("PreviousScene", currentSceneIndex);

        SceneManager.LoadScene(settingsSceneName);
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
