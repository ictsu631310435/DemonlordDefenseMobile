using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MaterialFlasher))]
public class MaterialFlasherEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        MaterialFlasher materialFlasher = (MaterialFlasher)target;

        GUILayout.BeginHorizontal();

        if (GUILayout.Button("Find Mesh Renderers"))
        {
            materialFlasher.FindMeshRenderers();
        }

        if (GUILayout.Button("Reset"))
        {
            materialFlasher.ResetMeshRenderers();
        }

        GUILayout.EndHorizontal();
    }
}
