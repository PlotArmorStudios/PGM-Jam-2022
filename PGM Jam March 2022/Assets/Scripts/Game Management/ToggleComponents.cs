using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleComponents : MonoBehaviour
{
    public void ToggleOffComponents()
    {
        var behaviors = GetComponents<Behaviour>();
        foreach (var behavior in behaviors)
        {
            if (behavior != this)
                behavior.enabled = false;
        }
    }

    public void ToggleOnComponents()
    {
        var behaviors = GetComponents<Behaviour>();
        foreach (var behavior in behaviors)
        {
            behavior.enabled = true;
        }
    }
}