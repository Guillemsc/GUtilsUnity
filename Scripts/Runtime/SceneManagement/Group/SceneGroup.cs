using System.Collections.Generic;
using GUtilsUnity.SceneManagement.Collections;
using UnityEngine;

namespace GUtilsUnity.SceneManagement.Group
{
    [CreateAssetMenu(fileName = "SceneGroup", menuName = "PopcoreCore/SceneManagement/SceneGroup", order = 1)]
    public sealed class SceneGroup : ScriptableObject, ISceneCollection
    {
        [SerializeField] public List<SceneGroupEntry> Entries = new();

        List<ISceneCollectionEntry> _sceneEntries;

        public IReadOnlyList<ISceneCollectionEntry> SceneEntries => GenerateCollectionEntries();

        IReadOnlyList<ISceneCollectionEntry> GenerateCollectionEntries()
        {
            if (!Application.isEditor)
            {
                if(_sceneEntries != null)
                {
                    return _sceneEntries;
                }
            }

            _sceneEntries = new List<ISceneCollectionEntry>();

            foreach(SceneGroupEntry entry in Entries)
            {
                if(entry.SceneReference == null)
                {
                    UnityEngine.Debug.LogError($"Missing scene detected at {nameof(SceneGroup)} {name}");
                    continue;
                }

                if(string.IsNullOrEmpty(entry.SceneReference.ScenePath))
                {
                    UnityEngine.Debug.LogError($"Missing scene detected at {nameof(SceneGroup)} {name}");
                    continue;
                }

                SceneCollectionEntry sceneEntry = new SceneCollectionEntry(
                    entry.SceneReference.ScenePath,
                    entry.LoadAsActive
                    );

                _sceneEntries.Add(sceneEntry);
            }

            return _sceneEntries;
        }
    }
}
