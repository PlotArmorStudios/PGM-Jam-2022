public class TogglePhantomComponents : ToggleComponents
{
    private void OnEnable()
    {
        //GameManager.OnTurnOnLanterns += ToggleOffComponents;
        //GameManager.OnTurnOffLanterns += ToggleOnComponents;
    }
    
    private void OnDisable()
    {
        //GameManager.OnTurnOnLanterns -= ToggleOffComponents;
        //GameManager.OnTurnOffLanterns -= ToggleOnComponents;
    }
}