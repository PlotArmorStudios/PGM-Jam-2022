//#define DebugShardChecker
//#define DebugLanternTimer
//#define DebugShardText
using System;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static event Action<int> OnCollectShard;
    public static event Action<int> OnResetShards;
    public static event Action OnWarnTooLow;
    public static event Action OnTurnOnLanterns;
    public static event Action OnTurnOffLanterns;

    public float LanternLightDuration;
    public int RequiredShardsToCollect = 3;

    public static GameManager Instance;
    private float _currentLanternTime;
    private bool _timerIsOn;

    public int NumberOfShards { get; set; }
    public float CurrentLanternTime => _currentLanternTime;

    void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        OnTurnOnLanterns += StartTimer;
        OnTurnOffLanterns += EndTimer;
        _currentLanternTime = LanternLightDuration;
    }

    private void StartTimer()
    {
        _timerIsOn = true;
    }
    
    private void EndTimer()
    {
        _timerIsOn = false;
    }

    private void Update()
    {
        if (_timerIsOn)
        {
            _currentLanternTime -= Time.deltaTime;
        }

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
        CheckShardsCollected();
    }

    private void CheckShardsCollected()
    {
        if (NumberOfShards >= RequiredShardsToCollect)
        {
#if DebugShardChecker
            Debug.Log("Turned all lights on");
#endif
            OnTurnOnLanterns?.Invoke();
            NumberOfShards = 0;
            OnResetShards?.Invoke(NumberOfShards);
        }
        else
        {
#if DebugShardChecker
            Debug.Log("Not enough shards");
#endif
        }
    }
}