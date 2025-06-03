using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour, Controls.IDialogusActions
{
    Controls _inputActions;
    Dialogues _dialogues;
    private void OnEnable()
    {
        _dialogues = FindObjectOfType<Dialogues>();
        if ( _inputActions != null )
        {
            return;
        }
        _inputActions = new Controls();
        _inputActions.Dialogus.SetCallbacks(this);
        _inputActions.Dialogus.Enable();
    }
    private void OnDisable()
    {
        _inputActions.Dialogus.Disable();
    }
    public void OnNextPhrase(InputAction.CallbackContext context)
    {
        if (context.started && _dialogues.DialogPlay)
        {
            _dialogues.ContinieStory(_dialogues.choiceButtonsPanel.activeInHierarchy);
        }
    }
}
