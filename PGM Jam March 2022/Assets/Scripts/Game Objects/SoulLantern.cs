using System.Collections;
using UnityEngine;

public class SoulLantern : Lantern
{
    [SerializeField] private int _areaToMoveTo = 1;
    [SerializeField] private bool _movesPhantom;
    [SerializeField] float _timeToReturnToPlayer = 7f;

    public override void TurnOn()
    {
        base.TurnOn();

        if (_movesPhantom)
        {
            GameManager.Instance.SwitchPhantomCam();
            GameManager.Instance.MovePhantom(_areaToMoveTo);
            StartCoroutine(ReturnToPlayerCam());
        }
    }

    private IEnumerator ReturnToPlayerCam()
    {
        yield return new WaitForSeconds(_timeToReturnToPlayer);
        GameManager.Instance.SwitchPlayerCam();
    }
}