using GUtilsUnity.SceneManagement.Group.CustomDrawers;
using GUtilsUnity.SceneManagement.Group.Data;

namespace GUtilsUnity.SceneManagement.Group.Drawers
{
    public static class SceneEntryCustomDrawersDrawer
    {
        public static void Draw(ToolData toolData, SceneGroup sceneGroup, SceneGroupEntry sceneGroupEntry)
        {
            foreach(ISceneEntryCustomDrawer customDrawer in toolData.SceneEntryCustomDrawers)
            {
                customDrawer.OnInspectorGUI(sceneGroup, sceneGroupEntry);
            }
        }
    }
}
