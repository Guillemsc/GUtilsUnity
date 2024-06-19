namespace GUtilsUnity.SceneManagement.Collections
{
    public interface ISceneCollectionEntry
    {
        string ScenePath { get; }
        bool LoadAsActive { get; }
    }
}
