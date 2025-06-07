using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SkipButton : MonoBehaviour
{
    private Button _button;
    private Dialogues _dialogues;
    private Coroutine _skipCoroutine;

    void Start()
    {
        _dialogues = FindObjectOfType<Dialogues>();
        _button = GetComponent<Button>();
        _button.onClick.AddListener(SkipDialog);
    }

    private void SkipDialog()
    {
        if (_skipCoroutine == null)
        {
            _skipCoroutine = StartCoroutine(SkipDialogCoroutine());
        }
    }

    private IEnumerator SkipDialogCoroutine()
    {
        while (_dialogues.DialogPlay && !_dialogues.choiceButtonsPanel.activeInHierarchy)
        {
            _dialogues.ContinieStory(_dialogues.choiceButtonsPanel.activeInHierarchy);
            yield return null;
        }

        _skipCoroutine = null;
    }
}
