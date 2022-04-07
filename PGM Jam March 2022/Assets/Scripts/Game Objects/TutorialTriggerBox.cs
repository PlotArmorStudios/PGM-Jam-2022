using Cinemachine;
using UnityEngine;
using FMODUnity;

public class TutorialTriggerBox : TriggerBox
{
    private void OnTriggerEnter(Collider other)
    {
        if (_alreadyTriggered) return;
        
        dialogueGameObject.SetActive(true);
        NPC.transform.position = NPCSpawnLocation.position;
        NPC.transform.LookAt(other.gameObject.transform.position);
        _vCam.Priority = 25;
        _alreadyTriggered = true;
        RuntimeManager.PlayOneShotAttached("event:/Dialogue Enter", gameObject);
    }
}