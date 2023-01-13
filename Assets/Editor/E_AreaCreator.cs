using UnityEditor;
using UnityEngine;
using System.Collections;




[CustomEditor(typeof(ES_AreaGenerator))]
public class E_AreaGenerator : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Generate"))
        {
            ES_AreaGenerator script = (ES_AreaGenerator)target;
            script.GenerateArea();
        }
    }
}