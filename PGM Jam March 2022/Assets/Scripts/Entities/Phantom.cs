using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof (NavMeshAgent))]
public class Phantom : MonoBehaviour
{
    [SerializeField] private float _playerDetectDistance;
    [SerializeField] private LayerMask _playerLayer;

    private PlayerControl _player;
    private NavMeshAgent _navAgent;
    private bool _playerInRange;

    private void Start()
    {
        _navAgent = GetComponent<NavMeshAgent>();
        _player = FindObjectOfType<PlayerControl>();
    }

    private void OnTriggerStay(Collider other)
    {
        _navAgent.SetDestination(_player.transform.position);
    }

    private void OnTriggerExit(Collider other)
    {
        _navAgent.SetDestination(transform.position);
    }
}