using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePlayer : MonoBehaviour
{
    [SerializeField] private float _mouseSensitivity = 100f;
    
    private Transform _player;
    private float _xRotation = 0f;

    private void Start()
    {
        _player = FindObjectOfType<Player>().transform;
    }

    void Update()
    {
        _player.transform.rotation =
            Quaternion.Euler(_player.transform.eulerAngles.x, transform.eulerAngles.y, _player.transform.eulerAngles.z);
    }
}
