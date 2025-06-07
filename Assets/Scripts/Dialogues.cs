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

    [SerializeField] private GameObject letterPanel;
    [SerializeField] private CanvasGroup letterCanvasGroup;

    private BackgroundController _backgroundController;

    private Coroutine _letterCoroutine;
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
    /*  private void Awake()
      {
          _currentStory = new Story(_inkJson.text);
      }*/
    private void Start()
    {
        _backgroundController = FindObjectOfType<BackgroundController>(); 

        foreach (var character in FindObjectsOfType<Character>())
        {
            characters.Add(character);
        }
        StartDialogue();
    }

    public void StartDialogue()
    {
        DialogPlay = true;
        _dialoguePanel.SetActive(true);

        int savedScene = PlayerPrefs.GetInt("SavedSceneIndex", -1);
        bool hasSave = PlayerPrefs.HasKey(DialogueProgressKey) && !string.IsNullOrEmpty(PlayerPrefs.GetString(DialogueProgressKey));
        bool isSameScene = savedScene == SceneManager.GetActiveScene().buildIndex;

        if (hasSave && isSameScene)
        {
            _currentStory = new Story(_inkJson.text);
            _saveLoadService.LoadData();

            _dialogueText.text = _currentStory.currentText;
            HandleVisualState(); 
            ChangeCharacter();
            ShowChoiceButtons();
        }
        else
        {
            _currentStory = new Story(_inkJson.text);
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
             if (tag == "letter_show")
             {
                 if (_letterCoroutine != null)
                     StopCoroutine(_letterCoroutine);

                 ShowLetterInstant();

                 _dialoguePanel.SetActive(false);
                 choiceButtonsPanel.SetActive(false);
             }
             if (tag == "letter_hide")
             {
                 if (_letterCoroutine != null)
                     StopCoroutine(_letterCoroutine);

                 HideLetterInstant();

                 _dialoguePanel.SetActive(true);
             }

         }
     }

    private void HandleVisualState()
    {
        if (_currentStory == null) return;


        string bg = _currentStory.variablesState["bgName"] as string;
        _backgroundController.SetBackground(bg); 


        bool alinaVisible = (bool)_currentStory.variablesState["alinaVisible"];
        Character alina = characters.Find(c => c.characterName == "Алина");
        if (alina != null)
        {
            if (alinaVisible) alina.Show();
            else alina.Hide();
        }

        bool unknownVisible = (bool)_currentStory.variablesState["ghostVisible"];
        Character unknown = characters.Find(c => c.characterName == "???");
        if (unknown != null)
        {
            if (unknownVisible) unknown.Show(); 
            else unknown.Hide();
        }
    }

    private void ShowLetterInstant()
    {
        letterCanvasGroup.alpha = 1f;
        letterPanel.SetActive(true);
    }
    private void HideLetterInstant()
    {
        letterCanvasGroup.alpha = 0f;
        letterPanel.SetActive(false);
    }

    private void ShowDialogue()
    {
        _dialogueText.text = _currentStory.Continue().Trim();
        HandleTags(_currentStory.currentTags);
        HandleVisualState();
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
