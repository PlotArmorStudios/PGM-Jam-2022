using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed = 6f;
    [SerializeField] private float _jumpHeight = 10f;
    [SerializeField] private int _maxJumps = 1;

    private CharacterController _characterController;
    private GroundCheck _groundCheck;
    private Vector3 _verticalVelocity;
    
    private float _horizontal;
    private float _vertical;

    public float _gravity = -9.81f;
    private int _jumpsRemaining;
    private Animator _animator;
    public PlayerHealth Health { get; set; }

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _groundCheck = GetComponent<GroundCheck>();
        _jumpsRemaining = _maxJumps;
        _animator = GetComponentInChildren<Animator>();
        Health = GetComponent<PlayerHealth>();
    }

    private void Update()
    {
        if (_groundCheck.ToggleGroundedState() && _verticalVelocity.y < 0)
        {
            _verticalVelocity.y = -2f;
            _jumpsRemaining = _maxJumps;
        }
        
        _horizontal = Input.GetAxis("Horizontal");
        _vertical= Input.GetAxis("Vertical");

        Vector3 movement = transform.right * _horizontal + transform.forward * _vertical;

        _animator.SetBool("Walking", movement.magnitude > .1f);
        
        _characterController.Move(movement * _speed * Time.deltaTime);


        if (Input.GetButtonDown("Jump") && _jumpsRemaining > 0)
        {
            _verticalVelocity.y = Mathf.Sqrt(_jumpHeight * -2f * _gravity);
            _jumpsRemaining--;
        }
        
        _verticalVelocity.y += _gravity * Time.deltaTime;

        _characterController.Move(_verticalVelocity * Time.deltaTime);
    }
}