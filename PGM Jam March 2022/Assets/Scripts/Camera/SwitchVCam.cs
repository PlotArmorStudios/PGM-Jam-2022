using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class SwitchVCam : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _playerCam;
    [SerializeField] private CinemachineVirtualCamera _phantomCam;

    private void OnEnable()
    {
        CameraSwitcher.Register(_playerCam);
        CameraSwitcher.Register(_phantomCam);
        CameraSwitcher.SwitchCamera(_playerCam);
        GameManager.OnSwitchToPlayerCam += SwitchToPlayerCam;
        GameManager.OnSwitchToPhantomCam += SwitchToPhantomCam;
    }

    private void OnDisable()
    {
        CameraSwitcher.Unregister(_playerCam);
        CameraSwitcher.Unregister(_phantomCam);
        GameManager.OnSwitchToPlayerCam -= SwitchToPlayerCam;
        GameManager.OnSwitchToPhantomCam -= SwitchToPhantomCam;
    }

    private void SwitchCams()
    {
        if (CameraSwitcher.IsActiveCamera(_playerCam))
            CameraSwitcher.SwitchCamera(_phantomCam);
        else if (CameraSwitcher.IsActiveCamera(_phantomCam))
            CameraSwitcher.SwitchCamera(_playerCam);
    }

    private void SwitchToPlayerCam() => CameraSwitcher.SwitchCamera(_playerCam);

    private void SwitchToPhantomCam() => CameraSwitcher.SwitchCamera(_phantomCam);

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (CameraSwitcher.IsActiveCamera(_playerCam))
                CameraSwitcher.SwitchCamera(_phantomCam);
            else if (CameraSwitcher.IsActiveCamera(_phantomCam))
                CameraSwitcher.SwitchCamera(_playerCam);
        }
    }
}