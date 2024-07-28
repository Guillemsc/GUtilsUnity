using System;
using GUtils.Disposing.Disposables;
using GUtils.Loadables;
using GUtilsUnity.Extensions;
using UnityEngine;

namespace GUtilsUnity.Loadables
{
    public sealed class PrefabInstantiateLoadable<T> : ILoadable<T>
    {
        readonly GameObject _prefab;

        public PrefabInstantiateLoadable(GameObject prefab)
        {
            _prefab = prefab;
        }

        public IDisposable<T> Load()
        {
            GameObject instance = UnityEngine.Object.Instantiate(_prefab);

            bool componentInterface = instance.TryGetComponent(out T installer);

            if (!componentInterface)
            {
                throw new Exception($"Prefab {_prefab.name} does not have a component with interface {typeof(T).FullName}");
            }

            var disposable = new CallbackDisposable<T>(
                installer,
                o =>
                {
                    var disposingComponent = o as Component;
                    disposingComponent!.gameObject.Unparent();
                    disposingComponent.DestroyGameObject();
                });

            return disposable;
        }
    }
}
