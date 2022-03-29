using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhantomAttack : MonoBehaviour
{
    public static event Action<float> OnPhantomAttack;
    [SerializeField] private float _attackRange = 10;
    [SerializeField] private float _attackDamage = 25;

    private Entity _entity;

    private void Start()
    {
        _entity = GetComponentInParent<Entity>();
    }

    public void Attack()
    {
        if (Vector3.Distance(transform.position, _entity.PlayerTarget.transform.position) <= _attackRange)
        {
            Debug.Log("Phantom Attack trigger");
            OnPhantomAttack?.Invoke(_attackDamage);
        }
    }
}