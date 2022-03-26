#define DebugCamLayerSwitch
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchViewLayers : MonoBehaviour
{
    private Camera _cam;

    private void Awake()
    {
        _cam = Camera.main;
    }

    private void OnEnable()
    {
        GameManager.OnTurnOnLanterns += SwitchToLightsOnLayers;
        GameManager.OnTurnOffLanterns += SwitchToLightsOffLayers;
    }

    private void OnDisable()
    {
        GameManager.OnTurnOnLanterns -= SwitchToLightsOnLayers;
        GameManager.OnTurnOffLanterns -= SwitchToLightsOffLayers;
    }

    [ContextMenu("Lights On Layers")]
    public void SwitchToLightsOnLayers()
    {
#if DebugCamLayerSwitch
        Debug.Log("Switch to lights ON layer");
#endif
        _cam.cullingMask &= ~LayerMask.GetMask("LightsOn", "LightsOff");
        _cam.cullingMask |= 1 << LayerMask.NameToLayer("LightsOn");
    }

    [ContextMenu("Lights Off Layers")]
    public void SwitchToLightsOffLayers()
    {
#if DebugCamLayerSwitch
        Debug.Log("Switch to lights OFF layer");
#endif
        _cam.cullingMask &= ~LayerMask.GetMask("LightsOn", "LightsOff");
        _cam.cullingMask |= 1 << LayerMask.NameToLayer("LightsOff");
    }
}