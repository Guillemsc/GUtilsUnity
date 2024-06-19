using GUtilsUnity.Attributes.SelectImplementation;
using GUtilsUnity.Attributes.SelectImplementation.Data;
using GUtilsUnity.Attributes.SelectImplementation.Logic;
using GUtilsUnity.ImplementationSelector.Logic;
using GUtilsUnity.PropertyDrawerLayout;
using GUtilsUnity.Extensions;
using UnityEditor;
using UnityEngine;

namespace GUtilsUnity.Attributes.ImplementationSelector.PropertyDrawers
{
    [CustomPropertyDrawer(typeof(SelectImplementationAttribute))]
    public class SelectImplementationPropertyDrawer : PropertyDrawer
    {
        readonly PropertyDrawerLayoutHelper _layoutHelper = new();
        readonly EditorData _editorData = new();

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            SelectImplementationAttribute typeAttribute = (SelectImplementationAttribute)attribute;

            float height = _layoutHelper.GetElementsHeight(1);

            bool isCollapsed = !property.isExpanded && !typeAttribute.ForceExpanded;

            if (isCollapsed)
            {
                return height;
            }

            return height + property.GetVisibleChildrenHeight();
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            SelectImplementationAttribute typeAttribute = (SelectImplementationAttribute)attribute;

            TryCacheTypesLogic.Execute(_editorData, typeAttribute, fieldInfo);
            TryCacheNamesGuiContentLogic.Execute(_editorData, fieldInfo);

            bool typeIndexFound = TryGetTypeIndexLogic.Execute(
                _editorData,
                property,
                out int typeIndex
            );

            bool isUninitalized = !typeIndexFound && _editorData.Types.Length > 0;

            if (isUninitalized)
            {
                typeIndex = GetDefaultTypeIndexLogic.Execute(_editorData);

                InitializePropertyAtIndexLogic.Execute(
                    _editorData,
                    property,
                    typeIndex
                );
            }

            if (Event.current.type == EventType.Layout)
            {
                return;
            }

            FixSerializeReferenceLogic.Execute(property, _editorData, typeIndex);

            _layoutHelper.Init(position);

            bool shouldDrawChildren = (property.hasVisibleChildren && property.isExpanded) || typeAttribute.ForceExpanded;

            Rect popupRect = _layoutHelper.NextVerticalRect();

            GUIContent finalLabel = GUIContent.none;

            if (typeAttribute.DisplayLabel)
            {
                finalLabel = label;
            }

            if (typeAttribute.ForceExpanded || !property.hasVisibleChildren)
            {
                property.isExpanded = true;
            }
            else
            {
                property.isExpanded = EditorGUI.Foldout(popupRect, property.isExpanded, GUIContent.none);
            }

            int newTypeIndex = EditorGUI.Popup(
                popupRect,
                finalLabel,
                typeIndex,
                _editorData.NamesGuiContent
            );

            if (newTypeIndex != typeIndex)
            {
                InitializePropertyAtIndexLogic.Execute(
                    _editorData,
                    property,
                    newTypeIndex
                );
            }

            if (!shouldDrawChildren && !property.isExpanded)
            {
                return;
            }

            EditorGUI.indentLevel++;
            {
                property.ForeachVisibleChildren(DrawChildPropertyField);
            }
            EditorGUI.indentLevel--;
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

