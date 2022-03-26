//#define DebugShard
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Build.Content;
using UnityEngine;

public class SoulShard : MonoBehaviour
{
    [SerializeField] private List<Light> _lights;

    private void OnValidate()
    {
        _lights = GetComponentsInChildren<Light>().ToList();
    }

    private void OnEnable()
    {
        GameManager.OnTurnOnLanterns += TurnOffLights;
        GameManager.OnTurnOffLanterns += TurnOnLights;
    }
    private void OnDisable()
    {
        GameManager.OnTurnOnLanterns -= TurnOffLights;
        GameManager.OnTurnOffLanterns -= TurnOnLights;
    }

    [ContextMenu("Lights On")]
    private void TurnOnLights()
    {
        foreach (var light in _lights) light.cullingMask = LayerMask.NameToLayer("Everything");
    }
    
    [ContextMenu("Lights Off")]
    private void TurnOffLights()
    {
        foreach (var light in _lights) light.cullingMask = 1 << 9;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<PlayerControl>();

        if (!player) return;
#if DebugShard
        Debug.Log("Picked up shard.");
#endif
        GameManager.Instance.CollectShard();
        gameObject.SetActive(false);
    }
}