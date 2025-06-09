using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChapterIntroTransition : MonoBehaviour
{
    [SerializeField] private string nextSceneName = "Chapter3Happy";
    [SerializeField] private float delayBeforeTransition = 3f;

    public void BeginTransition()
    {
        StartCoroutine(LoadNextSceneAfterDelay());
    }

    private IEnumerator LoadNextSceneAfterDelay()
    {
        yield return new WaitForSeconds(delayBeforeTransition);
        SceneManager.LoadScene(nextSceneName);
    }
}
