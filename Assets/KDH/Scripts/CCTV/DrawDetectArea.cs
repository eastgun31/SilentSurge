using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Sight))]
public class DrawDetectArea : Editor
{
    void OnSceneGUI()
    {
        Sight fov = (Sight)target;
        Handles.color = Color.red;
        Handles.DrawWireArc(fov.transform.position, Vector3.up, Vector3.forward, 360, fov.radius);
        Vector3 dAngle_A = fov.DirFromAngle(-fov.angle / 2, false);
        Vector3 dAngle_B = fov.DirFromAngle(fov.angle / 2, false);

        Handles.DrawLine(fov.transform.position, fov.transform.position + dAngle_A * fov.radius);
        Handles.DrawLine(fov.transform.position, fov.transform.position + dAngle_B * fov.radius);
    }
}