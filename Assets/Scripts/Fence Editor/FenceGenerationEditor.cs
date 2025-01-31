﻿#if UNITY_EDITOR
using System.Collections;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(FenceGeneration)), CanEditMultipleObjects]
public class FenceGenerationEditor : Editor {

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        FenceGeneration myFence = (FenceGeneration)target;
        if (GUILayout.Button("AddPost")) {
            myFence.AddPost();
        }
        if (GUILayout.Button("Remove Self")) {
            myFence.Remove();
        }
    }
}
#endif