using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class F_EnemyMusic : MonoBehaviour
{
    [SerializeField] [EventRef] private string _enemyMusic;

    private EventInstance _enemyMusicInst;

    private EventDescription _eventDes;

    private PARAMETER_DESCRIPTION _distanceParam;

    
    private Player _player;
    private Phantom _phantom;
    
    [SerializeField] private F_MusicPlayer _fMusicPlayer;

    void Start()
    {
        _enemyMusicInst = RuntimeManager.CreateInstance(_enemyMusic);
        _enemyMusicInst.start();

        _eventDes = RuntimeManager.GetEventDescription(_enemyMusic);
        _eventDes.getParameterDescriptionByName("distance", out _distanceParam);

        _player = FindObjectOfType<Player>();
        _phantom = GetComponent<Phantom>();
    }

    private void Update()
    {
        float distance = Vector3.Distance(_player.transform.position, _phantom.transform.position);
        if (distance <= 75 && distance >= 3)
        {
            //Distance Parameter in FMOD is set between 25 and 1.
            _enemyMusicInst.setParameterByID(_distanceParam.id, distance / 3, false);
        }
        else if (distance > 45)
        {
            //Distance Parameter fades out Phantom music between 25 and 30 with 30 being no volume.
            _enemyMusicInst.setParameterByID(_distanceParam.id, 30f);
        }

        if (distance <= 75 && distance > 50)
            _fMusicPlayer.SetLonely(1f);
        if (distance <= 50 && distance > 25)
            _fMusicPlayer.SetLonely(2f);
        if (distance <= 25)
            _fMusicPlayer.SetLonely(3f);
        else if (distance > 75)
            _fMusicPlayer.SetLonely(0f);
    }

    [ContextMenu("Context Menu Log Example")]
    public void LogExample()
    {
        Debug.Log("This menu item will trigger this log in play mode.");
    }

    [ContextMenu("Set Music")]
    private void SetMusicDangerInstance()
    {
        _enemyMusicInst.setParameterByID(_distanceParam.id, 0, false);
        _fMusicPlayer.SetLonely(3f);
    }

    [ContextMenu("Reset Music")]
    private void ResetMusicInstance()
    {
        _enemyMusicInst.setParameterByID(_distanceParam.id, 30f);
        _fMusicPlayer.SetLonely(0f);
    }

    private void OnDestroy()
    {
        _enemyMusicInst.release();
        _enemyMusicInst.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }
}