#define DebugLights
#define DebugShard
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;

public class SoulShard : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<PlayerControl>();

        if (!player) return;
#if DebugShard
        Debug.Log("Picked up shard.");
#endif
        GameManager.Instance.CollectShard();
        gameObject.SetActive(false);
    }
}