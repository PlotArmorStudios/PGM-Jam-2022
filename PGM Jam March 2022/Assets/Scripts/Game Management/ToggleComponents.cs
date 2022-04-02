using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleComponents : MonoBehaviour
{
    public virtual void ToggleOffComponents()
    {
        var behaviors = GetComponents<Behaviour>();
        foreach (var behavior in behaviors)
        {
            if (behavior is Player) behavior.GetComponent<Player>().Animator.SetBool("Walking", false);
            if (behavior != this) behavior.enabled = false;
        }
    }

    public virtual void ToggleOnComponents()
    {
        var behaviors = GetComponents<Behaviour>();
        foreach (var behavior in behaviors) behavior.enabled = true;
    }
}