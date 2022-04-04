using UnityEngine;
using UnityEngine.AI;

public class ChasePlayer : IState
{
    private readonly NavMeshAgent _navMeshAgent;
    private readonly Player _player;
    private float _attackTimer;
    private Animator _animator;
    private Entity _entity;
    private float _speed;

    private F_EnemyMusic _fEnemyMusic;

    public ChasePlayer(Entity entity, Player player, NavMeshAgent navMeshAgent, F_EnemyMusic fEnemyMusic)
    {
        _entity = entity;
        _player = player;
        _speed = _entity.Speed;

        _navMeshAgent = _entity.NavAgent;
        _animator = _entity.Animator;

        _fEnemyMusic = fEnemyMusic;
    }

    public void Tick()
    {
        FollowPlayer();
    }

    public void OnEnter()
    {
        _navMeshAgent.enabled = true;
    }

    public void OnExit()
    {
        _navMeshAgent.enabled = false;
        _fEnemyMusic.ChasedParameterSafe();
    }

    void FollowPlayer()
    {
        if (_player)
        {
            _navMeshAgent.isStopped = false;
            _navMeshAgent.speed = _speed;
            _attackTimer = 0;
            _animator.SetBool("Attacking", false);
            _animator.SetBool("Running", true);
            _entity.transform.rotation = Quaternion.Slerp(_entity.transform.rotation,
                Quaternion.LookRotation(_player.transform.position - _entity.transform.position), 5f * Time.deltaTime);
            _navMeshAgent.SetDestination(_player.transform.position);
            _fEnemyMusic.ChasedParameterDanger();
        }
    }
}