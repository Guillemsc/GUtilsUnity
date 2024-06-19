using System;
using GUtilsUnity.Attributes;
using UnityEditor;
using UnityEngine;

namespace GUtilsUnity.Extensions
{
    [Experimental]
    public static class AddressablesEditorGUIExtensions
    {
        [Experimental]
        public static void AssetReferencePropertyField(
            Rect position,
            SerializedProperty property,
            Type genericArgument,
            GUIContent label
        )
        {
            UnityEngine.Object asset = property.GetAssetReferenceObject();

            label = EditorGUI.BeginProperty(position, label, property);
            int controlId = GUIUtility.GetControlID(696969, FocusType.Keyboard, position);
            position = EditorGUI.PrefixLabel(position, controlId, label);
            UnityEngine.Object newObject = EditorGUI.ObjectField(position, asset, genericArgument, false);
            EditorGUI.EndProperty();

            property.SetAssetReferenceObject(newObject);
        }
    }
}
