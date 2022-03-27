using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField] private Lantern _lanternToActivate;
    [SerializeField] private int _requiredNumberOfShards;
    
    [SerializeField] private DialogueTrigger _dialogueTrigger1, _dialogueTrigger2;

    private bool _playerInRange;
    private bool _dialogueOpen;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && _playerInRange)
        {
            if (CheckShards())
            {
                //turn on this NPC's particular lantern if amount of shards is enough
                _lanternToActivate.TurnOn();
            }
        }
    }

    private bool CheckShards()
    {
        return GameManager.Instance.CheckShardsCollected();
    }
    private void TriggerDialogue()
    {
        //Debug.Log("Trigger dialogue/cutscene");
        //DialogueManager.Instance.TriggerDialogue();
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Player>();
        if (!player) return;
        _playerInRange = true;
        
        GameManager.Instance.RequiredShardsToCollect = _requiredNumberOfShards;
        
        if (!_dialogueOpen)
        {
            DialogueManager.Instance.RegisterDialogueTrigger(_dialogueTrigger1);
            _dialogueOpen = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var player = other.GetComponent<Player>();
        if (!player) return;
        _playerInRange = false;
        DialogueManager.Instance.DeregisterDialogueTrigger();
    }
}