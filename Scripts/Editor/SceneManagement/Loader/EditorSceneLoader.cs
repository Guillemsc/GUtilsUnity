using System.Collections.Generic;
using System.Threading.Tasks;
using GUtils.Optionals;
using GUtilsUnity.Extensions;
using GUtilsUnity.SceneManagement.Collections;
using GUtilsUnity.SceneManagement.Reference;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GUtilsUnity.SceneManagement.Loader
{
    public static class EditorSceneLoader
    {
        /// <summary>
        /// Tries to open a scene by its reference.
        /// </summary>
        /// <param name="sceneReference">The reference of the scene to open.</param>
        /// <param name="mode">The open mode for the scene.</param>
        /// <param name="scene">The opened scene, if successful.</param>
        /// <returns>A boolean indicating whether the scene was successfully opened.</returns>
        public static bool TryOpenFromReference(SceneReference sceneReference, OpenSceneMode mode, out Scene scene)
        {
            return TryOpenFromPath(sceneReference.ScenePath, mode, out scene);
        }

        /// <summary>
        /// Tries to open a scene by its path.
        /// </summary>
        /// <param name="scenePath">The path of the scene to open.</param>
        /// <param name="mode">The open mode for the scene.</param>
        /// <param name="scene">The opened scene, if successful.</param>
        /// <returns>A boolean indicating whether the scene was successfully opened.</returns>
        public static bool TryOpenFromPath(string scenePath, OpenSceneMode mode, out Scene scene)
        {
            scene = EditorSceneManager.OpenScene(scenePath, mode);

            if (!scene.IsValid())
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Tries to open a scene by its name.
        /// </summary>
        /// <param name="sceneName">The name of the scene to open.</param>
        /// <param name="mode">The open mode for the scene.</param>
        /// <param name="scene">The opened scene, if successful.</param>
        /// <returns>A boolean indicating whether the scene was successfully opened.</returns>
        public static bool TryOpenFromName(string sceneName, OpenSceneMode mode, out Scene scene)
        {
            bool scenePathFound = AssetDatabaseExtensions.TryGetScenePathFromSceneName(
                sceneName,
                out string scenePath
            );

            if (!scenePathFound)
            {
                scene = default;
                return false;
            }

            return TryOpenFromPath(scenePath, mode, out scene);
        }

        /// <summary>
        /// Tries to open a scene by its name.
        /// </summary>
        /// <param name="sceneName">The name of the scene to open.</param>
        /// <param name="mode">The open mode for the scene.</param>
        /// <returns>A boolean indicating whether the scene was successfully opened.</returns>
        public static bool TryOpenFromName(string sceneName, OpenSceneMode mode)
        {
            return TryOpenFromName(sceneName, mode, out Scene _);
        }

        /// <summary>
        /// Opens a collection of scenes.
        /// </summary>
        /// <param name="sceneCollection">The collection of scenes to open.</param>
        /// <param name="mode">The open mode for the first scene in the collection.</param>
        /// <returns>A list of scenes that were successfully opened.</returns>
        public static List<Scene> Open(ISceneCollection sceneCollection, OpenSceneMode mode)
        {
            List<Scene> ret = new List<Scene>();

            bool first = true;

            foreach (ISceneCollectionEntry sceneEntry in sceneCollection.SceneEntries)
            {
                OpenSceneMode actualMode = OpenSceneMode.Additive;

                if (first)
                {
                    first = false;

                    actualMode = mode;
                }

                bool opened = TryOpenFromPath(sceneEntry.ScenePath, actualMode, out Scene loadedScene);

                if (!opened)
                {
                    UnityEngine.Debug.LogError($"There was an error opening scene {sceneEntry}");
                    continue;
                }

                bool shouldBeSetToActive = sceneEntry.LoadAsActive;

                if (shouldBeSetToActive)
                {
                    SceneManager.SetActiveScene(loadedScene);
                }

                ret.Add(loadedScene);
            }

            return ret;
        }

        /// <summary>
        /// Closes a collection of scenes.
        /// </summary>
        /// <param name="sceneCollection">The collection of scenes to close.</param>
        /// <returns>A list of booleans, where each boolean indicates whether the corresponding
        /// scene in the collection was successfully closed.</returns>
        public static List<bool> Close(ISceneCollection sceneCollection)
        {
            List<bool> ret = new List<bool>();

            foreach (ISceneCollectionEntry sceneEntry in sceneCollection.SceneEntries)
            {
                Scene scene = SceneManager.GetSceneByPath(sceneEntry.ScenePath);

                if (!scene.IsValid())
                {
                    continue;
                }

                bool result = EditorSceneManager.CloseScene(scene, removeScene: true);

                ret.Add(result);
            }

            return ret;
        }

        /// <summary>
        /// Loads a scene by its path.
        /// </summary>
        /// <param name="scenePath">The path of the scene to load.</param>
        /// <param name="mode">The load mode for the scene.</param>
        /// <returns>A task that represents the asynchronous operation. The result of the task is an optional scene,
        /// which represents a loaded scene or an error if the scene could not be loaded.</returns>
        public static async Task<Optional<Scene>> Load(string scenePath, LoadSceneMode mode)
        {
            TaskCompletionSource<Optional<Scene>> taskCompletionSource = new();

            LoadSceneParameters loadParameters = new LoadSceneParameters()
            {
                loadSceneMode = mode
            };

            AsyncOperation asyncLoad = EditorSceneManager.LoadSceneAsyncInPlayMode(scenePath, loadParameters);

            if (asyncLoad == null)
            {
                return Optional<Scene>.None;
            }

            asyncLoad.completed += (AsyncOperation operation) =>
            {
                Scene loadedScene = SceneManager.GetSceneByPath(scenePath);

                if (!loadedScene.IsValid())
                {
                    UnityEngine.Debug.LogError($"There was an error loading scene: {scenePath}. Loaded scene is not valid at {nameof(RuntimeSceneExtensions)}");
                }

                taskCompletionSource.SetResult(Optional<Scene>.Some(loadedScene));
            };

            Optional<Scene> result = await taskCompletionSource.Task;

            await Task.Yield();

            return result;
        }

        /// <summary>
        /// Unloads a scene by its name.
        /// </summary>
        /// <param name="sceneName">The name of the scene to unload.</param>
        /// <returns>A task that represents the asynchronous operation.
        /// The result of the task is a boolean indicating whether the scene was successfully unloaded.</returns>
        public static Task<bool> UnloadFromName(string sceneName)
        {
            return RuntimeSceneExtensions.UnloadFromName(sceneName);
        }

        /// <summary>
        /// Unloads a scene by its path.
        /// </summary>
        /// <param name="scenePath">The path of the scene to unload.</param>
        /// <returns>A task that represents the asynchronous operation. The result of the task
        /// is a boolean indicating whether the scene was successfully unloaded.</returns>
        public static Task<bool> UnloadFromPath(string scenePath)
        {
            return RuntimeSceneExtensions.UnloadFromPath(scenePath);
        }

        /// <summary>
        /// Loads a collection of scenes.
        /// </summary>
        /// <param name="sceneCollection">The collection of scenes to load.</param>
        /// <param name="mode">The load mode for the first scene in the collection.</param>
        /// <returns>A task that represents the asynchronous operation. The result of the task
        /// is a list of optional scenes, where each optional scene represents a loaded scene or an
        /// error if the scene could not be loaded.</returns>
        public static async Task<List<Optional<Scene>>> Load(ISceneCollection sceneCollection, LoadSceneMode mode)
        {
            List<Optional<Scene>> ret = new();

            bool first = true;

            foreach (ISceneCollectionEntry sceneEntry in sceneCollection.SceneEntries)
            {
                LoadSceneMode actualMode = LoadSceneMode.Additive;

                if (first)
                {
                    first = false;

                    actualMode = mode;
                }

                Optional<Scene> result = await Load(sceneEntry.ScenePath, actualMode);

                bool hasResult = result.TryGet(out Scene resultScene);

                bool shouldBeSetToActive = sceneEntry.LoadAsActive && hasResult;

                if (shouldBeSetToActive)
                {
                    SceneManager.SetActiveScene(resultScene);
                }

                ret.Add(result);
            }

            return ret;
        }

        /// <summary>
        /// Unloads a collection of scenes.
        /// </summary>
        /// <param name="sceneCollection">The collection of scenes to unload.</param>
        /// <returns>A task that represents the asynchronous operation.
        /// The result of the task is a list of booleans, where each boolean indicates whether the corresponding
        /// scene in the collection was successfully unloaded.</returns>
        public static Task<List<bool>> Unload(ISceneCollection sceneCollection)
        {
            return RuntimeSceneExtensions.Unload(sceneCollection);
        }
    }
}
