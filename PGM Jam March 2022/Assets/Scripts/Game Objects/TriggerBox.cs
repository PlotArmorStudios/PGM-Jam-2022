using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class TriggerBox : MonoBehaviour
{
    [SerializeField] protected GameObject NPC;
    [SerializeField] protected Transform NPCSpawnLocation;
    [SerializeField] protected GameObject dialogueGameObject;
    [SerializeField] private GameObject collectMoreShards;
    [SerializeField] protected string NPCName;

    protected CinemachineVirtualCamera _vCam { get; set; }
    
    protected bool _alreadyTriggered;

    private void OnEnable()
    {
        _vCam = GetComponentInChildren<CinemachineVirtualCamera>();
        DialogueSection.OnEndDialogue += ResetVCam;
    }

    private void OnDisable() => DialogueSection.OnEndDialogue -= ResetVCam;

    private void ResetVCam() => _vCam.Priority = 0;

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (_alreadyTriggered) return;

        var player = other.GetComponent<Player>();
        if (!player) return;

        _vCam.Priority = 20;
        if (GameManager.Instance.CheckShardsCollected())
        {
            dialogueGameObject.SetActive(true);
            NPC.transform.position = NPCSpawnLocation.position;
            NPC.transform.LookAt(other.gameObject.transform.position);
            _alreadyTriggered = true;
        }
        else
        {
            collectMoreShards.GetComponentInChildren<DialogueSection>().SetCharacterName(NPCName);
            NPC.transform.position = NPCSpawnLocation.position;
            NPC.transform.LookAt(other.gameObject.transform.position);
            collectMoreShards.SetActive(true);
        }
    }
}