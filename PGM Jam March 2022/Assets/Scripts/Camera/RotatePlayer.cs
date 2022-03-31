using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePlayer : MonoBehaviour
{
    [SerializeField] private float _mouseSensitivity = 100f;

    private Transform _player;
    private float _xRotation = 0f;

    [SerializeField] private bool _rotationIsActive;

    private void Start()
    {
        _player = FindObjectOfType<Player>().transform;
        _rotationIsActive = true;
    }

    private void OnEnable()
    {
        GameManager.OnDeactivatePlayerControl += SetRotationOff;
        GameManager.OnActivatePlayerControl += SetRotationOn;
    }

    private void OnDisable()
    {
        GameManager.OnDeactivatePlayerControl -= SetRotationOff;
        GameManager.OnActivatePlayerControl -= SetRotationOn;
    }

    void Update()
    {
        if (_rotationIsActive)
            _player.transform.rotation =
                Quaternion.Euler(_player.transform.eulerAngles.x, transform.eulerAngles.y,
                    _player.transform.eulerAngles.z);
    }

    public void SetRotationOff()
    {
        _rotationIsActive = false;
    }

    public void SetRotationOn()
    {
        _rotationIsActive = true;
    }
}