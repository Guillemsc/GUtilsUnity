using System;
using GUtilsUnity.Extensions;
using UnityEditor;
using UnityEngine;

namespace GUtilsUnity.Serialization.SerializableTypes
{
    [CustomPropertyDrawer(typeof(SerializableDateTime))]
    public class SerializableDateTimeDrawer : PropertyDrawer
    {
        float LineHeight => EditorGUIUtility.singleLineHeight;
        float Spacing => EditorGUIUtility.standardVerticalSpacing;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            float lineHeight = EditorGUIUtility.singleLineHeight;
            float spacing = EditorGUIUtility.standardVerticalSpacing;

            EditorGUI.indentLevel++;

            SerializedProperty yearProp = property.FindPropertyRelative(nameof(SerializableDateTime.Year));
            SerializedProperty monthProp = property.FindPropertyRelative(nameof(SerializableDateTime.Month));
            SerializedProperty dayProp = property.FindPropertyRelative(nameof(SerializableDateTime.Day));
            SerializedProperty hourProp = property.FindPropertyRelative(nameof(SerializableDateTime.Hour));
            SerializedProperty minuteProp = property.FindPropertyRelative(nameof(SerializableDateTime.Minute));
            SerializedProperty secondProp = property.FindPropertyRelative(nameof(SerializableDateTime.Second));
            SerializedProperty kindProp = property.FindPropertyRelative(nameof(SerializableDateTime.DateTimeKind));

            if (yearProp.intValue == 0)
            {
                yearProp.intValue = 1;
                monthProp.intValue = 1;
                dayProp.intValue = 1;
                hourProp.intValue = 0;
                minuteProp.intValue = 0;
                secondProp.intValue = 0;
                kindProp.enumValueIndex = (int)DateTimeKind.Utc;
            }

            Rect labelRect = new Rect(position.x, position.y, position.width, lineHeight);
            Rect yearRect = new Rect(position.x, position.y + lineHeight + spacing, position.width, lineHeight);
            Rect monthRect = new Rect(position.x, yearRect.y + lineHeight + spacing, position.width, lineHeight);
            Rect dayRect = new Rect(position.x, monthRect.y + lineHeight + spacing, position.width, lineHeight);
            Rect hourRect = new Rect(position.x, dayRect.y + lineHeight + spacing, position.width, lineHeight);
            Rect minuteRect = new Rect(position.x, hourRect.y + lineHeight + spacing, position.width, lineHeight);
            Rect secondRect = new Rect(position.x, minuteRect.y + lineHeight + spacing, position.width, lineHeight);
            Rect kindRect = new Rect(position.x, secondRect.y + lineHeight + spacing, position.width, lineHeight);

            EditorGUI.LabelField(labelRect, label);
            EditorGUI.indentLevel++;

            EditorGUIExtensions.IntRangePropertyField(yearRect, yearProp, 1, 9999);
            EditorGUIExtensions.IntRangePropertyField(monthRect, monthProp, 1, 12);
            int maxDays = DateTime.DaysInMonth(yearProp.intValue, monthProp.intValue);
            EditorGUIExtensions.IntRangePropertyField(dayRect, dayProp, 1, maxDays);
            EditorGUIExtensions.IntRangePropertyField(hourRect, hourProp, 0, 23);
            EditorGUIExtensions.IntRangePropertyField(minuteRect, minuteProp, 0, 59);
            EditorGUIExtensions.IntRangePropertyField(secondRect, secondProp, 0, 59);
            EditorGUI.PropertyField(kindRect, kindProp);

            EditorGUI.indentLevel--;

            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            const int fieldCount = 7; // Number of fields in the SerializableDateTime class

            return (LineHeight + Spacing) * fieldCount;
        }
    }
}
