using UnityEditor;
using UnityEngine;

namespace GUtilsUnity.Serialization.SerializableTypes
{
    [CustomPropertyDrawer(typeof(SerializableOptional<>))]
    public class SerializableOptionalPropertyDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var height = EditorGUIUtility.singleLineHeight;

            if (property.FindPropertyRelative("HasValue").boolValue)
            {
                height += EditorGUI.GetPropertyHeight(property.FindPropertyRelative("Value"));
            }

            return height;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var hasValueProperty = property.FindPropertyRelative("HasValue");
            var previousHasValue = hasValueProperty.boolValue;

            var toggleRect = position;
            toggleRect.height = EditorGUIUtility.singleLineHeight;
            var hasValue = EditorGUI.ToggleLeft(toggleRect, label, hasValueProperty.boolValue);
            if (previousHasValue != hasValue)
            {
                hasValueProperty.boolValue = hasValue;
            }

            if (!hasValue)
            {
                return;
            }

            position.y += EditorGUIUtility.singleLineHeight;
            position.height -= EditorGUIUtility.singleLineHeight;

            EditorGUI.PropertyField(position, property.FindPropertyRelative("Value"), GUIContent.none, true);
        }
    }
}
