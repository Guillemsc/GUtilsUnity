using GUtilsUnity.Disposing.Disposables;
using GUtilsUnity.Extensions;
using UnityEngine;

namespace GUtilsUnity.Loadables
{
    public sealed class ScriptableResourceLoadable<T> : ILoadable<T>
        where T : ScriptableObject
    {
        readonly string _resource;

        public ScriptableResourceLoadable(string resource)
        {
            _resource = resource;
        }

        public IDisposable<T> Load()
        {
            T scriptableResource = Resources.Load<T>(_resource);

            var disposable = new CallbackDisposable<T>(
                scriptableResource,
                DelegateExtensions.DoNothing);

            return disposable;
        }
    }
}
