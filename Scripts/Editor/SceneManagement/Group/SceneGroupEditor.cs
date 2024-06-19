using GUtilsUnity.SceneManagement.Group.Data;
using GUtilsUnity.SceneManagement.Group.Drawers;
using GUtilsUnity.SceneManagement.Group.Helpers;
using GUtilsUnity.SceneManagement.Group.Logic;
using UnityEditor;
using UnityEngine;

namespace GUtilsUnity.SceneManagement.Group
{
    [CustomEditor(typeof(SceneGroup))]
    public sealed class SceneGroupEditor : Editor
    {
        readonly ToolData _toolData = new ToolData();

        readonly ReorderableHelper _reorderableHelper = new ReorderableHelper();

        SceneGroup ActualTarget { get; set; }

        private void OnEnable()
        {
            ActualTarget = (SceneGroup)target;

            GatherSceneGroupCustomDrawersLogic.Execute(
                _toolData
                );

            GatherSceneEntryCustomDrawersLogic.Execute(
                _toolData
                );
        }

        public override void OnInspectorGUI()
        {
            LoadAsActiveChangeCheckLogic.Execute(_toolData, ActualTarget);

            EditorGUI.BeginChangeCheck();

            serializedObject.Update();

            HeaderDrawer.Draw(ActualTarget);

            EditorGUILayout.Space(4);

            SceneEntriesDrawer.Draw(
                this,
                ActualTarget,
                _toolData,
                _reorderableHelper
                );

            EditorGUILayout.Space(2);

            AddEntriesDrawer.Draw(ActualTarget);

            EditorGUILayout.Space(2);

            OpenCloseDrawer.Draw(ActualTarget);

            SceneGroupCustomDrawersDrawer.Draw(_toolData, ActualTarget);

            if (Event.current.type != EventType.Layout)
            {
                ActuallyRemoveSceneEntriesLogic.Execute(_toolData, ActualTarget);
            }

            if (EditorGUI.EndChangeCheck())
            {
                EditorUtility.SetDirty(ActualTarget);
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}
