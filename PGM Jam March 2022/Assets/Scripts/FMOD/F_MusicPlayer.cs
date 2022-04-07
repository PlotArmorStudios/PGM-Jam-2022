using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class F_MusicPlayer : MonoBehaviour
{
    [SerializeField]
    [EventRef]
    public string Music;

    private EventInstance _musicInst;

    private EventDescription _eventDes;

    private PARAMETER_DESCRIPTION _lonelyParamDes;
    private PARAMETER_DESCRIPTION _menuVsGamplayParamDes;

    public static F_MusicPlayer instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        _musicInst = RuntimeManager.CreateInstance(Music);
        _eventDes = RuntimeManager.GetEventDescription(Music);
        _eventDes.getParameterDescriptionByName("Lonely", out _lonelyParamDes);
        _eventDes.getParameterDescriptionByName("MenuVsGameplay", out _menuVsGamplayParamDes);
    }

    void Start()
    {
        _musicInst.start();
    }

    public void SetMenuVsGameplay(float value)
    {
        _musicInst.setParameterByID(_menuVsGamplayParamDes.id, value);
    }

    public void SetLonely(float value)
    {
        _musicInst.setParameterByID(_lonelyParamDes.id, value);
    }

    void OnDestroy()
    {
        _musicInst.release();
        _musicInst.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }
}
