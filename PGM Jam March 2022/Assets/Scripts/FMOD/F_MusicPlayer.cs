using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class F_MusicPlayer : MonoBehaviour
{
    [SerializeField]
    [EventRef]
    private string _ambientMusic;

    private EventInstance _musicInst;

    private EventDescription _eventDes;

    private PARAMETER_DESCRIPTION _paramDes;

    void Start()
    {
        _musicInst = RuntimeManager.CreateInstance(_ambientMusic);
        _musicInst.start();

        _eventDes = RuntimeManager.GetEventDescription(_ambientMusic);
        _eventDes.getParameterDescriptionByName("Lonely", out _paramDes);
    }

    public void SetLonely(float value)
    {
        _musicInst.setParameterByID(_paramDes.id, value);
    }

    void OnDestroy()
    {
        _musicInst.release();
        _musicInst.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }
}
