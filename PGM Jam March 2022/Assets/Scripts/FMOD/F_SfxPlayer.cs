using UnityEngine;
using FMODUnity;

public class F_SfxPlayer : MonoBehaviour
{
    [SerializeField]
    [EventRef]
    private string _footstepEventPath;

    public void PlayFootsteps()
    {
        RuntimeManager.PlayOneShotAttached(_footstepEventPath, gameObject);
    }
}
