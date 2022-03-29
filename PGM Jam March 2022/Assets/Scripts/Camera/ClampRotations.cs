using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class ClampRotations : MonoBehaviour
{
    [SerializeField] private float _maxRotation = 90f;
    [SerializeField] private float _minRotation = -90f;
    
    private float _rotationY;
    
    void Update()
    {
        _rotationY = Mathf.Clamp(_rotationY, _minRotation, _maxRotation);
        
        transform.rotation = quaternion.Euler(_rotationY, _rotationY, transform.rotation.z);
    }
}
