using System;
using System.Threading;
using System.Threading.Tasks;
using GUtilsUnity.Disposing.Disposables;
using GUtilsUnity.Optionals;
using GUtilsUnity.SceneManagement.Loader;
using GUtilsUnity.Extensions;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GUtilsUnity.Loadables
{
    public sealed class SceneInstallerAsyncLoadable<T> : IAsyncLoadable<T>
        where T : MonoBehaviour
    {
        readonly string _sceneName;
        readonly bool _setAsActiveScene;

        public SceneInstallerAsyncLoadable(string sceneName, bool setAsActiveScene)
        {
            _sceneName = sceneName;
            _setAsActiveScene = setAsActiveScene;
        }

        public async Task<IAsyncDisposable<T>> Load(CancellationToken ct)
        {
            Optional<Scene> sceneLoadResult = await RuntimeSceneLoader.LoadFromName(
                _sceneName,
                LoadSceneMode.Additive,
                _setAsActiveScene
            );

            bool sceneLoaded = sceneLoadResult.TryGet(out Scene scene);

            if (!sceneLoaded)
            {
                throw new Exception($"Scene {_sceneName} could not be loaded for {nameof(SceneInstallerAsyncLoadable<T>)}");
            }

            bool instanceFound = scene.TryGetRootComponent(out T instance);

            if(!instanceFound)
            {
                throw new Exception($"{nameof(MonoBehaviour)} instance could not be found at scene {_sceneName} on {nameof(SceneInstallerAsyncLoadable<T>)}");
            }

            return new CallbackAsyncDisposable<T>(
                instance,
                async o =>
                {
                    await RuntimeSceneLoader.UnloadFromName(_sceneName);
                });
        }
    }
}
