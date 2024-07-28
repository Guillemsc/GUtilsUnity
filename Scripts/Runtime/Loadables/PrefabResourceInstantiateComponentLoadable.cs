using GUtils.Disposing.Disposables;
using GUtils.Loadables;
using GUtilsUnity.Disposing.Disposables;
using GUtilsUnity.Extensions;
using UnityEngine;

namespace GUtilsUnity.Loadables
{
    public sealed class PrefabResourceInstantiateComponentLoadable<T> : ILoadable<T>
    {
        readonly string _resource;
        readonly Transform _parent;

        public PrefabResourceInstantiateComponentLoadable(
            string resource,
            Transform parent = null)
        {
            _resource = resource;
            _parent = parent;
        }

        public IDisposable<T> Load()
        {
            GameObject gameObjectResource = Resources.Load<GameObject>(_resource);
            GameObject instance = Object.Instantiate(gameObjectResource, _parent, worldPositionStays: true);
            T component = instance.GetComponent<T>();

            var disposable = new CallbackDisposable<T>(
                component,
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
