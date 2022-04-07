//#define DebugShardChecker
//#define DebugLanternTimer
//#define DebugShardText

using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    public static event Action<int> OnCollectShard;
    public static event Action<int> OnResetShards;
    public static event Action OnTurnOnLanterns;
    public static event Action OnTurnOffLanterns;
    public static event Action<int> OnMovePhantom;
    public static event Action OnSwitchToPhantomCam;
    public static event Action OnSwitchToPlayerCam;
    public static event Action OnDeactivatePlayerControl;
    public static event Action OnActivatePlayerControl;

    public float LanternLightDuration;
    public int RequiredShardsToCollect;

    public static GameManager Instance;

    [SerializeField] private List<Transform> _phantomMovePoints;
    [SerializeField] private GameObject _pauseUI;

    public List<Transform> PhantomMovePoints => _phantomMovePoints;

    private float _currentLanternTime;
    private bool _timerIsOn;

    public int NumberOfShards { get; set; }
    public bool PlayerInDialogue;
    public float CurrentLanternTime => _currentLanternTime;

    public bool Paused { get; set; }
    void Awake() => Instance = this;

    private void OnEnable()
    {
        OnTurnOnLanterns += StartTimer;
        OnTurnOffLanterns += EndTimer;
        DialogueSection.OnToggleDialogue += TogglePlayerInDialogue;
        _currentLanternTime = LanternLightDuration;
    }
    private void OnDisable()
    {
        OnTurnOnLanterns -= StartTimer;
        OnTurnOffLanterns -= EndTimer;
        DialogueSection.OnToggleDialogue -= TogglePlayerInDialogue;
    }

    public void SwitchPhantomCam() => OnSwitchToPhantomCam?.Invoke();
    public void SwitchPlayerCam() => OnSwitchToPlayerCam?.Invoke();

    public void MovePhantom(int phantomSpawnPoint)
    {
        Debug.Log("Called area to move to event");
        OnMovePhantom?.Invoke(phantomSpawnPoint);
    }

    [ContextMenu("Turn ON Lights")]
    public void TurnOnLights() => OnTurnOnLanterns?.Invoke();

    [ContextMenu("Turn OFF Lights")]
    public void TurnOffLights() => OnTurnOffLanterns?.Invoke();

    private void StartTimer() => _timerIsOn = true;

    private void EndTimer() => _timerIsOn = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && Input.GetKeyDown(KeyCode.P))
            if (Paused) UnpauseGame();
            else PauseGame();
    }

    public void CollectShard()
    {
        NumberOfShards++;
        OnCollectShard?.Invoke(NumberOfShards);
    }

    public void ResetShards()
    {
        NumberOfShards = 0;
        OnResetShards?.Invoke(NumberOfShards);
    }

    public void DeactivatePlayer()
    {
        OnDeactivatePlayerControl?.Invoke();
    }

    public void ActivatePlayer()
    {
        OnActivatePlayerControl?.Invoke();
    }

    public void TogglePlayerInDialogue(bool toggle)
    {
        PlayerInDialogue = toggle;
    }

    public void UnpauseGame()
    {
        ActivatePlayer();
        Time.timeScale = 1f;
        Paused = false;
    }

    public void PauseGame()
    {
        DeactivatePlayer();
        Time.timeScale = 0f;
        Paused = true;
    }
}