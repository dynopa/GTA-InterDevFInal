using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(QuadraticBezier))]
public class BezierScriptEditor : Editor
{
    public override void OnInspectorGUI()
    {
        
        QuadraticBezier thisBezier = (QuadraticBezier)target;
        base.OnInspectorGUI();
        EditorGUILayout.LabelField("Bezier Length", thisBezier.bezierLength.ToString());


        EditorGUILayout.Space();


        if (GUILayout.Button("Center Bezier Between End Points"))
        {
            thisBezier.CenterBezierPoint();
        }

        EditorGUILayout.Space();


        if (GUILayout.Button("Create Right Angle Bezier[1]"))
        {
            thisBezier.RightAngleBezierPoint1();
        }

        if (GUILayout.Button("Create Right Angle Bezier[2]"))
        {
            thisBezier.RightAngleBezierPoint2();
        }


    }

}
