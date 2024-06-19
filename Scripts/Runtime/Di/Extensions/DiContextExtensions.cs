using GUtilsUnity.Di.Contexts;
using GUtilsUnity.Di.Installers;
using GUtilsUnity.Loadables;
using UnityEngine;

namespace GUtilsUnity.Di.Extensions
{
    public static class DiContextExtensions
    {
        public static IDiContext<TResult> AddComponentPrefabInstaller<TResult>(this IDiContext<TResult> asyncDiContext, MonoBehaviourInstaller prefab)
            => asyncDiContext.AddInstallerLoadable(new ComponentPrefabInstantiateLoadable<MonoBehaviourInstaller>(prefab));

        public static IDiContext<TResult> AddPrefabInstaller<TResult>(this IDiContext<TResult> asyncDiContext, GameObject prefab)
            => asyncDiContext.AddInstallerLoadable(new PrefabInstantiateLoadable<IInstaller>(prefab));

        public static IDiContext<TResult> AddPrefabResourceInstantiateComponentInstaller<TResult>(this IDiContext<TResult> asyncDiContext, string resource, Transform parent = null)
            => asyncDiContext.AddInstallerLoadable(new PrefabResourceInstantiateComponentLoadable<IInstaller>(resource, parent));
    }
}
