using Ink.Runtime;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
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

    [SerializeField] private GameObject letterPanel2;
    [SerializeField] private CanvasGroup letterCanvasGroup2;

    [SerializeField] private GameObject screamerPanel;
    [SerializeField] private CanvasGroup screamerCanvasGroup;

    [SerializeField] private GameObject bracletPanel;
    [SerializeField] private CanvasGroup bracletCanvasGroup;

    [SerializeField] private GameObject keyPanel;
    [SerializeField] private CanvasGroup keyCanvasGroup;

    [SerializeField] private GameObject keySoundPanel;
    [SerializeField] private CanvasGroup keySoundCanvasGroup;

    [SerializeField] private GameObject letterPanel3;
    [SerializeField] private CanvasGroup letterCanvasGroup3;

    [SerializeField] private GameObject streetPanel;
    [SerializeField] private CanvasGroup streetCanvasGroup;

    [SerializeField] private GameObject fragmentPanel;
    [SerializeField] private CanvasGroup fragmentCanvasGroup;


    private BackgroundController _backgroundController;

    private Coroutine _letterCoroutine;

    private Coroutine _letterCoroutine2;

    private Coroutine _screamerCoroutine;

    private Coroutine _bracletCoroutine; 

    private Coroutine _keyCoroutine;

    private Coroutine _keySoundCoroutine;

    private Coroutine _letterCoroutine3;

    private Coroutine _streetCoroutine;

    private Coroutine _fragmentCoroutine;

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

            if (tag == "fragment_show")
            {
                if (_fragmentCoroutine != null)
                    StopCoroutine(_fragmentCoroutine);

                ShowFragmentInstant();

                _dialoguePanel.SetActive(false);
                choiceButtonsPanel.SetActive(false);
            }
            else if (tag == "fragment_hide")
            {
                if (_fragmentCoroutine != null)
                    StopCoroutine(_fragmentCoroutine);

                HideFragmentInstant();

                _dialoguePanel.SetActive(true);
            }
            if (tag == "letter_show")
            {
                if (_letterCoroutine != null)
                    StopCoroutine(_letterCoroutine);

                ShowLetterInstant();

                _dialoguePanel.SetActive(false);
                choiceButtonsPanel.SetActive(false);
            }
            else if (tag == "letter_hide")
            {
                if (_letterCoroutine != null)
                    StopCoroutine(_letterCoroutine);

                HideLetterInstant();

                _dialoguePanel.SetActive(true);
            }

            if (tag == "letter_show2")
            {
                if (_letterCoroutine2 != null)
                    StopCoroutine(_letterCoroutine2);

                ShowLetterInstant2();

                _dialoguePanel.SetActive(false);
                choiceButtonsPanel.SetActive(false);
            }
            else if (tag == "letter_hide2")
            {
                if (_letterCoroutine2 != null)
                    StopCoroutine(_letterCoroutine2);

                HideLetterInstant2();

                _dialoguePanel.SetActive(true);
            }

            if (tag == "screamer_show")
            {
                if (_screamerCoroutine != null)
                    StopCoroutine(_screamerCoroutine);

                ShowScreamerInstant();

                _dialoguePanel.SetActive(false);
                choiceButtonsPanel.SetActive(false);
            }
            else if (tag == "screamer_hide")
            {
                if (_screamerCoroutine != null)
                    StopCoroutine(_screamerCoroutine);

                HideScreamerInstant();

                _dialoguePanel.SetActive(true);
            }
            if (tag == "braclet_show")
            {
                if (_bracletCoroutine != null)
                    StopCoroutine(_bracletCoroutine);

                ShowBracletInstant();

                choiceButtonsPanel.SetActive(false);
            }
            else if (tag == "braclet_hide")
            {
                if (_bracletCoroutine != null)
                    StopCoroutine(_bracletCoroutine);

                HideBracletInstant();

            }
            if (tag == "key_show")
            {
                if (_keyCoroutine != null)
                    StopCoroutine(_keyCoroutine);

                ShowKeyInstant();

            }
            else if (tag == "key_hide")
            {
                if (_keyCoroutine != null)
                    StopCoroutine(_keyCoroutine);

                HideKeyInstant();

            }
            if (tag == "key_sound_show")
            {
                if (_keySoundCoroutine != null)
                    StopCoroutine(_keySoundCoroutine);

                ShowKeySoundInstant();

            }
            else if (tag == "key_sound_hide")
            {
                if (_keySoundCoroutine != null)
                    StopCoroutine(_keySoundCoroutine);

                HideKeySoundInstant();

            }
            if (tag == "letter_show3")
            {
                if (_letterCoroutine3 != null)
                    StopCoroutine(_letterCoroutine3);

                ShowLetterInstant3();

                _dialoguePanel.SetActive(false);
                choiceButtonsPanel.SetActive(false);
            }
            else if (tag == "letter_hide3")
            {
                if (_letterCoroutine3 != null)
                    StopCoroutine(_letterCoroutine3);

                HideLetterInstant3();

                _dialoguePanel.SetActive(true);
            }
            if (tag == "sound_street_show")
            {
                if (_streetCoroutine != null)
                    StopCoroutine(_streetCoroutine);

                ShowStreetInstant();
            }
            else if (tag == "sound_street_hide")
            {
                if (_streetCoroutine != null)
                    StopCoroutine(_streetCoroutine);

                HideStreetInstant();
            }
            else if (tag.StartsWith("loadScene"))
            {
                string[] parts = tag.Split(' ');
                if (parts.Length > 1)
                {
                    string sceneName = parts[1];
                    Debug.Log($"[Ink] Loading scene: {sceneName}");
                    SceneManager.LoadScene(sceneName);
                }
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

    private void ShowFragmentInstant()
    {
        fragmentCanvasGroup.alpha = 1f;
        fragmentPanel.SetActive(true);
    }

    private void HideFragmentInstant()
    {
        fragmentCanvasGroup.alpha = 0f;
        fragmentPanel.SetActive(false);
    }

    private void ShowStreetInstant()
    {
        streetCanvasGroup.alpha = 1f;
        streetPanel.SetActive(true);
    }
    private void HideStreetInstant()
    {
        streetCanvasGroup.alpha = 0f;
        streetPanel.SetActive(false);
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

    private void ShowLetterInstant2()
    {
        letterCanvasGroup2.alpha = 1f;
        letterPanel.SetActive(true);
    }
    private void HideLetterInstant2()
    {
        letterCanvasGroup2.alpha = 0f;
        letterPanel.SetActive(false);
    }

    private void ShowLetterInstant3()
    {
        letterCanvasGroup3.alpha = 1f;
        letterPanel3.SetActive(true);
    }
    private void HideLetterInstant3()
    {
        letterCanvasGroup3.alpha = 0f;
        letterPanel3.SetActive(false);
    }

    private void ShowScreamerInstant()
    {
        screamerCanvasGroup.alpha = 1f;
        screamerPanel.SetActive(true);
    }
    private void HideScreamerInstant()
    {
        screamerCanvasGroup.alpha = 0f;
        screamerPanel.SetActive(false);
    }
    private void ShowBracletInstant()
    {
        bracletCanvasGroup.alpha= 1f;
        bracletPanel.SetActive(true);
    }

    private void HideBracletInstant()
    {
        bracletCanvasGroup.alpha = 0f;
        bracletPanel.SetActive(false);
    }    

    private void ShowKeyInstant()
    {
        keyCanvasGroup.alpha = 1f;
        keyPanel.SetActive(true);
    }
    private void HideKeyInstant()
    {
        keyCanvasGroup.alpha = 0f;
        keyPanel.SetActive(false);
    }

    private void ShowKeySoundInstant()
    {
        keySoundCanvasGroup.alpha = 1f;
        keySoundPanel.SetActive(true);
    }
    private void HideKeySoundInstant()
    {
        keySoundCanvasGroup.alpha = 0f;
        keySoundPanel.SetActive(false);
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

        GameObject music = GameObject.FindGameObjectWithTag("Music");
        if (music != null)
        {
            Destroy(music);
        }

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
