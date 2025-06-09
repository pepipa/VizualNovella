using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InputReader : MonoBehaviour, Controls.IDialogusActions
{
    Controls _inputActions;
    Dialogues _dialogues;

    private GraphicRaycaster _raycaster;
    private PointerEventData _pointerEventData;
    private EventSystem _eventSystem;

    private void Awake()
    {
        _eventSystem = EventSystem.current;
        _raycaster = FindObjectOfType<GraphicRaycaster>();
    }

    private void OnEnable()
    {
        _dialogues = FindObjectOfType<Dialogues>();

        if (_inputActions != null) return;

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
            if (IsPointerOverUIButton())
                return;

            _dialogues.ContinieStory(_dialogues.choiceButtonsPanel.activeInHierarchy);
        }
    }

    private bool IsPointerOverUIButton()
    {
        _pointerEventData = new PointerEventData(_eventSystem)
        {
            position = Mouse.current.position.ReadValue()
        };

        List<RaycastResult> results = new List<RaycastResult>();
        _raycaster.Raycast(_pointerEventData, results);

        foreach (var result in results)
        {
            if (result.gameObject.GetComponent<UnityEngine.UI.Button>() != null)
            {
                return true; 
            }
        }

        return false;
    }
}
