using UnityEngine;

public class TutorialTriggerBox : TriggerBox
{
    protected override void OnTriggerEnter(Collider other)
    {
        if (_alreadyTriggered) return;
        
        dialogueGameObject.SetActive(true);
        NPC.transform.position = NPCSpawnLocation.position;
        NPC.transform.LookAt(other.gameObject.transform.position);
        _vCam.Priority = 20;
        _alreadyTriggered = true;
    }
}