using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadService : ISaveLoadService
{
    private const string DialogueProgressKey = "CurrentState";
    private Dialogues _dialogues;
    private string _jsonData;

    public SaveLoadService(DialoguesInstaller dialoguesInstaller)
    {
        _dialogues = dialoguesInstaller.dialogues;
    }
    public void SaveData()
    {
        _jsonData = _dialogues.CurrentStory.state.ToJson();
        PlayerPrefs.SetString(DialogueProgressKey, _jsonData);
    }

    public void LoadData()
    {
        if(PlayerPrefs.GetString(DialogueProgressKey) != "")
        {
            _jsonData = PlayerPrefs.GetString(DialogueProgressKey);
            _dialogues.CurrentStory.state.LoadJson(_jsonData);
        }    
    }
}
