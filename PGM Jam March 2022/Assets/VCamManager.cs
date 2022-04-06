using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class VCamManager : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _vCam;

    private void OnEnable()
    {
        DialogueSection.OnEndDialogue += SetVCam;
    }

    private void OnDisable()
    {
        DialogueSection.OnEndDialogue -= SetVCam;
    }

    private void SetVCam()
    {
        _vCam.Priority = 20;
    }
}
