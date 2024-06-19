using GUtilsUnity.Json.ScriptableObjects.Data;
using GUtilsUnity.Json.ScriptableObjects.Drawers;
using UnityEditor;

namespace GUtilsUnity.Json.ScriptableObjects.Editors
{
    [CustomEditor(typeof(JsonGeneratorScriptableObject<>), true)]
    public sealed class JsonGeneratorScriptableObjectEditor : Editor
    {
        public BaseJsonGeneratorScriptableObject ActualTarget { get; set; }

        public SerializedPropertiesData SerializedPropertiesData { get; } = new();

        void OnEnable()
        {
            ActualTarget = (BaseJsonGeneratorScriptableObject)target;
            SerializedPropertiesData.GeneratedJsonProperty = serializedObject.FindProperty("_generatedJson");
        }

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            EditorGUILayout.Space();

            GenerateJsonAndGeneratedJsonDrawer.Draw(this);
        }
    }
}
