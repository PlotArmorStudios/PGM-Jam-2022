using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using FMODUnity;

public class TriggerBox : MonoBehaviour
{
    [SerializeField] protected NPC NPC;
    [SerializeField] protected Transform NPCSpawnLocation;
    [SerializeField] protected GameObject dialogueGameObject;
    [SerializeField] private GameObject collectMoreShards;
    [SerializeField] protected string NPCName;
    [SerializeField] private Transform _npcSpot;

    protected CinemachineVirtualCamera _vCam { get; set; }

    protected bool _alreadyTriggered;

    private void OnEnable()
    {
        _vCam = GetComponentInChildren<CinemachineVirtualCamera>();
        DialogueSection.OnEndDialogue += ResetVCam;
    }

    private void OnDisable() => DialogueSection.OnEndDialogue -= ResetVCam;

    private void ResetVCam()
    {
        NPC.transform.position = _npcSpot.position;
        _vCam.gameObject.SetActive(false);
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (_alreadyTriggered) return;

        var player = other.GetComponent<Player>();
        if (!player) return;

        _vCam.gameObject.SetActive(true);
        _vCam.Priority = 20;

        if (GameManager.Instance.NumberOfShards >= GameManager.Instance.RequiredShardsToCollect)
        {
            dialogueGameObject.SetActive(true);
            NPC.transform.position = NPCSpawnLocation.position;
            NPC.transform.LookAt(other.gameObject.transform.position);
            _alreadyTriggered = true;
            RuntimeManager.PlayOneShotAttached("event:/Dialogue Enter", gameObject);
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