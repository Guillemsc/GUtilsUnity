using GUtilsUnity.SceneManagement.Loader;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace GUtilsUnity.SceneManagement.Group.Drawers
{
    public static class OpenCloseDrawer
    {
        public static void Draw(
            SceneGroup sceneGroup
            )
        {
            using (new GUILayout.HorizontalScope(EditorStyles.helpBox))
            {
                if (GUILayout.Button($"Open All"))
                {
                    EditorSceneLoader.Open(sceneGroup, OpenSceneMode.Single);
                }

                if (GUILayout.Button($"Close All"))
                {
                    EditorSceneLoader.Close(sceneGroup);
                }
            }
        }
    }
}
