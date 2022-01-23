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

        DrawLevelStateSection(levelFlow);

        DrawChangeLevelSection(levelFlow);
    }

    private void DrawLevelStateSection(LevelFlow levelFlow)
    {
        GUILayout.BeginVertical("box");

        EditorGUILayout.LabelField($"LEVEL STATE SECTION");
        EditorGUILayout.LabelField($"Current Level = {SceneManager.GetActiveScene().name}");
        EditorGUILayout.LabelField($"Build Index = {SceneManager.GetActiveScene().buildIndex}");
        EditorGUILayout.LabelField($"Level State = {levelFlow.LevelStatus}");

        GUILayout.BeginHorizontal("box");
        if (GUILayout.Button("Win Level"))
        {
            levelFlow.WinLevel();
        }
        if (GUILayout.Button("Lose Level"))
        {
            levelFlow.LoseLevel();
        }
        GUILayout.EndHorizontal();
        if (GUILayout.Button("StartLevel"))
        {
            levelFlow.StartLevel();
        }

        GUILayout.EndVertical();
    }

    private void DrawChangeLevelSection(LevelFlow levelFlow)
    {
        GUILayout.BeginVertical("box");

        EditorGUILayout.LabelField($"CHANGE LEVEL SECTION");
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
        GUILayout.EndVertical();
    }


}
