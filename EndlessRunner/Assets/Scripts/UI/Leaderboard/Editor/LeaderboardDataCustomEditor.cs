using System;
using UnityEditor;
using UnityEngine;

namespace Triplano
{
    [CustomEditor(typeof(LeaderboardData))]
    public class LeaderboardDataCustomEditor : Editor
    {
        private int sampleScore = 10;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            LeaderboardData leaderboardData = target as LeaderboardData;

            for (int i = 0; i < leaderboardData.NumberOfRecords; i++)
            {
                CreateLabel($"Record[{i}] = {leaderboardData.GetRecordByIndex(i)}");
            }

            EditorGUILayout.BeginVertical("box");
            CreateLabel($"NumberOfRecords = {leaderboardData.NumberOfRecords}");
            CreateIntField("Sample Score", ref sampleScore);
            CreateButton("Add Score", () => leaderboardData.TryAddNewScore(sampleScore));
            CreateButton("Clear", () => leaderboardData.Clear());
            EditorGUILayout.EndVertical();
        }

        private void CreateLabel(string label)
        {
            EditorGUILayout.LabelField(label);
        }
        private void CreateIntField(string label, ref int intValue)
        {
            intValue = EditorGUILayout.IntField(label, intValue);
        }
        private void CreateButton(string label, Action action)
        {
            if(GUILayout.Button(label))
            {
                action?.Invoke();
            }
        }
    }
}
