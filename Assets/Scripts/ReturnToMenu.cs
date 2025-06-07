using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToMenu : MonoBehaviour
{
    public string previousScene = "MainMenu";

    public void GoBack()
    {
        SceneManager.LoadScene(previousScene);
    }
}
