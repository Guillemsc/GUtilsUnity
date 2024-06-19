using System;
using GUtilsUnity.Extensions;
using UnityEditor;
using UnityEngine;

namespace GUtilsUnity.Serialization.SerializableTypes
{
    /// <summary>
    /// Property drawer for SerializableGuid
    ///
    /// Author: Searous
    /// </summary>
    [CustomPropertyDrawer(typeof(SerializableGuid))]
    public class SerializableGuidPropertyDrawer : PropertyDrawer
    {
        const float YSeparation = 20;

        float _buttonSize;
        float _lockUnlockButtonSize;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            // Start property draw
            EditorGUI.BeginProperty(position, label, property);

            // Get property
            SerializedProperty serializedGuid = property.FindPropertyRelative("_serializedGuid");
            SerializedProperty locked = property.FindPropertyRelative("_locked");

            // Draw label
            position = EditorGUI.PrefixLabel(
                new Rect(position.x, position.y + YSeparation / 2, position.width, position.height),
                GUIUtility.GetControlID(FocusType.Passive),
                label);

            _lockUnlockButtonSize = Math.Min(60, position.width);

            position.y -= YSeparation / 2; // Offsets position so we can draw the label for the field centered

            int buttonsCount = 0;

            if (!locked.boolValue)
            {
                buttonsCount = 2;
            }
            else
            {
                buttonsCount = 1;
            }

            // Update size of buttons to always fit perfeftly above the string representation field
            _buttonSize = position.width / buttonsCount;
            _buttonSize -= _lockUnlockButtonSize / buttonsCount;

            if (!locked.boolValue)
            {
                DrawNewButton(serializedGuid, position, 0);
                DrawCopyButton(serializedGuid, position, 1);
                DrawLockButton(locked, position, 2);
            }
            else
            {
                DrawCopyButton(serializedGuid, position, 0);
                DrawUnlockButton(locked, position, 1);
            }

            EditorGUI.BeginDisabledGroup(locked.boolValue);
            {
                // Draw fields - passs GUIContent.none to each so they are drawn without labels
                Rect pos = new Rect(position.xMin, position.yMin + YSeparation, position.width, YSeparation - 2);
                EditorGUI.PropertyField(pos, serializedGuid, GUIContent.none);
            }
            EditorGUI.EndDisabledGroup();

            // End property
            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            // Field height never changes, so ySep * 2 will always return the proper hight of the field
            return YSeparation * 2;
        }

        void DrawNewButton(SerializedProperty serializedGuid, Rect position, int buttonIndex)
        {
            if (GUI.Button(new Rect(position.xMin + _buttonSize * buttonIndex, position.yMin, _buttonSize, YSeparation - 2), "New"))
            {
                serializedGuid.RunForEachTargetObject(
                    x => x.stringValue = Guid.NewGuid().ToString()
                    );
            }
        }

        void DrawCopyButton(SerializedProperty serializedGuid, Rect position, int buttonIndex)
        {
            if (GUI.Button(new Rect(position.xMin + _buttonSize * buttonIndex, position.yMin, _buttonSize, YSeparation - 2), "Copy"))
            {
                EditorGUIUtility.systemCopyBuffer = serializedGuid.stringValue;
            }
        }

        void DrawLockButton(SerializedProperty locked, Rect position, int buttonIndex)
        {
            Color oldColor = GUI.backgroundColor;
            GUI.backgroundColor = Color.green;
            if (GUI.Button(new Rect(position.xMin + _buttonSize * buttonIndex, position.yMin, _lockUnlockButtonSize, YSeparation - 2), "Lock"))
            {
                locked.RunForEachTargetObject(x => x.boolValue = true);
            }
            GUI.backgroundColor = oldColor;
        }

        void DrawUnlockButton(SerializedProperty locked, Rect position, int buttonIndex)
        {
            Color oldColor = GUI.backgroundColor;
            GUI.backgroundColor = Color.red;
            if (GUI.Button(new Rect(position.xMin + _buttonSize * buttonIndex, position.yMin, _lockUnlockButtonSize, YSeparation - 2), "Unock"))
            {
                locked.RunForEachTargetObject(x => x.boolValue = false);
            }
            GUI.backgroundColor = oldColor;
        }
    }
}
