using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField] private Lantern _lanternToActivate;
    [SerializeField] private int _requiredNumberOfShards;
    
    private bool _playerInRange;
    private bool _dialogueRegistered;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && _playerInRange)
        {
            //VerifyShardsCollected();
        }
    }

    private void VerifyShardsCollected()
    {
        if (CheckShards())
        {
            //turn on this NPC's particular lantern if amount of shards is enough
            _lanternToActivate.TurnOn();
        }
    }

    private bool CheckShards()
    {
        return GameManager.Instance.CheckShardsCollected();
    }
    private void TriggerDialogue()
    {
        Debug.Log("Trigger dialogue/cutscene");
        DialogueManager.Instance.TriggerDialogue();
        GameManager.Instance.DeactivatePlayer();
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Player>();
        if (!player) return;
        
        _playerInRange = true;
        
        GameManager.Instance.RequiredShardsToCollect = _requiredNumberOfShards;
        
        //Register dialogue if this this first time player walked up to this npc
        if (!_dialogueRegistered)
        {
            _dialogueRegistered = true;
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