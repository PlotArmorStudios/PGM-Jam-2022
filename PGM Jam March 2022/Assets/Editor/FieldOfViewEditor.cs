using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(FieldOfView))]
public class FieldOfViewEditor : Editor
{
  private void OnSceneGUI()
  {
    FieldOfView fov = (FieldOfView) target;
    Handles.color = Color.white;
    Handles.DrawWireArc(fov.transform.position, Vector3.up, Vector3.forward, 360, fov.Radius);

    Vector3 viewAngle1 = DirectionFromAngle(fov.transform.eulerAngles.y, -fov.Angle / 2);
    Vector3 viewAngle2 = DirectionFromAngle(fov.transform.eulerAngles.y, fov.Angle / 2);

    Handles.color = Color.yellow;
    Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngle1 * fov.Radius);
    Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngle2 * fov.Radius);

    if (fov.CanSeePlayer)
    {
      Handles.color = Color.green;
      Handles.DrawLine(fov.transform.position, fov.Player.transform.position);
    }
  }

  private Vector3 DirectionFromAngle(float eulerY, float angleInDegrees)
  {
    angleInDegrees += eulerY;

    return new Vector3((float) Math.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
  }
}
