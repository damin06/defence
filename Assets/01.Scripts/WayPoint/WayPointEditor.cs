using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(WayPoint))]  // waypoint ´ëÇØ¼­ custom
public class WayPointEditor : Editor
{
    private WayPoint _wayPoints => target as WayPoint;

    private void OnSceneGUI()
    {
        for (int i = 0; i < _wayPoints.points.Length; i++)
        {
            EditorGUI.BeginChangeCheck();

            //create handle
            Vector3 currentWatpointPoint = _wayPoints.currentPosition + _wayPoints.points[i];
            Vector3 newWaypointPoint = Handles.FreeMoveHandle(currentWatpointPoint, Quaternion.identity, 0.7f, new Vector3(x:0.3f, y:0.3f, z:0.3f), Handles.SphereHandleCap);

            //numbering handles
            GUIStyle numStyle = new GUIStyle();
            numStyle.fontSize = 16;
            numStyle.fontStyle = FontStyle.Bold;
            numStyle.normal.textColor = Color.magenta;
            Vector3 textAllingnment = Vector3.down * 0.35f + Vector3.right * 0.35f;

            Handles.Label(_wayPoints.currentPosition + _wayPoints.points[i] + textAllingnment, text: $"{i + 1}", numStyle);

            EditorGUI.EndChangeCheck();

            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(target, name: "Free Move Handle");
                _wayPoints.points[i] = newWaypointPoint - _wayPoints.currentPosition;
            }
        }
    }
}
