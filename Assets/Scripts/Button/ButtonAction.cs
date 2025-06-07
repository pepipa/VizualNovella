using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ButtonAction : MonoBehaviour
{
    private Button _button;
    public int index;
    private Dialogues _dialogues;
    private UnityAction _clickAction;
    void Start()
    {
        _button = GetComponent<Button>();
        _dialogues = FindObjectOfType<Dialogues>();
        _clickAction = new UnityAction(() => _dialogues.ChoiceButtonActions(index));
        _button.onClick.AddListener(_clickAction);
    }
 
}
