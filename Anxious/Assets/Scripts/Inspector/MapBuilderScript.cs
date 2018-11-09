using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(MapBuildScript))]
public class MapBuilderEditor : Editor {
    public override void OnInspectorGUI() {
        DrawDefaultInspector();

        MapBuildScript myScript = (MapBuildScript)target;
        if (GUILayout.Button("Build Map")) {
            myScript.BuildMap();
        }
    }
}
