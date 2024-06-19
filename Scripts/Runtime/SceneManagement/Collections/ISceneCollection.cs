using System.Collections.Generic;

namespace GUtilsUnity.SceneManagement.Collections
{
    public interface ISceneCollection
    {
        IReadOnlyList<ISceneCollectionEntry> SceneEntries { get; }
    }
}
