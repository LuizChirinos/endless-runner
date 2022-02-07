using System;
using UnityEditor;
using UnityEngine;

namespace Triplano
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(LaneObjectPlacement))]
    public class LaneObjectPlacementCustomEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            LaneObjectPlacement[] laneObjects = new LaneObjectPlacement[serializedObject.targetObjects.Length];
            for (int i = 0; i < serializedObject.targetObjects.Length; i++)
            {
                laneObjects[i] = serializedObject.targetObjects[i] as LaneObjectPlacement;
            }
            CreateLabel("Z Axis");
            EditorGUILayout.BeginHorizontal("box");
            CreateButton("Increment Z", () =>
            {
                foreach (LaneObjectPlacement laneObject in laneObjects)
                {
                    laneObject.IncrementOnZ();
                }
            });
            CreateButton("Decrement Z", () =>
            {
                foreach (LaneObjectPlacement laneObject in laneObjects)
                {
                    laneObject.DecrementOnZ();
                }
            });
            EditorGUILayout.EndHorizontal();

            CreateLabel("Y Axis");
            EditorGUILayout.BeginHorizontal("box");
            CreateButton("Increment Y", () =>
            {
                foreach (LaneObjectPlacement laneObject in laneObjects)
                {
                    laneObject.IncrementOnY();
                }
            });
            CreateButton("Decrement Y", () =>
            {
                foreach (LaneObjectPlacement laneObject in laneObjects)
                {
                    laneObject.DecrementOnY();
                }
            });
            EditorGUILayout.EndHorizontal();
        }

        private void CreateButton(string buttonLabel, Action action)
        {
            if (GUILayout.Button(buttonLabel))
                action?.Invoke();
        }
        private void CreateLabel(string label)
        {
            EditorGUILayout.LabelField(label);
        }
    }

}
