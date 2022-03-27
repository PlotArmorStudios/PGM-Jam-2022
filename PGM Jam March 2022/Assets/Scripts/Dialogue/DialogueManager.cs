using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    public static event Action OnSetTransition;
    public static event Action<DialogueTrigger> OnTriggerDialogue;

    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private TMP_Text _dialogueText;
    [SerializeField] private Animator _animator;

    [SerializeField] private float _dialogueTypeTime = .2f;
    [SerializeField] private float _delayDialogueAmount = 1.5f;

    public static DialogueManager Instance;
    private DialogueTrigger _dialogueTrigger;
    
    private Queue<string> _sentences;
    private Queue<string> _names;
    private Queue<int> _transitionNumbers;
    private int _currentTransitionNumber;
    private bool _conversationInProgress;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _sentences = new Queue<string>();
        _names = new Queue<string>();
        _transitionNumbers = new Queue<int>();
    }

    public void RegisterDialogueTrigger(DialogueTrigger dialogueTrigger)
    {
        _dialogueTrigger = dialogueTrigger;
    }

    public void DeregisterDialogueTrigger()
    {
        _dialogueTrigger = null;
    }

    public void TriggerDialogue()
    {
        StartCoroutine(DelayTriggerDialogue());
    }
    
    private IEnumerator DelayTriggerDialogue()
    {
        yield return new WaitForSeconds(_delayDialogueAmount);
        _dialogueTrigger.TriggerDialogue();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        _conversationInProgress = true;
        _animator.SetBool("IsOpen", true);

        _names.Clear();

        foreach (var name in dialogue.Names)
        {
            _names.Enqueue(name);
        }

        _sentences.Clear();

        foreach (var sentence in dialogue.Sentences)
        {
            _sentences.Enqueue(sentence);
        }

        _transitionNumbers.Clear();

        foreach (var transitionNumber in dialogue.NumberOfContinues)
        {
            _transitionNumbers.Enqueue(transitionNumber);
        }

        DisplayNextName();
        DisplayNextSentence();
        SetTransitionNumber();
    }

    private void SetTransitionNumber()
    {
        if (_transitionNumbers.Count == 0)
        {
            return;
        }

        _currentTransitionNumber = _transitionNumbers.Dequeue();
    }

    public void TrySetTransitionNumber()
    {
        if (!_conversationInProgress) return;
        _currentTransitionNumber--;

        if (_currentTransitionNumber > 0) return;
        SetTransitionNumber();
        OnSetTransition?.Invoke();
    }


    public void DisplayNextName()
    {
        if (_names.Count == 0)
        {
            return;
        }

        string name = _names.Dequeue();
        _nameText.text = name;
    }

    public void DisplayNextSentence()
    {
        if (_sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = _sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    private IEnumerator TypeSentence(string sentence)
    {
        _dialogueText.text = "";

        foreach (char letter in sentence.ToCharArray())
        {
            _dialogueText.text += letter;
            yield return new WaitForSeconds(_dialogueTypeTime);
        }
    }

    private void EndDialogue()
    {
        _conversationInProgress = false;
        _animator.SetBool("IsOpen", false);
    }
}