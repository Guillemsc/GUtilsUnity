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
    [Obsolete("Use AsyncDiContext instead with the scene extension")]
    public sealed class SceneDiContext<TResult, TMonoBehaviourInstance>
        where TMonoBehaviourInstance : IInstaller
    {
        readonly string _sceneName;
        readonly bool _setAsActiveScene;
        readonly List<IInstaller> _installers = new();

        bool hasValidContainer;
        IDiContainer _container;

        public SceneDiContext(
            string sceneName,
            bool setAsActiveScene = false,
            params IInstaller[] installers
            )
        {
            _sceneName = sceneName;
            _setAsActiveScene = setAsActiveScene;

            AddInstallers(installers);
        }

        public void AddInstallers(params IInstaller[] installers)
        {
            if (hasValidContainer)
            {
                throw new Exception($"Can't Add more installers when container is already installed {nameof(SceneDiContext<TResult, TMonoBehaviourInstance>)}");
            }

            _installers.AddRange(installers);
        }

        public async Task<ITaskDisposable<TResult>> Install()
        {
            Optional<Scene> sceneLoadResult = await RuntimeSceneLoader.LoadFromName(
                _sceneName,
                LoadSceneMode.Additive,
                _setAsActiveScene
                );

            bool sceneLoaded = sceneLoadResult.TryGet(out Scene scene);

            if (!sceneLoaded)
            {
                throw new Exception($"Scene {_sceneName} could not be loaded for {nameof(SceneDiContext<TResult, TMonoBehaviourInstance>)}");
            }

            bool instanceFound = scene.TryGetRootComponent(out TMonoBehaviourInstance instance);

            if(!instanceFound)
            {
                throw new Exception($"{typeof(TMonoBehaviourInstance).Name} instance not found for {nameof(SceneDiContext<TResult, TMonoBehaviourInstance>)}");
            }

            AddInstallers(instance);

            _container = DiContainerBuilderExtensions.BuildFromInstallers(_installers);

            Task Dispose(TResult result)
            {
                hasValidContainer = false;

                _container.Dispose();

                return RuntimeSceneLoader.UnloadFromName(_sceneName);
            }

            TResult result = _container.Resolve<TResult>();

            hasValidContainer = true;

            return new CallbackTaskDisposable<TResult>(
                result,
                Dispose
            );
        }

        public IDiContainer GetContainerUnsafe()
        {
            if (!hasValidContainer)
            {
                throw new AccessViolationException("Tried to get container but it was not created or already disposed");
            }

            return _container;
        }
    }
}
