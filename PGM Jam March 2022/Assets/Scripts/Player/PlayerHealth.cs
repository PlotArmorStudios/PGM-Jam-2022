#define DebugTakeDamage
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerHealth : MonoBehaviour
{
    public static event Action OnTakeDamage;
    public static event Action OnPlayerDeath;

    [SerializeField] private float _maxHealth;
    [SerializeField] private float _healRate = 5f;

    private Volume _globalVolume;
    private float _currentHealth;
    private Animator _animator;
    public bool IsAlive { get; set; }

    private void OnValidate()
    {
        _globalVolume = GetComponentInChildren<Volume>();
    }

    private void OnEnable() => PhantomAttack.OnPhantomAttack += TakeDamage;
    private void OnDisable() => PhantomAttack.OnPhantomAttack -= TakeDamage;

    private void Start()
    {
        _currentHealth = _maxHealth;
        _animator = GetComponentInChildren<Animator>();
        IsAlive = true;
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

        if (_currentHealth <= 0) Die();
    }

    private void Die()
    {
        OnPlayerDeath?.Invoke();
        IsAlive = false;
        _animator.SetTrigger("Die");
        GameManager.Instance.DeactivatePlayer();
        StartCoroutine(LoadLastCheckPoint());
    }

    private IEnumerator LoadLastCheckPoint()
    {
        yield return new WaitForSeconds(3f);
        transform.position = new Vector3(PlayerPrefs.GetFloat("PlayerX"), PlayerPrefs.GetFloat("PlayerY"),
            PlayerPrefs.GetFloat("PlayerZ"));
        _animator.SetTrigger("Revive");
        _currentHealth = _maxHealth;
        IsAlive = true;
        GameManager.Instance.ActivatePlayer();
    }

    [ContextMenu("Take Damage Test")]
    public void TakeDamageTest()
    {
        TakeDamage(20);
    }
}