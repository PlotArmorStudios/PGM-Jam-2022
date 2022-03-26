#define DebugTakeDamage
using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public static event Action OnTakeDamage;
    [SerializeField] private int _maxHealth;

    private int _currentHealth;

    private void Start()
    {
        _currentHealth = _maxHealth;
    }

    public void TakeDamage()
    {
#if DebugTakeDamage
        Debug.Log($"{gameObject.name} took damage");
#endif
        _currentHealth--;
        OnTakeDamage?.Invoke();
    }
}