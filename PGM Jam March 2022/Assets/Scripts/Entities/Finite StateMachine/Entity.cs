using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Entity : MonoBehaviour
{
    [SerializeField] protected float _detectionRadius;
    [SerializeField] protected float _attackRadius;
    [SerializeField] protected float _attackDelay;
    [SerializeField] protected float _returnHomeTime;
    [SerializeField] protected float _homeRadius;
    [SerializeField] protected float _speed;
    [SerializeField] protected float _patrolSpeed;
    
    public Rigidbody Rigidbody { get; private set; }
    public NavMeshAgent NavAgent { get; protected set; }
    public Player PlayerTarget { get; private set; }
    public Animator Animator { get; private set; }
    public Vector3 InitialPosition { get; protected set; }
    public EntityStateMachine StateMachine { get; private set; }
    public FieldOfView FieldOfView { get; private set; }
    
    //Public access to AI values
    public float HomeRadius => _homeRadius;
    public float ReturnHomeTime => _returnHomeTime;
    public float AttackDelay => _attackDelay;
    public float DetectionRadius => _detectionRadius;
    public float AttackRadius => _attackRadius;
    public float Speed => _speed;
    public float PatrolSpeed => _patrolSpeed;

    private float _attackTimer;
    private bool _canResetNavMesh;

    private void Awake()
    {
        PlayerTarget = FindObjectOfType<Player>();
        NavAgent = GetComponent<NavMeshAgent>();
        Animator = GetComponentInChildren<Animator>();
        StateMachine = GetComponent<EntityStateMachine>();
        Rigidbody = GetComponent<Rigidbody>();
        FieldOfView = GetComponent<FieldOfView>();
            InitialPosition = transform.position;
    }
}