using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Triplano
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(RaycastCollisionDetection))]
    public class RaycastCollisionDetectionCustomEditor : Editor
    {
        private SerializedProperty isCollidingProperty;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            RaycastCollisionDetection raycastCollision = target as RaycastCollisionDetection;
            EditorGUILayout.Toggle(nameof(raycastCollision.IsColliding), raycastCollision.IsColliding);
            GUI.enabled = false;
            EditorGUILayout.FloatField(nameof(raycastCollision.DistanceToCollider), raycastCollision.DistanceToCollider);
            GUI.enabled = true;

            serializedObject.ApplyModifiedProperties();
        }
    }
}
