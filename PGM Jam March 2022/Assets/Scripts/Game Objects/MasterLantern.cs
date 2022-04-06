using System;
using UnityEngine;

public class MasterLantern : Lantern
{
    [SerializeField] private GameObject _pressFCanvas;
    private ParticleSystem _particleSystem;
    private bool _playerInRange;
    private SceneLoader _sceneLoader;

    private void Start()
    {
        _particleSystem = GetComponent<ParticleSystem>();
        TurnOff();
        _sceneLoader = GetComponent<SceneLoader>(); 
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && _playerInRange)
        {
            Purify();
        }
    }

    [ContextMenu("Purify")]
    private void Purify()
    {
        _particleSystem.Play();
        TurnOn();
        _sceneLoader.LoadScene("OutroScene");
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Player>();
        if (!player) return;
        _playerInRange = true;
        _pressFCanvas.SetActive(true);
    }
    private void OnTriggerExit(Collider other)
    {
        var player = other.GetComponent<Player>();
        if (!player) return;
        _playerInRange = false;
        _pressFCanvas.SetActive(false);
    }
}