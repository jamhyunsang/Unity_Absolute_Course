﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Assertions.Must;

[CustomEditor(typeof(EnemyFOV))]
public class FOVEditor : Editor
{
    private void OnSceneGUI()
    {
        EnemyFOV fov = (EnemyFOV)target;

        Vector3 formAnglePos = fov.CirclePoint(-fov.viewAngle * 0.5f);
        Handles.color = Color.white;
        Handles.DrawWireDisc(fov.transform.position, Vector3.up, fov.viewRange);
        Handles.color = new Color(1, 1, 1, 0.2f);
        Handles.Label(fov.transform.position + (fov.transform.forward * 2.0f),fov.viewAngle.ToString());

    }
}