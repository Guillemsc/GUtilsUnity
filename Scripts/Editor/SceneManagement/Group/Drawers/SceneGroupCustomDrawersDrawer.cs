using GUtilsUnity.SceneManagement.Group.CustomDrawers;
using GUtilsUnity.SceneManagement.Group.Data;

namespace GUtilsUnity.SceneManagement.Group.Drawers
{
    public static class SceneGroupCustomDrawersDrawer
    {
        public static void Draw(ToolData toolData, SceneGroup sceneGroup)
        {
            foreach (ISceneGroupCustomDrawer customDrawer in toolData.SceneGroupCustomDrawer)
            {
                customDrawer.OnInspectorGUI(sceneGroup);
            }
        }
    }
}
