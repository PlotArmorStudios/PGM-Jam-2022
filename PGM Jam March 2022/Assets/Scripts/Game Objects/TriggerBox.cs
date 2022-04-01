using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBox : MonoBehaviour
{
    [SerializeField] private GameObject NPC;
    [SerializeField] private Transform NPCSpawnLocation;
    [SerializeField] private GameObject dialogueGameObject;
    [SerializeField] private GameObject collectMoreShards;
    [SerializeField] private string NPCName;

    private void OnTriggerEnter(Collider other)
    {
        if (GameManager.Instance.CheckShardsCollected())
        {
            dialogueGameObject.SetActive(true);
            NPC.transform.position = NPCSpawnLocation.position;
        }
        else
        {
            collectMoreShards.GetComponentInChildren<DialogueSection>().SetCharacterName(NPCName);
            collectMoreShards.SetActive(true);
        }
    }
}
