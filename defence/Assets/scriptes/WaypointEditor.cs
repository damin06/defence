using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Waypoint))] //waypoint 대해서 custom
public class WaypointEditor : Editor
{
    Waypoint waypoint => target as Waypoint;

    private void OnSceneGUI()
    {
        EditorGUI.BeginChangeCheck();

        for(int i=0; i<waypoint.Points.Length; i++)
        {
            //create handle
            Vector3 currentWayPoint = waypoint.CurrentPosition + waypoint.Points[i];
            Vector3 newWaypointPoint = Handles.FreeMoveHandle(currentWayPoint, Quaternion.identity, 0.7f, new Vector3(x:0.3f, y:0.3f, z:0.3f), Handles.SphereHandleCap);

            //number ing handles
            GUIStyle numstyle = new GUIStyle();
            numstyle.fontSize = 16;
            numstyle.fontStyle = FontStyle.Bold;
            numstyle.normal.textColor = Color.magenta;
            Vector3 textAllignment = Vector3.down * 0.35f + Vector3.right * 0.35f;

            Handles.Label(waypoint.CurrentPosition + waypoint.Points[i] + textAllignment, text: $"{i + 1}", numstyle);

            EditorGUI.EndChangeCheck();

            if(EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(target, name: "Free Move Handle");
                waypoint.Points[i] = newWaypointPoint - waypoint.CurrentPosition;
            }
        }
    }
}
