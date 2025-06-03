    using System.Collections;
    using System.Collections.Generic;
    using TMPro;
    using UnityEngine;
    using UnityEngine.SceneManagement;

    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI startText;
        private const string DialogueProgressKey = "CurrentState";
        void Start()
        {
            startText.text = PlayerPrefs.GetString(DialogueProgressKey) != "" ? "Continue" : "Start";
        }

        public void SceneChange(int indexScene)
        {
            SceneManager.LoadScene(indexScene);
        }

        public void ExitGame()
        {
            Debug.Log("Выход из игры");
            Application.Quit();
        }
    }
