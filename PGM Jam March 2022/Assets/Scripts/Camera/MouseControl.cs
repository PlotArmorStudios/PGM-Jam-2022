using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseControl : MonoBehaviour
{
    [SerializeField] private float _mouseSensitivity = 100f;
    
    private Transform _player;

    private void Start()
    {
        _player = FindObjectOfType<PlayerControl>().transform;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * _mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * _mouseSensitivity * Time.deltaTime;
        
        _player.Rotate(Vector3.up * mouseX);
    }
}