using UnityEngine;
using FMODUnity;

public class F_UISfx : MonoBehaviour
{
    private float _play = 1f;

    void Start()
    {
        
    }

    public void PlayClick()
    {
        RuntimeManager.PlayOneShotAttached("event:/UI Click", gameObject);
    }

    public void PlayDialogueEnter()
    {
        RuntimeManager.PlayOneShotAttached("event:/Dialogue Enter", gameObject);
    }

    public void PlayDialogueExit()
    {
        RuntimeManager.PlayOneShotAttached("event:/Dialogue Exit", gameObject);
    }

    public void PlayDialogueDuo()
    {
        _play = _play + 1;

        if (_play <= 5)
            RuntimeManager.PlayOneShotAttached("event:/Dialogue Duo", gameObject);
        else if (_play >= 6)
            PlayDialogueExit();
    }
}
