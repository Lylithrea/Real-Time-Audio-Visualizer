using UnityEditor;
using UnityEngine;
using System.Collections;




[CustomEditor(typeof(ES_CyclinderGenerator))]
public class E_CyclinderGenerator : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Generate"))
        {
            ES_CyclinderGenerator script = (ES_CyclinderGenerator)target;
            script.GenerateCyclinder();
        }
    }
}