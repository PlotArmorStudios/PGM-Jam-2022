using UnityEngine;
using UnityEngine.AI;

public class AvoidPlayer : IState
{
    private readonly NavMeshAgent _navMeshAgent;
    private readonly Player _player;
    private float _attackTimer;
    private Animator _animator;
    private Entity _entity;
    private float _speed;

    public AvoidPlayer(Entity entity, Player player, NavMeshAgent navMeshAgent)
    {
        _entity = entity;
        _player = player;
        _speed = _entity.Speed;

        _navMeshAgent = _entity.NavAgent;
        _animator = _entity.Animator;
    }

    public void OnEnter()
    {
        _navMeshAgent.enabled = true;
    }

    public void OnExit()
    {
        _navMeshAgent.enabled = false;
        Debug.Log("Disable navmesh");
    }

    public void Tick()
    {
        GoAwayFromPlayer();
    }

    void GoAwayFromPlayer()
    {
        if (_player)
        {
            _navMeshAgent.isStopped = false;
            _navMeshAgent.speed = _speed;
            _attackTimer = 0;
            _animator.SetBool("Attack", false);
            _animator.SetBool("Running", true);
            _entity.transform.rotation = Quaternion.Slerp(_entity.transform.rotation,
                Quaternion.LookRotation(_entity.transform.position - _player.transform.position), 5f * Time.deltaTime);
            Vector3 awayDirection = _player.transform.position + _entity.transform.position;
            _navMeshAgent.SetDestination(awayDirection * 2f);
        }
    }
}