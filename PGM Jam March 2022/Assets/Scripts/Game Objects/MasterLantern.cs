using System;
using UnityEngine;

public class MasterLantern : Lantern
{
    private ParticleSystem _particleSystem;
    private bool _playerInRange;

    private void Start()
    {
        _particleSystem = GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && _playerInRange)
        {
            Purify();
        }
    }

    private void Purify()
    {
        _particleSystem.Play();
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Player>();
        if (!player) return;
        _playerInRange = true;
    }
    private void OnTriggerExit(Collider other)
    {
        var player = other.GetComponent<Player>();
        if (!player) return;
        _playerInRange = false;
    }
}