using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToMenu : MonoBehaviour
{
    public string previousScene = "MainMenu";

    public void GoBack()
    {
        GameObject music = GameObject.FindGameObjectWithTag("Music");
        if (music != null)
        {
            Destroy(music);
        }

        SceneManager.LoadScene(previousScene);
    }
}

