using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [SerializeField] private Transform _feet;
    [SerializeField] private float _groundDistance = 2f;
    [SerializeField] private LayerMask _groundLayerMask;

    private bool _isGrounded;

    private void Update()
    {
        ToggleGroundedState();
    }

    public bool ToggleGroundedState()
    {
        _isGrounded = Physics.CheckSphere(_feet.position, _groundDistance, _groundLayerMask);
        return _isGrounded;
    }
}