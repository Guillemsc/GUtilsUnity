using GUtilsUnity.SceneManagement.Group.Data;
using GUtilsUnity.SceneManagement.Group.Logic;
using UnityEditor;
using UnityEngine;

namespace GUtilsUnity.SceneManagement.Group.Drawers
{
    public static class EntryContextMenuDrawer
    {
        public static void Draw(
            ToolData toolData,
            SceneGroupEntry sceneGroupEntry
            )
        {
            GenericMenu menu = new GenericMenu();

            menu.AddItem(new GUIContent("Remove"), false,
                () =>
                {
                    RemoveSceneEntryLogic.Execute(toolData, sceneGroupEntry);
                });

            menu.ShowAsContext();
        }

    }
}
