//#define DEBUG_LOG

using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class EntityStateMachine : MonoBehaviour
{
    private StateMachine _stateMachine;
    private NavMeshAgent _navMeshAgent;
    private Player _player;
    private Entity _entity;

    private Idle _idle;
    private ChasePlayer _chasePlayer;
    private AvoidPlayer _avoidPlayer;
    private Attack _attack;
    private Dead _dead;
    private ReturnHome _returnHome;
    private Patrol _patrol;

    public IState CurrentState => _stateMachine.CurrentState;
    public bool IsHome => Vector3.Distance(_entity.transform.position, _entity.InitialPosition) < 4;
    public bool CanSeePlayer => _entity.FieldOfView.CanSeePlayer && _entity.PlayerTarget.Health.IsAlive;
    private float DistanceToPlayer => Vector3.Distance(_navMeshAgent.transform.position, _player.transform.position);
    public bool Patrolling => _idle.TogglePatrol();

    private F_EnemyMusic _fEnemyMusic; 

    private void Start()
    {
        _entity = GetComponent<Entity>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _player = FindObjectOfType<Player>();

        _stateMachine = new StateMachine();

        InitializeStates();

        AddStateTransitions();

        //Set default state
        _stateMachine.SetState(_idle);

    }


    public void InitializeStates()
    {
        _idle = new Idle(_entity);
        _chasePlayer = new ChasePlayer(_entity, _player, _navMeshAgent);
        _avoidPlayer = new AvoidPlayer(_entity, _player, _navMeshAgent);
        _attack = new Attack(_entity, _player);
        _dead = new Dead(_entity);
        _returnHome = new ReturnHome(_entity);
        _patrol = new Patrol(_entity);
    }

    private void AddStateTransitions()
    {
        _stateMachine.AddTransition(
            _idle,
            _patrol,
            () => Patrolling);

        _stateMachine.AddTransition(
            _patrol,
            _avoidPlayer,
            () => DistanceToPlayer < _entity.FieldOfView.Radius && CanSeePlayer && Torch.TorchVolumeWeight > Torch.TorchFractionToAttack);

        _stateMachine.AddTransition(
            _avoidPlayer,
            _patrol,
            () => DistanceToPlayer > _entity.FieldOfView.Radius * Torch.TorchVolumeWeight);

        _stateMachine.AddTransition(
            _idle,
            _chasePlayer,
            () => DistanceToPlayer < _entity.FieldOfView.Radius && CanSeePlayer);

        _stateMachine.AddTransition(
            _chasePlayer,
            _idle,
            () => DistanceToPlayer > _entity.FieldOfView.Radius && Torch.TorchVolumeWeight <= Torch.TorchFractionToAttack);
     
        _stateMachine.AddTransition(
            _patrol,
            _chasePlayer,
            () => DistanceToPlayer < _entity.FieldOfView.Radius && CanSeePlayer && Torch.TorchVolumeWeight <= Torch.TorchFractionToAttack);

        _stateMachine.AddTransition(
            _chasePlayer,
            _attack,
            () => DistanceToPlayer <= _entity.AttackRadius && CanSeePlayer);

        _stateMachine.AddTransition(
            _attack,
            _chasePlayer,
            () => DistanceToPlayer > _entity.AttackRadius && CanSeePlayer);
        
        _stateMachine.AddTransition(
            _attack,
            _returnHome,
            () => Torch.TorchVolumeWeight > Torch.TorchFractionToAttack || GameManager.Instance.PlayerInDialogue);
        
        _stateMachine.AddTransition(
            _attack,
            _idle,
            () => DistanceToPlayer > _entity.AttackRadius || !_entity.PlayerTarget.Health.IsAlive);
        
        _stateMachine.AddTransition(
            _idle,
            _returnHome,
            () => !IsHome && _idle.UpdateReturnHomeTime());
        
        _stateMachine.AddTransition(
            _patrol,
            _returnHome,
            () => !IsHome);
        
        _stateMachine.AddTransition(
            _returnHome,
            _idle,
            () => IsHome);

        _stateMachine.AddTransition(
            _returnHome,
            _chasePlayer,
            () => DistanceToPlayer < _entity.FieldOfView.Radius && Torch.TorchVolumeWeight <= Torch.TorchFractionToAttack);
        
        _stateMachine.AddTransition(
            _chasePlayer,
            _returnHome,
            () => GameManager.Instance.PlayerInDialogue);

        //_stateMachine.AddAnyTransition(_dead, () => _entity.Health.CurrentHealthValue <= 0);
    }

    private void Update()
    {
#if DEBUG_LOG
        Debug.Log("Enemy is patrolling: " + Patrolling);
#endif
        _stateMachine.Tick();
    }
}