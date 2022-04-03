using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueSection : MonoBehaviour
{
    public static event Action OnEndDialogue;

    [SerializeField] [TextArea(7, 10)] private string[] sentences;

    [Tooltip("Character Name should be the same number of elements as Sentences array above")] [SerializeField]
    private string[] characterName;

    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private Animator dialogueSection;
    [SerializeField] private Animator dialogue;
    [SerializeField] private Animator nameAnim;
    [SerializeField] private Button continueButton;
    [SerializeField] private bool _resetDialogueOnEnable;
    [SerializeField] private SceneLoader _sceneLoader;
    [SerializeField] private string _sceneToLoad;
    [SerializeField] private Lantern _lanternToActivate;

    private string currentName = "";
    private int numOfSentences;

    void OnEnable()
    {
        if (GameManager.Instance)
            GameManager.Instance.DeactivatePlayer();
        numOfSentences = sentences.Length;
        continueButton.onClick.AddListener(ContinueButtonPressed);

        if (_resetDialogueOnEnable)
            currentSentenceIndex = 0;

        DisplayNextSen();
    }

    private void OnDestroy() => continueButton.onClick.RemoveListener(ContinueButtonPressed);

    private int currentSentenceIndex = 0;

    private void DisplayNextSen()
    {
        if (currentSentenceIndex >= numOfSentences)
        {
            dialogueSection.Play("DialogueFlyOut", -1, 0f);

            if (_sceneLoader) _sceneLoader.LoadScene(_sceneToLoad);
            if (GameManager.Instance) GameManager.Instance.ActivatePlayer();
            if (_lanternToActivate) _lanternToActivate.TurnOn();
            
            OnEndDialogue?.Invoke();
            
            return;
        }

        dialogue.Play("TextFadeIn", -1, 0f);
        dialogueText.text = sentences[currentSentenceIndex];
        if (!characterName[currentSentenceIndex].Equals(currentName))
        {
            nameAnim.Play("NameFadeIn", -1, 0f);
        }

        currentName = characterName[currentSentenceIndex];
        nameText.text = currentName;

        currentSentenceIndex++;
    }

    private void ContinueButtonPressed()
    {
        dialogue.Play("TextFadeOut", -1, 0f);
        if (currentSentenceIndex < numOfSentences && !characterName[currentSentenceIndex].Equals(currentName)
            || currentSentenceIndex == numOfSentences)
        {
            nameAnim.Play("NameFadeOut", -1, 0f);
        }
    }

    public void SetCharacterName(string name)
    {
        characterName[0] = name;
    }
}