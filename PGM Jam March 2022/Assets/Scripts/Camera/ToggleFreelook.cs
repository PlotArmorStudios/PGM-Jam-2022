using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;
using Cinemachine;

//Script to handle cinemachine freelook control
public class ToggleFreelook : MonoBehaviour
{
    public static ToggleFreelook Instance;
    public bool ToggleCamInput = true;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        CinemachineCore.GetInputAxis = GetAxisCustom;
    }

    private void OnEnable()
    {
        GameManager.OnActivatePlayerControl += ToggleFreelookOn;
        GameManager.OnDeactivatePlayerControl += ToggleFreelookOff;
    }

    private void OnDisable()
    {
        GameManager.OnActivatePlayerControl -= ToggleFreelookOn;
        GameManager.OnDeactivatePlayerControl -= ToggleFreelookOff;
    }

    public float GetAxisCustom(string axisName)
    {
        if (axisName == "Mouse X")
        {
            if (ToggleCamInput)
            {
                return UnityEngine.Input.GetAxis("Mouse X");
            }
            else
            {
                return 0;
            }
        }
        else if (axisName == "Mouse Y")
        {
            if (ToggleCamInput)
            {
                return UnityEngine.Input.GetAxis("Mouse Y");
            }
            else
            {
                return 0;
            }
        }

        return UnityEngine.Input.GetAxis(axisName);
    }

    private void ToggleFreelookOn()
    {
        ToggleCamInput = true;
    }
    
    private void ToggleFreelookOff()
    {
        ToggleCamInput = false;
    }
}