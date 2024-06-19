using GUtilsUnity.PropertyDrawerLayout;
using GUtilsUnity.Extensions;
using UnityEditor;
using UnityEngine;

namespace GUtilsUnity.Attributes
{
    [CustomPropertyDrawer(typeof(NoFoldoutAttribute))]
    public sealed class NoFoldoutPropertyDrawer : PropertyDrawer
    {
        readonly PropertyDrawerLayoutHelper _layoutHelper = new();

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            NoFoldoutAttribute typeAttribute = (NoFoldoutAttribute)attribute;

            if (typeAttribute.DisplayName)
            {
                return EditorGUI.GetPropertyHeight(property, label, true);
            }

            return EditorGUI.GetPropertyHeight(property, label, true) - EditorGUIUtility.singleLineHeight;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (Event.current.type == EventType.Layout)
            {
                return;
            }

            NoFoldoutAttribute typeAttribute = (NoFoldoutAttribute)attribute;

            _layoutHelper.Init(position);

            if (!typeAttribute.DisplayName)
            {
                property.ForeachVisibleChildren(DrawChildPropertyField);
            }
            else
            {
                Rect labelRect = _layoutHelper.NextVerticalRect();

                EditorGUI.LabelField(labelRect, property.displayName);

                EditorGUI.indentLevel++;
                {
                    property.ForeachVisibleChildren(DrawChildPropertyField);
                }
                EditorGUI.indentLevel--;
            }
        }

        void DrawChildPropertyField(SerializedProperty childProperty)
        {
            EditorGUI.PropertyField(
                _layoutHelper.NextVerticalRect(childProperty),
                childProperty,
                includeChildren: true
            );
        }
    }
}
