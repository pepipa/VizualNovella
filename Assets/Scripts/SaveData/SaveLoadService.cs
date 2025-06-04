using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveLoadService : ISaveLoadService
{
    private const string DialogueProgressKey = "CurrentState";
    private const string SceneIndexKey = "SavedSceneIndex";
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
        PlayerPrefs.SetInt(SceneIndexKey, SceneManager.GetActiveScene().buildIndex);
        PlayerPrefs.Save();
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
