using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetAsInactiveAfterDone : MonoBehaviour
{
    private void SetInactive()
    {
        gameObject.SetActive(false);
    }
}
