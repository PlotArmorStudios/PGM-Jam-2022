using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    [SerializeField] private LayerMask _targetMask;
    [SerializeField] private LayerMask _obstructionMask;

    [Range(0, 360)] public float Angle;
    public float Radius;
    public Player Player;
    public bool CanSeePlayer;

    private Entity _entity;

    [SerializeField]
    private F_EnemyMusic _enemyMusic;

    private void Start()
    {
        _entity = GetComponent<Entity>();
        Player = _entity.PlayerTarget;
        StartCoroutine(FOVRoutine());
    }


    private IEnumerator FOVRoutine()
    {
        float delay = 0.2f;

        WaitForSeconds wait = new WaitForSeconds(delay);

        while (true)
        {
            yield return wait;
            FieldOfViewCheck();
        }
    }

    private void FieldOfViewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, Radius, _targetMask);

        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < Angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, _obstructionMask))
                    CanSeePlayer = true;
                else
                    CanSeePlayer = false;
            }
            else
            {
                CanSeePlayer = false;
            }
        }
        else if (CanSeePlayer)
            CanSeePlayer = false;
        _enemyMusic.ChasedParameterSafe();
    }
}