public class TogglePlayerComponents : ToggleComponents
{
    private void OnEnable()
    {
        GameManager.OnSwitchToPlayerCam += ToggleOnComponents;
        GameManager.OnSwitchToPhantomCam += ToggleOffComponents;
        GameManager.OnActivatePlayerControl += ToggleOnComponents;
        GameManager.OnDeactivatePlayerControl += ToggleOffComponents;
    }
    
    private void OnDisable()
    {
        GameManager.OnSwitchToPlayerCam -= ToggleOnComponents;
        GameManager.OnSwitchToPhantomCam -= ToggleOffComponents;
        GameManager.OnActivatePlayerControl -= ToggleOnComponents;
        GameManager.OnDeactivatePlayerControl -= ToggleOffComponents;
    }
}