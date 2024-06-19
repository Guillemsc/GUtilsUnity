using System;
using GUtilsUnity.Attributes.FindFirstAsset;
using GUtilsUnity.Extensions;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace GUtilsUnity.Attributes
{
    [CustomPropertyDrawer(typeof(FindFirstAssetAttribute), true)]
    public class FindFirstAssetPropertyDrawer : PropertyDrawer
    {
        FindFirstAssetAttribute ActualAttribut => (FindFirstAssetAttribute)attribute;

        Action<Rect, SerializedProperty, GUIContent> _onGui;

        public override void OnGUI(
            Rect position,
            SerializedProperty property,
            GUIContent label
        )
        {
            if (_onGui == null)
            {
                _onGui = GetOnGui(property);
            }

            _onGui.Invoke(position, property, label);
        }

        Action<Rect, SerializedProperty, GUIContent> GetOnGui(SerializedProperty property)
        {
            if (property.propertyType != SerializedPropertyType.ObjectReference)
            {
                return OnGuiNotAnObjectReference;
            }

            return OnGuiComponent;
        }

        void OnGuiComponent(
            Rect rect,
            SerializedProperty property,
            GUIContent label
        )
        {
            if (property.objectReferenceValue == null)
            {
                property.objectReferenceValue = TryFindAsset();
            }

            EditorGUI.PropertyField(rect, property, label);
        }

        void OnGuiNotAnObjectReference(
            Rect arg1,
            SerializedProperty arg2,
            GUIContent arg3
        )
        {
            EditorGUI.LabelField(arg1, "Can only be used on Object references");
        }

        Object TryFindAsset()
        {
            bool isPrefab = IsPrefab();

            if (isPrefab)
            {
                AssetDatabaseExtensions.TryFindFirstPrefabWithComponentType(
                    fieldInfo.FieldType,
                    out Object prefabAsset
                );
                return prefabAsset;
            }

            AssetDatabaseExtensions.TryFindFirstAssetByTypeAndNameOrFirstByType(
                fieldInfo.FieldType,
                ActualAttribut.SearchString,
                out Object asset
                );
            return asset;
        }

        bool IsPrefab()
        {
            return typeof(Component).IsAssignableFrom(fieldInfo.FieldType);
        }
    }
}
