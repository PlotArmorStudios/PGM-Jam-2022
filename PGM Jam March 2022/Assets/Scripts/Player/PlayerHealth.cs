#define DebugTakeDamage
using System;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerHealth : MonoBehaviour
{
    public static event Action OnTakeDamage;

    [SerializeField] private float _maxHealth;
    [SerializeField] private float _healRate = 5f;

    private Volume _globalVolume;
    private float _currentHealth;

    private void OnValidate()
    {
        _globalVolume = GetComponentInChildren<Volume>();
    }

    private void OnEnable() => PhantomAttack.OnPhantomAttack += TakeDamage;
    private void OnDisable() => PhantomAttack.OnPhantomAttack -= TakeDamage;

    private void Start()
    {
        _currentHealth = _maxHealth;
    }

    private void Update()
    {
        if (_currentHealth > _maxHealth) _currentHealth = _maxHealth;
        if (_currentHealth < 0) _currentHealth = 0;
        if (_currentHealth < _maxHealth) _currentHealth += _healRate * Time.deltaTime;
        _globalVolume.weight = (_maxHealth - _currentHealth) / _maxHealth;
    }

    public void TakeDamage(float damage)
    {
#if DebugTakeDamage
        Debug.Log($"{gameObject.name} took damage");
#endif
        _currentHealth -= damage;
        OnTakeDamage?.Invoke();
    }

    [ContextMenu("Take Damage Test")]
    public void TakeDamageTest()
    {
        TakeDamage(20);
    }
}