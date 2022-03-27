#define MovePhantom
using System;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Phantom : MonoBehaviour
{
    private Player _player;
    private NavMeshAgent _navAgent;
    private bool _playerInRange;

    private void Start()
    {
        _navAgent = GetComponent<NavMeshAgent>();
        _player = FindObjectOfType<Player>();
    }

    private void OnEnable()
    {
        GameManager.OnMovePhantom += MoveToArea;
    }

    private void OnDisable()
    {
        GameManager.OnMovePhantom -= MoveToArea;
    }

    public void MoveToArea(int area)
    {
#if MovePhantom
        Debug.Log($"Move Phantom to area {area}");
#endif
        _navAgent.speed = 20;
        _navAgent.SetDestination(GameManager.Instance.PhantomMovePoints[area - 1].position);
    }

    private void OnTriggerStay(Collider other)
    {
        var player = other.GetComponent<Player>();
        if (!player) return;
        _navAgent.speed = 3.5f;
        _navAgent.SetDestination(_player.transform.position);
    }

    private void OnTriggerExit(Collider other)
    {
        var player = other.GetComponent<Player>();
        if (!player) return;
        _navAgent.SetDestination(transform.position);
    }
}