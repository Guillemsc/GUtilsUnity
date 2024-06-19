using System;
using UnityEditor;
using UnityEngine;

namespace GUtilsUnity.Attributes
{
    [CustomPropertyDrawer(typeof(ChildComponentOfAttribute))]
    public class ChildComponentOfPropertyDrawer : PropertyDrawer
    {
        Action<Rect, SerializedProperty, GUIContent> _onGui;

        public override void OnGUI(
            Rect position,
            SerializedProperty property,
            GUIContent label)
        {
            if (_onGui == null)
            {
                _onGui = GetOnGui(property);
            }

            _onGui.Invoke(position, property, label);
        }

        Action<Rect, SerializedProperty, GUIContent> GetOnGui(
            SerializedProperty property)
        {
            var componentOfFieldType = fieldInfo.FieldType;
            if (!typeof(Component).IsAssignableFrom(componentOfFieldType))
            {
                return OnGuiNotComponentField;
            }

            var componentOfAttribute = (ChildComponentOfAttribute)attribute;
            var objectType = property.serializedObject.targetObject.GetType();
            var sourceFieldType = objectType.GetField(componentOfAttribute.FieldName);

            if (sourceFieldType == null)
            {
                return OnGuiNoTargetField;
            }

            UnityEngine.Object GetObject()
            {
                var sourceObject = sourceFieldType.GetValue(property.serializedObject.targetObject);

                if (sourceObject == null || !(UnityEngine.Object)sourceObject)
                {
                    return null;
                }

                return sourceObject switch
                {
                    Component component => component.GetComponentInChildren(fieldInfo.FieldType),
                    GameObject gameObject => gameObject.GetComponentInChildren(fieldInfo.FieldType),
                    _ => throw new ArgumentOutOfRangeException(nameof(sourceObject))
                };
            }

            return (rect, serializedProperty, label) => OnGuiComponentOf(rect, serializedProperty, label, GetObject);
        }

        void OnGuiNoTargetField(
            Rect arg1,
            SerializedProperty arg2,
            GUIContent arg3)
        {
            var componentOfAttribute = (ComponentOfAttribute)attribute;
            EditorGUI.LabelField(arg1, $"ComponentOf can't find any field with name {componentOfAttribute.FieldName}");
        }

        void OnGuiComponentOf(
            Rect rect,
            SerializedProperty serializedProperty,
            GUIContent label,
            Func<UnityEngine.Object> getObject
        )
        {
            if (serializedProperty.objectReferenceValue == null)
            {
                serializedProperty.objectReferenceValue = getObject();
            }

            EditorGUI.PropertyField(rect, serializedProperty, label);
        }

        void OnGuiNotComponentField(
            Rect arg1,
            SerializedProperty arg2,
            GUIContent arg3
        )
        {
            EditorGUI.LabelField(arg1, "ComponentOf can only be used on Component fields");
        }
    }
}
