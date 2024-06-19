namespace GUtilsUnity.SceneManagement.Group.CustomDrawers
{
    public interface ISceneEntryCustomDrawer
    {
        void OnInspectorGUI(
            SceneGroup sceneGroup,
            SceneGroupEntry sceneGroupEntry
            );
    }
}
