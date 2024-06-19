using UnityEditor;

namespace GUtilsUnity.SceneManagement.Group.Logic
{
    public static class AddSceneEntryLogic
    {
        public static void Execute(
            SceneGroup sceneGroup
            )
        {
            sceneGroup.Entries.Add(new SceneGroupEntry());

            EditorUtility.SetDirty(sceneGroup);
        }
    }
}
