using System.IO;
using GUtilsUnity.SceneManagement.Group.Data;
using GUtilsUnity.SceneManagement.Group.Helpers;
using GUtilsUnity.SceneManagement.Group.Logic;
using GUtilsUnity.SceneManagement.Loader;
using GUtilsUnity.Extensions;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GUtilsUnity.SceneManagement.Group.Drawers
{
    public static class SceneEntriesDrawer
    {
        public static void Draw(
            SceneGroupEditor sceneGroupEditor,
            SceneGroup sceneGroup,
            ToolData toolData,
            ReorderableHelper reorderableHelper
            )
        {
            for (int i = 0; i < sceneGroup.Entries.Count; ++i)
            {
                SceneGroupEntry entry = sceneGroup.Entries[i];

                string sceneName = Path.GetFileNameWithoutExtension(entry.SceneReference);

                bool isValidScene = !string.IsNullOrEmpty(sceneName);

                if (!isValidScene)
                {
                    sceneName = "Missing Scene!";
                }

                if(i != 0)
                {
                    EditorGUILayout.Space(4);
                }

                using (new GUILayout.VerticalScope(EditorStyles.helpBox))
                {
                    ComponentHeaderDrawer.Draw(
                        sceneName,
                        string.Empty,
                        () => EntryContextMenuDrawer.Draw(toolData, entry),
                        out Rect reorderInteractionRect,
                        out Rect secondaryInteractionRect
                        );

                    reorderableHelper.CheckDraggingItem(
                        i,
                        Event.current,
                        reorderInteractionRect,
                        secondaryInteractionRect
                        );

                    if (isValidScene)
                    {
                        if (GUILayout.Button("Open"))
                        {
                            EditorSceneLoader.TryOpenFromPath(entry.SceneReference.ScenePath, OpenSceneMode.Single, out Scene _);
                        }
                    }

                    SerializedProperty entriesProperty = GetEntriesPropertyLogic.Execute(sceneGroupEditor);
                    SerializedProperty entryProperty = entriesProperty.GetArrayElementAtIndex(i);

                    entryProperty.ForeachVisibleChildren(DrawChildPropertyField);

                    if (isValidScene)
                    {
                        SceneEntryCustomDrawersDrawer.Draw(toolData, sceneGroup, entry);
                    }
                }
            }

            // Finish dragging
            int startIndex;
            int endIndex;
            bool dragged = reorderableHelper.ResolveDragging(Event.current, out startIndex, out endIndex);

            if (dragged)
            {
                ReorderSceneEntriesLogic.Execute(sceneGroup, startIndex, endIndex);

                EditorUtility.SetDirty(sceneGroup);
            }
        }

        private static void DrawChildPropertyField(SerializedProperty childProperty)
        {
            EditorGUILayout.PropertyField(
                childProperty,
                includeChildren: true
                );
        }
    }
}
