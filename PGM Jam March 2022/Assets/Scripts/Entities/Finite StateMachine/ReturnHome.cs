using UnityEngine;
using UnityEngine.AI;

public class ReturnHome : IState
{
    private NavMeshAgent _navMeshAgent;
    private Animator _animator;
    private Vector3 _initialPosition;
    private Entity _entity;

    public ReturnHome(Entity entity)
    {
        _entity = entity;
        _navMeshAgent = entity.NavAgent;
        _initialPosition = new Vector3(entity.InitialPosition.x, entity.InitialPosition.y, entity.InitialPosition.z);
        Debug.Log("New init position is: " + entity.InitialPosition);
        Debug.Log("Position is set to: " + _initialPosition);
        _animator = entity.Animator;
    }

    public void Tick()
    {
        ReturnToStartPosition();
    }

    public void OnEnter()
    {
        _navMeshAgent.enabled = true;
        _animator.SetBool("Running", true);
    }

    public void OnExit()
    {
        _navMeshAgent.enabled = false;
        _animator.SetBool("Running", false);
    }

    private void ReturnToStartPosition()
    {
        Debug.Log($"Position to move to is {_entity.InitialPosition}");
        _navMeshAgent.destination = _entity.InitialPosition;
    }
}