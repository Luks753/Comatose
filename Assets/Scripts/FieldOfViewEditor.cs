using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor (typeof (EnemyFOV))]
public class FieldOfViewEditor : Editor {

    void OnSceneGUI() {
        EnemyFOV fow = (EnemyFOV)target;

        //Draws view reach
        Handles.color = Color.red;
        Handles.DrawWireArc(fow.transform.position, Vector3.forward, Vector3.up, 360, fow.viewRadius);

        //Draws cone of view
        Vector3 viewAngleA = fow.DirFromAngle(-fow.viewAngle / 2, false);
        Vector3 viewAngleB = fow.DirFromAngle(fow.viewAngle / 2, false);
        Handles.DrawLine(fow.transform.position, fow.transform.position + viewAngleA * fow.viewRadius);
        Handles.DrawLine(fow.transform.position, fow.transform.position + viewAngleB * fow.viewRadius);
    }
}