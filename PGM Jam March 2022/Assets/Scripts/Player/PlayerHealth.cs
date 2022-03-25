#define DebugTakeDamage
using System;
using UnityEngine;
using UnityEngine.AI;

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

[RequireComponent(typeof (NavMeshAgent))]
public class Phantom : MonoBehaviour
{
    [SerializeField] private float _playerDetectDistance;
    [SerializeField] private LayerMask _playerLayer;

    private PlayerControl _player;
    private NavMeshAgent _navAgent;
    private bool _playerInRange;

    private void Start()
    {
        _navAgent = GetComponent<NavMeshAgent>();
        _player = FindObjectOfType<PlayerControl>();
    }

    private void OnTriggerStay(Collider other)
    {
        _navAgent.SetDestination(_player.transform.position);
    }

    private void OnTriggerExit(Collider other)
    {
        _navAgent.SetDestination(transform.position);
    }
}