using UnityEditor;
using UnityEngine;

namespace Triplano
{
    [CustomEditor(typeof(ScriptableNumber))]
    public class ScriptableNumberEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            ScriptableNumber scriptableNumber = target as ScriptableNumber;

            EditorGUILayout.LabelField($"{nameof(scriptableNumber.SavedValue)} = {scriptableNumber.SavedValue}");

            EditorGUILayout.BeginVertical("box");
            
            EditorGUILayout.BeginHorizontal("box");
            if (GUILayout.Button("Add"))
                scriptableNumber.Add(1);
            if (GUILayout.Button("Remove"))
                scriptableNumber.Remove(1);
            EditorGUILayout.EndHorizontal();
            
            if (GUILayout.Button("Reset"))
                scriptableNumber.Reset();


            EditorGUILayout.EndVertical();
        }
    }
}
