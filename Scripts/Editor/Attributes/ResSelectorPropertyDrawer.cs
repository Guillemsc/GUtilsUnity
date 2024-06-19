#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using GUtilsUnity.ResSelector;
using GUtilsUnity.Extensions;
using GUtilsUnity.Optionals;
using UnityEditor;
using UnityEngine;

namespace GUtilsUnity.Attributes
{
    [CustomPropertyDrawer(typeof(ResSelectorAttribute))]
    public sealed class ResSelectorPropertyDrawer : PropertyDrawer
    {
        static bool _isCacheInitialized;

        static readonly List<string> Names = new() { "Invalid" };
        static readonly List<string> Values = new() { "" };

        void InitializeCacheIfNecessary()
        {
            if (!_isCacheInitialized)
            {
                _isCacheInitialized = true;

                var type = Type.GetType("Res, Popcore.GameKit, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null");
                var fields = type != null
                    ? type
                        .GetFields(BindingFlags.Public | BindingFlags.Static)
                        .Where(fi => fi.IsLiteral && !fi.IsInitOnly && fi.FieldType == typeof(string))
                    : Enumerable.Empty<FieldInfo>();

                foreach (var field in fields)
                {
                    Names.Add(field.Name);
                    Values.Add((string)field.GetValue(null));
                }

            }
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            // Ensure the property is a string
            if (property.propertyType != SerializedPropertyType.String)
            {
                EditorGUI.PropertyField(position, property, label);
                return;
            }

            InitializeCacheIfNecessary();

            int index = Values.IndexOf(property.stringValue);

            // If value was not found select "Invalid"
            if (index == -1)
            {
                index = 0;
            }

            EditorGUI.BeginChangeCheck();

            index = EditorGUI.Popup(position, property.displayName, index, Names.ToArray());

            if (EditorGUI.EndChangeCheck())
            {
                property.stringValue = Values[index];
            }
        }
    }
}
#endif
