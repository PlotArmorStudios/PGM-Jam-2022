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

    [ContextMenu("Set Destination")]
    public void TestNavDestination()
    {
        NavAgent.SetDestination(InitialPosition);
    }
    
    public void MoveToArea(int area)
    {
#if MovePhantom
        Debug.Log($"Move Phantom to area {area}");
        Debug.Log($"Initial position before change is {InitialPosition}");
        Debug.Log($"Initial position to be set is {GameManager.Instance.PhantomMovePoints[area - 1].position}");
#endif
        
        InitialPosition = GameManager.Instance.PhantomMovePoints[area - 1].position;

#if MovePhantom
        Debug.Log($"Initial position after change is {InitialPosition}");
#endif
        StateMachine.InitializeStates();
        
        NavAgent.speed = 20;
        //NavAgent.SetDestination(GameManager.Instance.PhantomMovePoints[area - 1].position);
    }

    private void OnTriggerStay(Collider other)
    {
        var player = other.GetComponent<Player>();
        if (!player) return;
    }

    private void OnTriggerExit(Collider other)
    {
        var player = other.GetComponent<Player>();
        if (!player) return;
    }
}