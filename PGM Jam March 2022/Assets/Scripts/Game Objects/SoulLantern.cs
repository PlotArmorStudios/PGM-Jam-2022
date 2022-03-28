using UnityEngine;

public class SoulLantern : Lantern
{
    [SerializeField] private int _areaToMoveTo = 1;
    [SerializeField] private bool _movesPhantom;

    public override void TurnOn()
    {
        base.TurnOn();

        if (_movesPhantom)
        {
            GameManager.Instance.MovePhantom(_areaToMoveTo);
        }
    }
}