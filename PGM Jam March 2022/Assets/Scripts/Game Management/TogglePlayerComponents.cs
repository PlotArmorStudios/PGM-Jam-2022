public class TogglePlayerComponents : ToggleComponents
{
    private void OnEnable()
    {
        GameManager.OnSwitchToPlayerCam += ToggleOnComponents;
        GameManager.OnSwitchToPhantomCam += ToggleOffComponents;
    }
    
    private void OnDisable()
    {
        GameManager.OnSwitchToPlayerCam -= ToggleOnComponents;
        GameManager.OnSwitchToPhantomCam -= ToggleOffComponents;
    }
}