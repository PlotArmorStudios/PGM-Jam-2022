//#define DebugShardText
using TMPro;
using UnityEngine;

public class SoulShardText : MonoBehaviour
{
    private TMP_Text _text;

    private void Start()
    {
        _text = GetComponent<TMP_Text>();
        _text.text = "Shards " + GameManager.Instance.NumberOfShards + "/" + GameManager.Instance.RequiredShardsToCollect;
    }

    private void OnEnable()
    {
        GameManager.OnCollectShard += UpdateShardText;
        GameManager.OnResetShards += ResetShards;
    }

    private void OnDisable()
    {
        GameManager.OnCollectShard -= UpdateShardText;
        GameManager.OnResetShards -= ResetShards;
    }

    private void UpdateShardText(int shardAmount)
    {
#if DebugShardText
        Debug.Log("Shard Text Updated");
#endif
        _text.text = "Shards " + GameManager.Instance.NumberOfShards + "/" + GameManager.Instance.RequiredShardsToCollect;
    }

    private void ResetShards(int shardAmount)
    {
        UpdateShardText(shardAmount);
    }
}