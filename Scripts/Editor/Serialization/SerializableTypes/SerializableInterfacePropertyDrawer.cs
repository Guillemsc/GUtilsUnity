using UnityEditor;
using UnityEngine;

namespace GUtilsUnity.Serialization.SerializableTypes
{
    [CustomPropertyDrawer(typeof(SerializableInterface<>))]
    public class SerializableInterfacePropertyDrawer : PropertyDrawer
    {
        float LineHeight => EditorGUIUtility.singleLineHeight;

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return LineHeight;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var serializedObjectProperty = property.FindPropertyRelative("_object");
            var type = fieldInfo.FieldType;
            var genericType = type.GetGenericArguments()[0];

            // TODO: Allow scene objects could be set to false in some cases, but unless we get problems not worth for now
            serializedObjectProperty.objectReferenceValue = EditorGUI.ObjectField(
                position,
                label,
                serializedObjectProperty.objectReferenceValue,
                genericType,
                true);
        }
    }
}
