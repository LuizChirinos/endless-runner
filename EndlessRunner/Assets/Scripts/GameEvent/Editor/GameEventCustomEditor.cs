using UnityEditor;
using UnityEngine;

namespace Triplano
{
    [CustomEditor(typeof(GameEvent))]
    public class GameEventCustomEditor : Editor
    {
        private bool debug;
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            GameEvent gameEvent = target as GameEvent;

            if (GUILayout.Button("Trigger Event"))
            {
                gameEvent.TriggerEvent();
            }

            debug = EditorGUILayout.Toggle(nameof(debug), debug);

            if (debug)
            {
                if (gameEvent.OnTriggeredEvent != null)
                {
                    foreach (System.Delegate item in gameEvent.OnTriggeredEvent.GetInvocationList())
                    {
                        EditorGUILayout.BeginVertical("box");
                        EditorGUILayout.LabelField($"Origin:{item.Target}");
                        EditorGUILayout.LabelField($"Type:{item.GetType().Name}");
                        EditorGUILayout.LabelField($"Method:{item.Method.Name}");
                        EditorGUILayout.EndVertical();
                    }
                }
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}
