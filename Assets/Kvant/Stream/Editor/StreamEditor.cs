//
// Custom editor class for Stream
//
using UnityEngine;
using UnityEditor;

namespace Kvant
{
    [CustomEditor(typeof(Stream)), CanEditMultipleObjects]
    public class StreamEditor : Editor
    {
        SerializedProperty _maxParticles;
        SerializedProperty _emitterPosition;
        SerializedProperty _debug;
        static GUIContent _textCenter = new GUIContent("Center");

        void OnEnable()
        {
            _maxParticles = serializedObject.FindProperty("_maxParticles");
            _emitterPosition = serializedObject.FindProperty("_emitterPosition");
            _debug = serializedObject.FindProperty("_debug");
        }

        public override void OnInspectorGUI()
        {
            var targetStream = target as Stream;

            serializedObject.Update();

            EditorGUI.BeginChangeCheck();

            EditorGUILayout.PropertyField(_maxParticles);
            if (!_maxParticles.hasMultipleDifferentValues)
            {
                EditorGUILayout.LabelField(" ", "Allocated: " + targetStream.maxParticles, EditorStyles.miniLabel);
                EditorGUILayout.LabelField(" ", targetStream.BufferWidth + "x" + targetStream.BufferHeight, EditorStyles.miniLabel);
            }
            if (EditorGUI.EndChangeCheck())
                targetStream.NotifyConfigChange();

            EditorGUILayout.LabelField("Emitter", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(_emitterPosition, _textCenter);

            EditorGUILayout.Space();

            EditorGUILayout.PropertyField(_debug);

            serializedObject.ApplyModifiedProperties();
        }
    }
}
