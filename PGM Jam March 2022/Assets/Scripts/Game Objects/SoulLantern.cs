public class SoulLantern : Lantern
{
    private void OnEnable()
    {
        GameManager.OnTurnOnLanterns += TurnOn;
        GameManager.OnTurnOffLanterns += TurnOff;
    }

    private void OnDisable()
    {
        GameManager.OnTurnOnLanterns -= TurnOn;
        GameManager.OnTurnOffLanterns -= TurnOff;
    }
}