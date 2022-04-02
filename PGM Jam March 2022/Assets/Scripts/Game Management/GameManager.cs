//#define DebugShardChecker
//#define DebugLanternTimer
//#define DebugShardText

using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static event Action<int> OnCollectShard;
    public static event Action<int> OnResetShards;
    public static event Action OnWarnTooLow;
    public static event Action OnTurnOnLanterns;
    public static event Action OnTurnOffLanterns;
    public static event Action<int> OnMovePhantom;
    public static event Action OnSwitchToPhantomCam;
    public static event Action OnSwitchToPlayerCam;
    public static event Action OnDeactivatePlayerControl;
    public static event Action OnActivatePlayerControl;

    public float LanternLightDuration;
    public int RequiredShardsToCollect { get; set; }

    public static GameManager Instance;
    [SerializeField] private List<Transform> phantomMovePoints;

    public List<Transform> PhantomMovePoints => phantomMovePoints;

    private float _currentLanternTime;
    private bool _timerIsOn;

    public int NumberOfShards { get; set; }
    public float CurrentLanternTime => _currentLanternTime;

    public bool Paused { get; set; }
    void Awake() => Instance = this;

    private void OnEnable()
    {
        OnTurnOnLanterns += StartTimer;
        OnTurnOffLanterns += EndTimer;
        _currentLanternTime = LanternLightDuration;
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
        if (_timerIsOn) _currentLanternTime -= Time.deltaTime;

        if (_currentLanternTime <= 0)
        {
#if DebugLanternTimer
            Debug.Log("Lantern time ran out");
#endif
            OnTurnOffLanterns?.Invoke();
            _currentLanternTime = LanternLightDuration;
        }
    }

    public void CollectShard()
    {
        NumberOfShards++;
        OnCollectShard?.Invoke(NumberOfShards);
    }

    public bool CheckShardsCollected()
    {
        if (NumberOfShards >= RequiredShardsToCollect)
        {
#if DebugShardChecker
            Debug.Log("Turned all lights on");
#endif

            OnTurnOnLanterns?.Invoke();
            NumberOfShards = 0;
            OnResetShards?.Invoke(NumberOfShards);
            return true;
        }
        else
        {
#if DebugShardChecker
            Debug.Log("Not enough shards");
#endif
            return false;
        }
    }

    public void DeactivatePlayer()
    {
        OnDeactivatePlayerControl?.Invoke();
    }
    public void ActivatePlayer()
    {
        OnActivatePlayerControl?.Invoke();
    }

    public void UnpauseGame()
    {
        Time.timeScale = 1f;
        Paused = false;
    }
    
    public void PauseGame()
    {
        Time.timeScale = 0f;
        Paused = true;
    }

}