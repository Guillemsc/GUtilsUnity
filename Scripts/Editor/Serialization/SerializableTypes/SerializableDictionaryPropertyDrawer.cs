using UnityEditor;
using UnityEngine;

namespace GUtilsUnity.Serialization.SerializableTypes
{
    /// <summary>
    /// Draws the dictionary and a warning-box if there are duplicate keys.
    /// </summary>
    [CustomPropertyDrawer(typeof(SerializableDictionary<,>))]
    public class SerializableDictionaryPropertyDrawer : PropertyDrawer
    {
        static readonly float _lineHeight = EditorGUIUtility.singleLineHeight;
        static readonly float _vertSpace = EditorGUIUtility.standardVerticalSpacing;
        const float _warningBoxHeight = 1.5f;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            // Draw list of key/value pairs.
            var list = property.FindPropertyRelative("_list");
            EditorGUI.PropertyField(position, list, label, true);

            // Draw key collision warning.
            var keyCollision = property.FindPropertyRelative("_keyCollision").boolValue;
            if (keyCollision)
            {
                position.y += EditorGUI.GetPropertyHeight(list, true);
                if (!list.isExpanded)
                {
                    position.y += _vertSpace;
                }

                position.height = _lineHeight * _warningBoxHeight;
                position = EditorGUI.IndentedRect(position);
                EditorGUI.HelpBox(position, "Duplicate keys will not be serialized.", MessageType.Warning);
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            // Height of KeyValue list.
            float height = 0f;
            var list = property.FindPropertyRelative("_list");
            height += EditorGUI.GetPropertyHeight(list, true);

            // Height of key collision warning.
            bool keyCollision = property.FindPropertyRelative("_keyCollision").boolValue;
            if (keyCollision)
            {
                height += _warningBoxHeight * _lineHeight;
                if (!list.isExpanded)
                {
                    height += _vertSpace;
                }
            }

            return height;
        }
    }
}
