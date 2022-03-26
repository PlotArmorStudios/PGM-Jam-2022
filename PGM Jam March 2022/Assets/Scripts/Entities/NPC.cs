using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            TriggerDialogue();
        }
    }

    private void TriggerDialogue()
    {
        Debug.Log("Trigger dialogue/cutscene");
    }
}
