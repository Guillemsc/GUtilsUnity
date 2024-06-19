using GUtilsUnity.Di.Contexts;
using GUtilsUnity.Di.Installers;
using GUtilsUnity.Loadables;
using UnityEngine;

namespace GUtilsUnity.Di.Extensions
{
    public static class AsyncDiContextExtensions
    {
        public static IAsyncDiContext<TResult> AddSceneInstaller<TResult>(
            this IAsyncDiContext<TResult> asyncDiContext,
            string sceneName,
            bool setAsActiveScene = false)
            => asyncDiContext.AddInstallerAsyncLoadable(
                new TypeAdapterAsyncLoadable<MonoBehaviourInstaller, IInstaller>(
                    new SceneInstallerAsyncLoadable<MonoBehaviourInstaller>(sceneName, setAsActiveScene)
                ));

        public static IAsyncDiContext<TResult> AddComponentPrefabInstantiateInstaller<TResult>(this IAsyncDiContext<TResult> asyncDiContext, MonoBehaviourInstaller prefab)
            => asyncDiContext.AddInstallerLoadable(new ComponentPrefabInstantiateLoadable<MonoBehaviourInstaller>(prefab));

        public static IAsyncDiContext<TResult> AddGameObjectPrefabInstantiateInstaller<TResult>(this IAsyncDiContext<TResult> asyncDiContext, GameObject prefab)
            => asyncDiContext.AddInstallerLoadable(new PrefabInstantiateLoadable<IInstaller>(prefab));
    }
}
