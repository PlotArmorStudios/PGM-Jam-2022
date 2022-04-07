using UnityEngine;
using FMODUnity;

public class F_SetSceneMusic : MonoBehaviour
{
    [SerializeField]
    private float _menuVsGameplay;

    //0 = Menu  .   1 = Gameplay

    private void Start()
    {
        F_MusicPlayer.instance.SetMenuVsGameplay(_menuVsGameplay);
    }
}
