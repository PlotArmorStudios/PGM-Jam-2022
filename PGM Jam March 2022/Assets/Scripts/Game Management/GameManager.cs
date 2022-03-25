#define DebugLanternTimer
#define DebugShardText
using System;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static event Action<int> OnCollectShard;
    public static event Action OnWarnTooLow;
    public static event Action OnTurnOnLanterns;
    public static event Action OnTurnOffLanterns;

    public float LanternLightDuration;

    public static GameManager Instance;
    private float _currentLanternTime;
    private bool _timerIsOn;
    public int RequiredShardsToCollect { get; set; }
    public int NumberOfShards { get; set; }

    void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        OnTurnOnLanterns += StartTimer;
    }

    private void StartTimer()
    {
        _timerIsOn = true;
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
        if (NumberOfShards == RequiredShardsToCollect) OnTurnOnLanterns?.Invoke();
        else OnWarnTooLow?.Invoke();
    }
}


