#if UNITY_EDITOR

using GUtilsUnity.Features.ExtendableButtons.MonoBehaviours;
using UnityEditor;
using UnityEditor.UI;

namespace GUtilsUnity.Features.ExtendableButtons
{
    [CustomEditor(typeof(ExtendableButton))]
    public class ExtendableButtonEditor : ButtonEditor
    {
        SerializedProperty _extensionsProperty;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            _extensionsProperty = serializedObject.FindProperty("Extensions");

            serializedObject.Update();

            EditorGUILayout.PropertyField(_extensionsProperty);

            serializedObject.ApplyModifiedProperties();
        }
    }
}

#endif
