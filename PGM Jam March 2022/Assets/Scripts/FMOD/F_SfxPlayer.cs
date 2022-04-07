using UnityEngine;
using FMODUnity;

public class F_SfxPlayer : MonoBehaviour
{
    private void Start()
    {
        RuntimeManager.PlayOneShotAttached("event:/Intro", gameObject);
    }

    public void PlayPlayerFootsteps()
    {
        RuntimeManager.PlayOneShotAttached("event:/Footsteps", gameObject);
    }

    public void PlayPhantomFootsteps()
    {
        RuntimeManager.PlayOneShotAttached("event:/Phantom Footsteps", gameObject);
    }
}
