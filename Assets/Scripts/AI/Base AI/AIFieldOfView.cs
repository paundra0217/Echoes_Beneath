using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

[CustomEditor(typeof(AIFieldOfView))]
public class AIFieldOfViewEditor : Editor
{
    void OnSceneGUI()
    {
        AIFieldOfView fow = (AIFieldOfView)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(fow.transform.position, Vector3.up, Vector3.forward, 360, fow.viewRadius);
        Vector3 viewAngA = fow.DirFromAngle(-fow.viewAngle / 2, false);
        Vector3 viewAngB = fow.DirFromAngle(fow.viewAngle / 2, false);

        Handles.DrawLine(fow.transform.position, fow.transform.position + viewAngA * fow.viewAngle);
        Handles.DrawLine(fow.transform.position, fow.transform.position + viewAngB * fow.viewAngle);
    }
}
