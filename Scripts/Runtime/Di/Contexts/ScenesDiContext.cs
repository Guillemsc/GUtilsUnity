using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GUtilsUnity.Di.Container;
using GUtilsUnity.Di.Extensions;
using GUtilsUnity.Di.Installers;
using GUtilsUnity.Disposing.Disposables;
using GUtilsUnity.Optionals;
using GUtilsUnity.SceneManagement.Loader;
using GUtilsUnity.Extensions;
using UnityEngine.SceneManagement;

namespace GUtilsUnity.Di.Contexts
{
    [Obsolete("Use AsyncDiContext with scene extensions")]
    public sealed class ScenesDiContext<TResult>
    {
        readonly List<string> _sceneNames = new();
        readonly List<IInstaller> _installers = new();

        bool _hasValidContainer;
        IDiContainer _container;

        public ScenesDiContext(
            IReadOnlyList<string> sceneNames,
            params IInstaller[] installers
            )
        {
            AddSceneNames(sceneNames);
            AddInstallers(installers);
        }

        public void AddSceneNames(params string[] installers)
        {
            AddSceneNames((IReadOnlyList<string>)installers);
        }

        public void AddSceneNames(IReadOnlyList<string> installers)
        {
            if (_hasValidContainer)
            {
                throw new Exception($"Can't Add more scene names when container is already installed {nameof(ScenesDiContext<TResult>)}");
            }

            _sceneNames.AddRange(installers);
        }

        public void AddInstallers(params IInstaller[] installers)
        {
            if (_hasValidContainer)
            {
                throw new Exception($"Can't Add more installers when container is already installed {nameof(ScenesDiContext<TResult>)}");
            }

            _installers.AddRange(installers);
        }

        public async Task<ITaskDisposable<TResult>> Install()
        {
            foreach (string sceneName in _sceneNames)
            {
                Optional<Scene> sceneLoadResult = await RuntimeSceneLoader.LoadFromName(
                    sceneName,
                    LoadSceneMode.Additive,
                    false
                );

                bool sceneLoaded = sceneLoadResult.TryGet(out Scene scene);

                if (!sceneLoaded)
                {
                    throw new Exception($"Scene {sceneName} could not be loaded for {nameof(ScenesDiContext<TResult>)}");
                }

                bool instanceFound = scene.TryGetRootComponent(out MonoBehaviourInstaller instance);

                if(!instanceFound)
                {
                    throw new Exception($"{nameof(MonoBehaviourInstaller)} instance not at scene {sceneName} found for {nameof(ScenesDiContext<TResult>)}");
                }

                AddInstallers(instance);
            }

            _container = DiContainerBuilderExtensions.BuildFromInstallers(_installers);

            async Task Dispose(TResult result)
            {
                _hasValidContainer = false;

                _container.Dispose();

                foreach (string sceneName in _sceneNames)
                {
                    await RuntimeSceneLoader.UnloadFromName(sceneName);
                }
            }

            TResult result = _container.Resolve<TResult>();

            _hasValidContainer = true;

            return new CallbackTaskDisposable<TResult>(
                result,
                Dispose
            );
        }

        public IDiContainer GetContainerUnsafe()
        {
            if (!_hasValidContainer)
            {
                throw new AccessViolationException("Tried to get container but it was not created or already disposed");
            }

            return _container;
        }
    }
}
