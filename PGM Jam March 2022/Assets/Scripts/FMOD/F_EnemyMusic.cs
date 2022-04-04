using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class F_EnemyMusic : MonoBehaviour
{
    [SerializeField]
    [EventRef]
    private string _enemyMusic;

    private EventInstance _enemyMusicInst;

    private EventDescription _eventDes;

    private PARAMETER_DESCRIPTION _chasedParam;
    private PARAMETER_DESCRIPTION _distanceParam;

    private Player _player;
    private Phantom _phantom;

    [SerializeField]
    private F_MusicPlayer _fMusicPlayer;

    void Start()
    {
        _enemyMusicInst = RuntimeManager.CreateInstance(_enemyMusic);
        _enemyMusicInst.start();
        _enemyMusicInst.release();

        _eventDes = RuntimeManager.GetEventDescription(_enemyMusic);
        _eventDes.getParameterDescriptionByName("Chased", out _chasedParam);
        _eventDes.getParameterDescriptionByName("distance", out _distanceParam);

        _player = FindObjectOfType<Player>();
        _phantom = GetComponent<Phantom>();
    }

    private void Update()
    {
        float distance = Vector3.Distance(_player.transform.position, _phantom.transform.position);
        if (distance <= 45 && distance >= 3)
        {
            //Distance Parameter in FMOD is set between 15 and 1.
            _enemyMusicInst.setParameterByID(_distanceParam.id, distance / 3, false);
            _fMusicPlayer.SetAmbVolume();
        }
        else if (distance > 45)
            _enemyMusicInst.setParameterByID(_distanceParam.id, 16f);
    }

    public void ChasedParameterDanger()
    {
        _enemyMusicInst.setParameterByID(_chasedParam.id, 1);
    }

    public void ChasedParameterSafe()
    {
        _enemyMusicInst.setParameterByID(_chasedParam.id, 0);
    }

    private void OnDestroy()
    {
        _enemyMusicInst.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }
}
