using Ink.Runtime;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class Dialogues : MonoBehaviour
{
    private const string DialogueProgressKey = "CurrentState";
    private Story _currentStory;
    private TextAsset _inkJson;

    private GameObject _dialoguePanel;
    private TextMeshProUGUI _dialogueText;
    private TextMeshProUGUI _nameText;

    [HideInInspector] public GameObject choiceButtonsPanel;
    private GameObject _choiceButton;
    private List<TextMeshProUGUI> _choicesText = new();
    private List<Character> characters = new();
    private float multiplier = 1.1f;
    private SaveLoadService _saveLoadService;

    public bool DialogPlay {  get; private set; }
    public Story CurrentStory => _currentStory;
    [Inject]
    public void Construct(DialoguesInstaller dialoguesInstaller, SaveLoadService saveLoadService)
    {
        _inkJson = dialoguesInstaller.inkJson;
        _dialoguePanel = dialoguesInstaller.dialoguePanel;
        _dialogueText = dialoguesInstaller.dialogueText;
        _nameText = dialoguesInstaller.nameText;
        choiceButtonsPanel = dialoguesInstaller.choiceButtonsPanel;
        _choiceButton = dialoguesInstaller.choiceButton;
        _saveLoadService = saveLoadService;
    }
    private void Awake()
    {
        _currentStory = new Story(_inkJson.text);
    }
    private void Start()
    {
        foreach (var character in FindObjectsOfType<Character>())
        {
            characters.Add(character);
        }
        StartDialogue();
    }

    public void StartDialogue()
    {    
        int savedScene = PlayerPrefs.GetInt("SavedScene", -1);
        DialogPlay = true;
        _dialoguePanel.SetActive(true);
        if (savedScene == UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex && PlayerPrefs.GetString(DialogueProgressKey) != "")
        {         
            _saveLoadService.LoadData();
            _dialogueText.text = _currentStory.currentText;
            ChangeCharacter();
            ShowChoiceButtons();
        }
        else
        {
            ContinieStory();
        }
    }

    public void ContinieStory(bool choiceBefore = false)
    {
        if (_currentStory.canContinue)
        {
            ShowDialogue();
            ShowChoiceButtons();
        }
        else if (!choiceBefore)
        {
            ExitDialogue();
        }
    }
    private void HandleTags(List<string> tags)
    {
        foreach (string tag in tags)
        {
            if (tag.StartsWith("bg "))
            {
                string bgName = tag.Substring(3);
                FindObjectOfType<BackgroundController>().SetBackground(bgName);
            }
            if (tag.StartsWith("show "))
            {
                string name = tag.Substring(5);
                var character = characters.Find(c => c.characterName == name);
                if (character != null)
                {
                    character.Show();

                    if (_currentStory.variablesState[character.currentEmotionVariable] != null)
                    {
                        int emotionIndex = Convert.ToInt32(_currentStory.variablesState[character.currentEmotionVariable]);
                        character.ChangeEmotions(emotionIndex);
                    }
                    else
                    {
                        Debug.LogWarning($"Не найдена переменная эмоции: {character.currentEmotionVariable}");
                    }
                }
                else
                {
                    Debug.LogWarning($"Персонаж с именем '{name}' не найден!");
                }
            }
            if (tag.StartsWith("hide "))
            {
                string name = tag.Substring(5);
                var character = characters.Find(c => c.characterName == name);
                if (character != null)
                {
                    character.Hide();
                }
                else
                {
                    Debug.LogWarning($"Персонаж для скрытия с именем '{name}' не найден!");
                }
            }

        }
    }
    private void ShowDialogue()
    {
        _dialogueText.text = _currentStory.Continue();
        HandleTags(_currentStory.currentTags);
        _saveLoadService.SaveData();
        ChangeCharacter();
    }

    private void ChangeCharacter()
    {
        _nameText.text = (string)_currentStory.variablesState["characterName"];
        var index = characters.FindIndex(character => character.characterName.Contains(_nameText.text));
        characters.ForEach(c => c.ChangeEmotions((int)_currentStory.variablesState[c.currentEmotionVariable]));
        ChangeCharacterScale(index);
    }

    private void ChangeCharacterScale(int indexCharacter)
    {
        if (indexCharacter >= 0)
        {
            foreach (var character in characters)
            {
                if (character != characters[indexCharacter])
                {
                    character.ResetScale();
                }
                else if (character.DefaultScale == character.transform.localScale)
                {
                    character.ChangeScale(multiplier);
                }
            }               
        }
        else
        {
            characters.ForEach(character=>character.ResetScale());
        }    
    }

    private void ShowChoiceButtons()
    {
        List<Choice> currentChoices = _currentStory.currentChoices;
        choiceButtonsPanel.SetActive(currentChoices.Count != 0);
        if (currentChoices.Count <= 0 )
        {
            return;
        }
        choiceButtonsPanel.transform.Cast<Transform>().ToList().ForEach(child => Destroy(child.gameObject));
        _choicesText.Clear();
        for (int i = 0; i < currentChoices.Count; i++)
        {
            GameObject choice = Instantiate(_choiceButton);
            choice.GetComponent<ButtonAction>().index = i;
            choice.transform.SetParent(choiceButtonsPanel.transform);

            TextMeshProUGUI choiceText = choice.GetComponentInChildren<TextMeshProUGUI>();
            choiceText.text = currentChoices[i].text;
            _choicesText.Add(choiceText);
        }
    }

    public void ChoiceButtonActions (int choiceIndex)
    {
        _currentStory.ChooseChoiceIndex(choiceIndex);
        ContinieStory(true);
    }

    private void ExitDialogue()
    {
        DialogPlay = false;
        _dialoguePanel.SetActive(false);
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.LogWarning("Следующей сцены нет в Build Settings!");
        }
    }
}
