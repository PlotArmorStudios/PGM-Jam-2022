#define DebugLights
using System;
using UnityEngine;

public abstract class Lantern : MonoBehaviour
{
    [SerializeField] private Light _soulLight;
    private void Start()
    {
        _soulLight = GetComponentInChildren<Light>(true);
    }

    public virtual void TurnOff()
    {
#if DebugLights
        Debug.Log($"{gameObject.name} turned off.");
#endif
        _soulLight.intensity = 0f;
    }

    public virtual void TurnOn()
    {
#if DebugLights
        Debug.Log($"{gameObject.name} turned on.");
#endif
        _soulLight.intensity = 60f;
    }
}