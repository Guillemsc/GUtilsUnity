using System.Collections.Generic;
using GUtilsUnity.Extensions;
using GUtilsUnity.Pickers;
using UnityEditor;
using UnityEngine;

namespace GUtilsUnity.Attributes
{
    [Experimental]
    [CustomPropertyDrawer(typeof(PrefabSelectorAttribute))]
    public sealed class PrefabSelectorPropertyDrawer : PropertyDrawer
    {
        const int Margin = 2;
        const float ButtonSize = 80;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            bool isValidField = IsValidField();

            if (!isValidField)
            {
                EditorGUI.LabelField(position, "Field needs to be a Component");
                return;
            }

            float objectFieldSize = position.width - ButtonSize - Margin;

            position.width = objectFieldSize;
            EditorGUI.PropertyField(position, property, new GUIContent(property.name));

            position.x += position.width + Margin;
            position.width = ButtonSize;

            if (GUI.Button(position, "Find Prefab"))
            {
                OpenObjectPicker(property);
            }
        }

        void OpenObjectPicker(SerializedProperty property)
        {
            List<Object> options = AssetDatabaseExtensions.FindAllPrefabsWithComponentType(fieldInfo.FieldType);

            void SelectOption(Object option)
            {
                property.objectReferenceValue = option;
                property.serializedObject.ApplyModifiedProperties();
            }

            bool IsSelectedObject(Object checkingObject)
            {
                if (property.objectReferenceValue is not Component component)
                {
                    return false;
                }

                bool isSame = checkingObject == component.gameObject;
                return isSame;
            }

            ObjectPickerWindow.Open("Prefab selector", options, SelectOption, IsSelectedObject);
        }

        public bool IsValidField()
        {
            return typeof(Component).IsAssignableFrom(fieldInfo.FieldType);
        }
    }
}
