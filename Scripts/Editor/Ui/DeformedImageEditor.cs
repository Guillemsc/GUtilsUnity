using System;
using UnityEditor;
using UnityEditor.UI;

namespace GUtilsUnity.Ui
{
    [CustomEditor(typeof(DeformedImage)), CanEditMultipleObjects]
    [Obsolete]
    public sealed class DeformedImageEditor : ImageEditor
    {
        SerializedProperty _offsets;

        protected override void OnEnable()
        {
            base.OnEnable();

            _offsets = serializedObject.FindProperty(nameof(DeformedImage.Offsets));
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.PropertyField(_offsets, includeChildren: true);
            serializedObject.ApplyModifiedProperties();

            base.OnInspectorGUI();
        }
    }
}
