using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueSection : MonoBehaviour
{
    [SerializeField] [TextArea(7, 10)] private string[] sentences;
    [Tooltip("Character Name should be the same number of elements as Sentences array above")]
    [SerializeField] private string[] characterName;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private Animator dialogueSection;
    [SerializeField] private Animator dialogue;
    [SerializeField] private Animator nameAnim;
    [SerializeField] private Button continueButton;
    private string currentName = "";
    private int numOfSentences;

    // Start is called before the first frame update
    void Start()
    {
        numOfSentences = sentences.Length;
        continueButton.onClick.AddListener(ContinueButtonPressed);
        DisplayNextSen();
    }

    private int currentSentenceIndex = 0;
    private void DisplayNextSen()
    {
        if (currentSentenceIndex >= sentences.Length)
        {
            dialogueSection.Play("DialogueFlyOut", -1, 0f);
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
}