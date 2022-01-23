using UnityEngine;
using UnityEditor;
using Triplano.Levels;
using UnityEngine.SceneManagement;

[CustomEditor(typeof(LevelFlow))]
public class LevelFlowEditor : Editor
{
    public override void OnInspectorGUI()
    {
        LevelFlow levelFlow = target as LevelFlow;

        GUILayout.BeginVertical("box");
        EditorGUILayout.LabelField($"Current Level = {SceneManager.GetActiveScene().name}");
        EditorGUILayout.LabelField($"Build Index = {SceneManager.GetActiveScene().buildIndex}");
        EditorGUILayout.LabelField($"Level State = {levelFlow.LevelStatus}");
        GUILayout.EndVertical();

        GUILayout.BeginHorizontal("box");
        if (GUILayout.Button("Previous Level"))
        {
            levelFlow.PreviousLevel();
        }
        if (GUILayout.Button("Next Level"))
        {
            levelFlow.NextLevel();
        }
        GUILayout.EndHorizontal();

        if (GUILayout.Button("Reset Level"))
        {
            levelFlow.ResetLevel();
        }
    }
}
