using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerCounter : MonoBehaviour
{
    [SerializeField] private TMP_Text _timerText;

    private void OnEnable()
    {
        GameManager.OnTurnOnLanterns += ActivateTimer;
        GameManager.OnTurnOffLanterns += DeactivateTimer;
    }
    
    private void OnDisable()
    {
        GameManager.OnTurnOnLanterns -= ActivateTimer;
        GameManager.OnTurnOffLanterns -= DeactivateTimer;
    }

    private void ActivateTimer()
    {
        _timerText.gameObject.SetActive(true);
        StartCoroutine(DecreaseTimer());
    }

    private IEnumerator DecreaseTimer()
    {
        while (GameManager.Instance.CurrentLanternTime > 0)
        {
            _timerText.text = "Lantern time = " + GameManager.Instance.CurrentLanternTime.ToString("0");
            yield return null;
        }
    }

    private void DeactivateTimer()
    {
        StopCoroutine(DecreaseTimer());
        _timerText.gameObject.SetActive(false);
    }
}
