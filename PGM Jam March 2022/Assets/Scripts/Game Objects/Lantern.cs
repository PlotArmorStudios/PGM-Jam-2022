//#define DebugLights
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Lantern : MonoBehaviour
{
    [SerializeField] private List<Light> _soulLights;
    [SerializeField] private List<ParticleSystem> _soulFlames;
    
    private void OnValidate() => _soulLights = GetComponentsInChildren<Light>(true).ToList();

    public virtual void TurnOn()
    {
#if DebugLights
        Debug.Log($"{gameObject.name} turned on.");
#endif
        foreach (var soulLight in _soulLights) soulLight.intensity = 2f;
        foreach (var soulFlame in _soulFlames) soulFlame.gameObject.SetActive(true);
    }
    
    public virtual void TurnOff()
    {
#if DebugLights
        Debug.Log($"{gameObject.name} turned off.");
#endif
        foreach (var soulLight in _soulLights) soulLight.intensity = 0f;
        foreach (var soulFlame in _soulFlames) soulFlame.gameObject.SetActive(false);
    }
}