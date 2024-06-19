using System.Collections.Generic;
using GUtilsUnity.SceneManagement.Group.CustomDrawers;

namespace GUtilsUnity.SceneManagement.Group.Data
{
    public sealed class ToolData
    {
        public List<SceneGroupEntry> EntriesToRemove { get; } = new List<SceneGroupEntry>();
        public Dictionary<SceneGroupEntry, bool> LastUpdateSceneEntryLoadAsActiveMap { get; } = new Dictionary<SceneGroupEntry, bool>();

        public List<ISceneEntryCustomDrawer> SceneEntryCustomDrawers { get; } = new List<ISceneEntryCustomDrawer>();
        public List<ISceneGroupCustomDrawer> SceneGroupCustomDrawer { get; } = new List<ISceneGroupCustomDrawer>();
    }
}
