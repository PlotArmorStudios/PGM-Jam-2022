//#define DebugTorchVolume
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Torch : MonoBehaviour
{
    [SerializeField] private Volume _torchVolume;
    public static float TorchVolumeWeight { get; private set; }
    [SerializeField] private float _maxLuminosity = 100;
    [SerializeField] float _rateOfDepletion = 5f;

    private float _currentLuminosity;
    public bool Depleting = true;

    private void Start()
    {
        _currentLuminosity = _maxLuminosity;
    }

    private void OnEnable()
    {
        SoulLantern.OnTurnOn += SetLuminosity;
    }

    private void OnDisable()
    {
        SoulLantern.OnTurnOn -= SetLuminosity;
    }

    private void Update()
    {
        if (Depleting)
            _currentLuminosity -= Time.deltaTime * _rateOfDepletion;

        _torchVolume.weight = _currentLuminosity / _maxLuminosity;
        TorchVolumeWeight = Mathf.Clamp(_torchVolume.weight, 0, 1);

#if DebugTorchVolume
        Debug.Log(TorchVolumeWeight);
#endif
    }

    [ContextMenu("Set Lumi")]
    public void SetLuminosity()
    {
        _currentLuminosity = _maxLuminosity;
    }
}