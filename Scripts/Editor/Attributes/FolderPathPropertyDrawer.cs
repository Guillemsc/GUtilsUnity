using GUtilsUnity.Extensions;
using UnityEditor;
using UnityEngine;

namespace GUtilsUnity.Attributes
{
    [CustomPropertyDrawer(typeof(FolderPathAttribute))]
    public sealed class FolderPathPropertyDrawer : PropertyDrawer
    {
        const float ButtonWidth = 60.0f;
        const float ButtonPadding = 2.5f;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            Rect textFieldPosition = position;
            textFieldPosition.width -= ButtonWidth + ButtonPadding * 2;

            if (property.propertyType != SerializedPropertyType.String)
            {
                EditorGUI.LabelField(textFieldPosition, $"{nameof(FolderPathAttribute)} can only be used with string types, at {property.name}");
                return;
            }

            Rect buttonPosition = position;
            buttonPosition.width = ButtonWidth;
            buttonPosition.x = textFieldPosition.x + textFieldPosition.width + ButtonPadding;

            property.stringValue = EditorGUI.TextField(textFieldPosition, property.stringValue);

            bool openFolderPanel = GUI.Button(buttonPosition, "Select...");

            if (openFolderPanel)
            {
                string directory = EditorUtility.OpenFolderPanel("Select Folder", property.stringValue, "");

                if (!string.IsNullOrEmpty(directory))
                {
                    property.stringValue = DirectoryExtensions.AbsolutePathToAssetsRelativePath(directory);
                    property.serializedObject.ApplyModifiedProperties();
                }

                GUIUtility.ExitGUI();
            }
        }
    }
}
