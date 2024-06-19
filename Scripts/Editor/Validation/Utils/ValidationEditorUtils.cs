using System.Linq;
using GUtilsUnity.Validation.Data;
using GUtilsUnity.Validation.Enums;
using GUtilsUnity.Validation.Results;
using UnityEditor;
using UnityEngine;

namespace GUtilsUnity.Validation.Utils
{
    public static class ValidationEditorUtils
    {
        public static void DrawValidationResult(IValidationResult validationResult)
        {
            if (validationResult == null)
            {
                EditorGUILayout.LabelField("Validation result is null", EditorStyles.boldLabel);
                return;
            }

            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                EditorGUILayout.LabelField("Validation", EditorStyles.boldLabel);
                EditorGUILayout.LabelField($"Result: {validationResult.ValidationResultType}");

                IOrderedEnumerable<IValidationLog> validationLogs = validationResult.ValidationLogs.OrderBy(i => i.ValidationLogType);

                foreach (IValidationLog validationLog in validationLogs)
                {
                    switch (validationLog.ValidationLogType)
                    {
                        case ValidationLogType.Info:
                            {
                                EditorGUILayout.HelpBox(validationLog.LogMessage, MessageType.Info);
                            }
                            break;

                        case ValidationLogType.Warning:
                            {
                                EditorGUILayout.HelpBox(validationLog.LogMessage, MessageType.Warning);
                            }
                            break;

                        case ValidationLogType.Error:
                            {
                                EditorGUILayout.HelpBox(validationLog.LogMessage, MessageType.Error);
                            }
                            break;
                    }
                }
            }
        }

        public static void DrawSimplifiedValidationResult(IValidationResult validationResult)
        {
            if (validationResult == null)
            {
                EditorGUILayout.LabelField("Validation result is null", EditorStyles.boldLabel);
                return;
            }

            if (validationResult.ValidationResultType == ValidationResultType.Success)
            {
                using (new EditorGUILayout.HorizontalScope(EditorStyles.helpBox))
                {
                    GUILayout.Label($"Validation Result: {validationResult.ValidationResultType}");
                }
            }
            else
            {
                Color oldColor = GUI.backgroundColor;
                GUI.backgroundColor = Color.red;
                using (new EditorGUILayout.HorizontalScope(EditorStyles.helpBox))
                {
                    int errorsCount = validationResult.ValidationLogs.Select(i => i.ValidationLogType == ValidationLogType.Error).Count();

                    GUILayout.Label($"Validation Result: {validationResult.ValidationResultType} ({errorsCount})");
                }
                GUI.backgroundColor = oldColor;
            }
        }
    }
}
