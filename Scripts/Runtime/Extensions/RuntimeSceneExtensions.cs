using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using GUtils.DiscriminatedUnions;
using GUtils.Optionals;
using GUtils.Types;
using GUtilsUnity.SceneManagement.Collections;
using GUtilsUnity.SceneManagement.Reference;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GUtilsUnity.Extensions
{
    /// <summary>
    /// Scene management at run-time.
    /// </summary>
    public static class RuntimeSceneExtensions
    {
        /// <summary>
        /// Checks if a scene exists based on the given scene reference.
        /// </summary>
        /// <param name="sceneReference">The scene reference to check.</param>
        /// <returns>True if the scene exists, false otherwise.</returns>
        public static bool ExistsFromReference(ISceneReference sceneReference)
        {
            return ExistsFromPath(sceneReference.ScenePath);
        }

        /// <summary>
        /// Checks if a scene exists based on the given scene path.
        /// </summary>
        /// <param name="scenePath">The path of the scene to check.</param>
        /// <returns>True if the scene exists, false otherwise.</returns>
        public static bool ExistsFromPath(string scenePath)
        {
            return SceneUtility.GetBuildIndexByScenePath(scenePath) >= 0;
        }

        /// <summary>
        /// Loads a scene asynchronously based on a scene reference.
        /// </summary>
        /// <param name="sceneReference">The scene reference representing the scene to load.</param>
        /// <param name="mode">The mode to use for loading the scene.</param>
        /// <param name="setAsActive">Specifies whether the loaded scene should be set as the active scene.</param>
        /// <returns>A task that represents the asynchronous loading operation.
        /// The task result contains an optional <see cref="Scene"/>.</returns>
        public static Task<Optional<Scene>> LoadFromReference(
            ISceneReference sceneReference,
            LoadSceneMode mode,
            bool setAsActive
            )
        {
            return LoadFromPath(sceneReference.ScenePath, mode, setAsActive);
        }

        /// <summary>
        /// Loads a scene asynchronously based on a scene reference,
        /// and returns either the loaded scene or an error message.
        /// </summary>
        /// <param name="sceneReference">The scene reference representing the scene to load.</param>
        /// <param name="mode">The mode to use for loading the scene.</param>
        /// <param name="setAsActive">Specifies whether the loaded scene should be set as the active scene.</param>
        /// <returns>A task that represents the asynchronous loading operation. The task result contains a
        /// <see cref="OneOf{TFirst,TSecond}"/> representing either the loaded <see cref="Scene"/> or an <see cref="ErrorMessage"/>.</returns>
        public static Task<OneOf<Scene, ErrorMessage>> LoadFromReferenceWithError(
            ISceneReference sceneReference,
            LoadSceneMode mode,
            bool setAsActive
            )
        {
            return LoadFromPathWithError(sceneReference.ScenePath, mode, setAsActive);
        }

        /// <summary>
        /// Loads a scene asynchronously based on the scene path.
        /// </summary>
        /// <param name="scenePath">The path of the scene to load.</param>
        /// <param name="mode">The mode to use for loading the scene.</param>
        /// <param name="setAsActive">Specifies whether the loaded scene should be set as the active scene.</param>
        /// <returns>A task that represents the asynchronous loading operation.
        /// The task result contains an optional <see cref="Scene"/>.</returns>
        public static Task<Optional<Scene>> LoadFromPath(string scenePath, LoadSceneMode mode, bool setAsActive)
        {
            string sceneName = Path.GetFileNameWithoutExtension(scenePath);

            return LoadFromName(sceneName, mode, setAsActive);
        }

        /// <summary>
        /// Loads a scene asynchronously based on the scene path,
        /// and returns either the loaded scene or an error message.
        /// </summary>
        /// <param name="scenePath">The path of the scene to load.</param>
        /// <param name="mode">The mode to use for loading the scene.</param>
        /// <param name="setAsActive">Specifies whether the loaded scene should be set as the active scene.</param>
        /// <returns>A task that represents the asynchronous loading operation.
        /// The task result contains a <see cref="OneOf{T0,T1}"/> representing either the loaded
        /// <see cref="Scene"/> or an <see cref="ErrorMessage"/>.</returns>
        public static Task<OneOf<Scene, ErrorMessage>> LoadFromPathWithError(
            string scenePath,
            LoadSceneMode mode,
            bool setAsActive
            )
        {
            string sceneName = Path.GetFileNameWithoutExtension(scenePath);

            return LoadFromNameWithError(sceneName, mode, setAsActive);
        }

        /// <summary>
        /// Loads a scene asynchronously based on the scene name.
        /// </summary>
        /// <param name="sceneName">The name of the scene to load.</param>
        /// <param name="mode">The mode to use for loading the scene.</param>
        /// <param name="setAsActive">Specifies whether the loaded scene should be set as the active scene.</param>
        /// <returns>A task that represents the asynchronous loading operation.
        /// The task result contains an optional <see cref="Scene"/> if it could be loaded properly.</returns>
        public static async Task<Optional<Scene>> LoadFromName(string sceneName, LoadSceneMode mode, bool setAsActive)
        {
            OneOf<Scene, ErrorMessage> result = await LoadFromNameWithError(sceneName, mode, setAsActive);

            bool hasError = result.TryGetSecond(
                out ErrorMessage errorMessage
            );

            if (hasError)
            {
                UnityEngine.Debug.LogError(errorMessage.Message);
                return Optional<Scene>.None;
            }

            return result.UnsafeGetFirst();
        }

        /// <summary>
        /// Loads a scene asynchronously based on the scene name and load settings,
        /// and returns either the loaded scene or an error message.
        /// </summary>
        /// <param name="sceneName">The name of the scene to load.</param>
        /// <param name="mode">The mode to use for loading the scene.</param>
        /// <param name="setAsActive">Specifies whether the loaded scene should be set as the active scene.</param>
        /// <returns>A task that represents the asynchronous loading operation.
        /// The task result contains a <see cref="OneOf{T0,T1}"/> representing either the loaded
        /// <see cref="Scene"/> or an <see cref="ErrorMessage"/>.</returns>
        public static async Task<OneOf<Scene, ErrorMessage>> LoadFromNameWithError(string sceneName, LoadSceneMode mode, bool setAsActive)
        {
            AsyncOperation asyncLoad;

            try
            {
                asyncLoad = SceneManager.LoadSceneAsync(sceneName, mode);
            }
            catch (Exception e)
            {
                return new ErrorMessage(e.Message);
            }

            if (asyncLoad == null)
            {
                return new ErrorMessage($"There was an error loading scene: {sceneName}. {nameof(AsyncOperation)} was null.");
            }

            TaskCompletionSource<OneOf<Scene, ErrorMessage>> taskCompletionSource = new();

            asyncLoad.completed += _ =>
            {
                Scene loadedScene = SceneManager.GetSceneByName(sceneName);

                if (!loadedScene.IsValid())
                {
                    taskCompletionSource.SetResult(
                        new ErrorMessage($"There was an error loading scene: {sceneName}. Loaded scene is not valid at {nameof(RuntimeSceneExtensions)}")
                    );
                    return;
                }

                if (setAsActive)
                {
                    SceneManager.SetActiveScene(loadedScene);
                }

                taskCompletionSource.SetResult(loadedScene);
            };

            OneOf<Scene, ErrorMessage> result = await taskCompletionSource.Task;

            await Task.Yield();

            return result;
        }

        /// <summary>
        /// Unloads a scene asynchronously based on a scene reference.
        /// </summary>
        /// <param name="sceneReference">The scene reference representing the scene to unload.</param>
        /// <returns>A task that represents the asynchronous unloading operation.
        /// The task result contains a boolean value indicating whether the scene was successfully unloaded.</returns>
        public static Task<bool> UnloadFromReference(ISceneReference sceneReference)
        {
            return UnloadFromPath(sceneReference.ScenePath);
        }

        /// <summary>
        /// Unloads a scene asynchronously based on the scene path.
        /// </summary>
        /// <param name="scenePath">The path of the scene to unload.</param>
        /// <returns>A task that represents the asynchronous unloading operation.
        /// The task result contains a boolean value indicating whether the scene was successfully unloaded.</returns>
        public static Task<bool> UnloadFromPath(string scenePath)
        {
            string sceneName = Path.GetFileNameWithoutExtension(scenePath);

            return UnloadFromName(sceneName);
        }

        /// <summary>
        /// Unloads a scene asynchronously based on the scene name.
        /// </summary>
        /// <param name="sceneName">The name of the scene to unload.</param>
        /// <returns>A task that represents the asynchronous unloading operation.
        /// The task result contains a boolean value indicating whether the scene was successfully unloaded.</returns>
        public static Task<bool> UnloadFromName(string sceneName)
        {
            TaskCompletionSource<bool> taskCompletionSource = new TaskCompletionSource<bool>();

            Scene loadedScene = SceneManager.GetSceneByName(sceneName);

            if (!loadedScene.IsValid())
            {
                // Is already unloaded or wrong name.
                // We only need to log errors at loading.
                return Task.FromResult(true);
            }

            AsyncOperation asyncUnload = SceneManager.UnloadSceneAsync(loadedScene);

            if (asyncUnload == null)
            {
                return Task.FromResult(false);
            }

            asyncUnload.completed += _ => { taskCompletionSource.SetResult(true); };

            return taskCompletionSource.Task;
        }

        /// <summary>
        /// Loads a collection of scenes asynchronously.
        /// </summary>
        /// <param name="sceneCollection">The scene collection to load.</param>
        /// <param name="mode">The load scene mode.</param>
        /// <returns>A task representing the asynchronous loading operation.
        /// The task result contains a list of optional scenes that were loaded.</returns>
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

                Optional<Scene> result = await LoadFromPath(sceneEntry.ScenePath, actualMode, sceneEntry.LoadAsActive);

                ret.Add(result);
            }

            return ret;
        }

        /// <summary>
        /// Unloads a collection of scenes asynchronously.
        /// </summary>
        /// <param name="sceneCollection">The scene collection to unload.</param>
        /// <returns>A task representing the asynchronous unloading operation.
        /// The task result contains a list of boolean values indicating whether
        /// the scenes were successfully unloaded.</returns>
        public static async Task<List<bool>> Unload(ISceneCollection sceneCollection)
        {
            List<bool> ret = new List<bool>();

            foreach (ISceneCollectionEntry sceneEntry in sceneCollection.SceneEntries)
            {
                bool result = await UnloadFromPath(sceneEntry.ScenePath);

                ret.Add(result);
            }

            return ret;
        }
    }
}
