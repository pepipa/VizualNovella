using System.Collections;
using System.Collections.Generic;
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

    void Start()
    {
        bool hasSave = PlayerPrefs.HasKey(DialogueProgressKey) && !string.IsNullOrEmpty(PlayerPrefs.GetString(DialogueProgressKey));
        continueButton.gameObject.SetActive(hasSave);
        newGameButton.gameObject.SetActive(true);
    }

    public void ContinueGame()
    {
        int savedScene = PlayerPrefs.GetInt(SceneIndexKey, 1);
        SceneManager.LoadScene(savedScene);
    }

    public void NewGame()
    {
        PlayerPrefs.DeleteKey(DialogueProgressKey);
        PlayerPrefs.DeleteKey(SceneIndexKey);
        SceneManager.LoadScene(1); 
    }

    public void ExitGame()
    {
        Debug.Log("Выход из игры");
        Application.Quit();
    }
}
