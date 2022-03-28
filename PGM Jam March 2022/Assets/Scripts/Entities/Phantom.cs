#define MovePhantom
using System;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Phantom : Entity
{
    private bool _playerInRange;

    private void Start()
    {
        NavAgent = GetComponent<NavMeshAgent>();
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
        NavAgent.speed = 20;
        InitialPosition = GameManager.Instance.PhantomMovePoints[area - 1].position;
        NavAgent.SetDestination(GameManager.Instance.PhantomMovePoints[area - 1].position);
    }

    private void OnTriggerStay(Collider other)
    {
        var player = other.GetComponent<Player>();
        if (!player) return;
        NavAgent.speed = 3.5f;
        NavAgent.SetDestination(PlayerTarget.transform.position);
    }

    private void OnTriggerExit(Collider other)
    {
        var player = other.GetComponent<Player>();
        if (!player) return;
        NavAgent.SetDestination(transform.position);
    }
}