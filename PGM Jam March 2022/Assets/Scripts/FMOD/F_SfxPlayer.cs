using UnityEngine;
using FMODUnity;

public class F_SfxPlayer : MonoBehaviour
{
    [SerializeField]
    [EventRef]
    private string _footstepEventPath;

    private void Start()
    {
        RuntimeManager.PlayOneShotAttached("event:/Intro", gameObject);
    }

    public void PlayFootsteps()
    {
        RuntimeManager.PlayOneShotAttached(_footstepEventPath, gameObject);
    }
}
