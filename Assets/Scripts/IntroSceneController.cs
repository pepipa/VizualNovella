using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroSceneController : MonoBehaviour
{
    public CanvasGroup textGroup;
    public float fadeDuration = 2f;
    public float displayDuration = 2f;

    private void Start()
    {
        StartCoroutine(PlayIntroSequence());
    }

    private IEnumerator PlayIntroSequence()
    {
        textGroup.alpha = 0f;

        float timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            textGroup.alpha = timer / fadeDuration;
            yield return null;
        }

        textGroup.alpha = 1f;
        yield return new WaitForSeconds(displayDuration);

        timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            textGroup.alpha = 1f - (timer / fadeDuration);
            yield return null;
        }

        textGroup.alpha = 0f;

        int currentIndex = SceneManager.GetActiveScene().buildIndex;

        if(currentIndex + 1 < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(currentIndex + 1);
        }
        else
        {
            SceneManager.LoadScene(0);
        }
    }
}