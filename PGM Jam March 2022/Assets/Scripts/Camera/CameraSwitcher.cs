using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public static class CameraSwitcher
{
    private static List<CinemachineVirtualCamera> _cameras = new List<CinemachineVirtualCamera>();
    public static CinemachineVirtualCamera ActiveCamera = null;

    public static bool IsActiveCamera(CinemachineVirtualCamera camera)
    {
        return camera == ActiveCamera;
    }
    
    public static void SwitchCamera(CinemachineVirtualCamera camera)
    {
        camera.Priority = 20;
        ActiveCamera = camera;

        foreach (var cam in _cameras)
        {
            if (cam != camera && cam.Priority != 0)
            {
                cam.Priority = 0;
            }
        }
    }
    
    public static void Register(CinemachineVirtualCamera camera)
    {
        _cameras.Add(camera);
    }

    public static void Unregister(CinemachineVirtualCamera camera)
    {
        _cameras.Remove(camera);
    }
}
