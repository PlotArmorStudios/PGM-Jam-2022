using UnityEngine;

public abstract class Lantern : MonoBehaviour
{
    [SerializeField] private Light _soulLight;

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
        _soulLight.intensity = 1f;
    }
}