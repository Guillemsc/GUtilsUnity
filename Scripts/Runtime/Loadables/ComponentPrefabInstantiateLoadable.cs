using GUtils.Disposing.Disposables;
using GUtils.Loadables;
using GUtilsUnity.Extensions;
using UnityEngine;

namespace GUtilsUnity.Loadables
{
    public sealed class ComponentPrefabInstantiateLoadable<T> : ILoadable<T>
        where T : MonoBehaviour
    {
        readonly T _prefabComponent;

        public ComponentPrefabInstantiateLoadable(T prefabComponent)
        {
            _prefabComponent = prefabComponent;
        }

        public IDisposable<T> Load()
        {
            T instance = UnityEngine.Object.Instantiate(_prefabComponent);

            var disposable = new CallbackDisposable<T>(
                instance,
                o =>
                {
                    o.gameObject.Unparent();
                    o.DestroyGameObject();
                });

            return disposable;
        }
    }
}
