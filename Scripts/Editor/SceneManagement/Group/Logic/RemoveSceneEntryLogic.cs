using GUtilsUnity.SceneManagement.Group.Data;

namespace GUtilsUnity.SceneManagement.Group.Logic
{
    public static class RemoveSceneEntryLogic
    {
        public static void Execute(
            ToolData toolData,
            SceneGroupEntry sceneGroupEntry
            )
        {
            toolData.EntriesToRemove.Add(sceneGroupEntry);
        }
    }
}
