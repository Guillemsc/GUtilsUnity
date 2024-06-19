using GUtilsUnity.Json.ScriptableObjects.Editors;
using UnityEditor;
using UnityEngine;

namespace GUtilsUnity.Json.ScriptableObjects.Drawers
{
    public static class GenerateJsonAndGeneratedJsonDrawer
    {
        public static void Draw(JsonGeneratorScriptableObjectEditor editor)
        {
            if (Event.current.type == EventType.Repaint)
            {
                editor.ActualTarget.GenerateJson();
            }

            string jsonString = editor.SerializedPropertiesData.GeneratedJsonProperty.stringValue;

            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                EditorGUILayout.LabelField("Json:", EditorStyles.boldLabel);

                if (GUILayout.Button("Copy"))
                {
                    GUIUtility.systemCopyBuffer = editor.ActualTarget.GenerateCopyJson(false);
                }

                if (GUILayout.Button("Copy Indented"))
                {
                    GUIUtility.systemCopyBuffer = editor.ActualTarget.GenerateCopyJson(true);
                }

                EditorGUILayout.TextArea(jsonString);
            }
        }
    }
}
