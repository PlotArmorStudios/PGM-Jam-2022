using UnityEngine;
using FMODUnity;

public class F_DialogueSfx : MonoBehaviour
{
    private float _tutorial = 1f;
    private float _l1 = 1f;
    private float _l2 = 1f;
    private float _l3 = 1f;

    public void PlayDialogueEnter()
    {
        RuntimeManager.PlayOneShotAttached("event:/Dialogue Enter", gameObject);
    }

    public void PlayDialogueExit()
    {
        RuntimeManager.PlayOneShotAttached("event:/Dialogue Exit", gameObject);
    }

    public void MoreShardsDialogue()
    {
        PlayDialogueExit();
    }

    public void Lantern0Dialogue()
    {
        _tutorial = _tutorial + 1;

        if (_tutorial <= 7)
            RuntimeManager.PlayOneShotAttached("event:/UI Click", gameObject);
        else if (_tutorial >= 8)
        {
            RuntimeManager.PlayOneShotAttached("event:/UI Click", gameObject);
            PlayDialogueExit();
        }
    }

    public void Lantern1Dialogue()
    {
        _l1 = _l1 + 1;

        if (_l1 <= 7)
            RuntimeManager.PlayOneShotAttached("event:/UI Click", gameObject);
        else if (_l1 >= 8)
        {
            RuntimeManager.PlayOneShotAttached("event:/UI Click", gameObject);
            PlayDialogueExit();
        }
    }

    public void Lantern2Dialogue()
    {
        _l2 = _l2 + 1;

        if (_l2 <= 13)
            RuntimeManager.PlayOneShotAttached("event:/UI Click", gameObject);
        else if (_l2 >= 14)
        {
            RuntimeManager.PlayOneShotAttached("event:/UI Click", gameObject);
            PlayDialogueExit();
        }
    }

    public void Lantern3Dialogue()
    {
        _l3 = _l3 + 1;

        if (_l3 <= 13)
            RuntimeManager.PlayOneShotAttached("event:/UI Click", gameObject);
        else if (_l3 >= 14)
        {
            RuntimeManager.PlayOneShotAttached("event:/UI Click", gameObject);
            PlayDialogueExit();
        }
    }
}
