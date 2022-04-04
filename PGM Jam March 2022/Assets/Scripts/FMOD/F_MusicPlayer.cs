using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class F_MusicPlayer : MonoBehaviour
{
    [SerializeField]
    [EventRef]
    private string _gameplayMusic;

    private EventInstance _musicInst;

    void Start()
    {
        _musicInst = RuntimeManager.CreateInstance(_gameplayMusic);
        _musicInst.start();
        _musicInst.release();
    }

    public void SetAmbVolume()
    {
        _musicInst.setVolume(0f);
    }

    void OnDestroy()
    {
        _musicInst.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }
}
