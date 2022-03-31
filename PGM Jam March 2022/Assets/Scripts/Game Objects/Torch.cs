using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Torch : MonoBehaviour
{
    [SerializeField] private Volume _torchVolume;
    [SerializeField] private float _maxLuminosity = 100;
    [SerializeField] float _rateOfDepletion = 5f;

    private float _currentLuminosity;
    public bool Depleting = true;

    private void Start()
    {
        _currentLuminosity = _maxLuminosity;
    }

    private void Update()
    {
        if (Depleting)
            _currentLuminosity -= Time.deltaTime * _rateOfDepletion;
        
        _torchVolume.weight = _currentLuminosity / _maxLuminosity;
    }

    [ContextMenu("Set Lumi")]
    public void SetLuminosity()
    {
        _currentLuminosity = _maxLuminosity;
    }
}