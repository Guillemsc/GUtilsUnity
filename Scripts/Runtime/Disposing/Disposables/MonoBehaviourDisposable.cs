using GUtilsUnity.Extensions;
using UnityEngine;

namespace GUtilsUnity.Disposing.Disposables
{
    public sealed class MonoBehaviourDisposable<T> : IDisposable<T> where T : MonoBehaviour
    {
        public T Value { get; }

        public MonoBehaviourDisposable(T value)
        {
            Value = value;
        }

        public void Dispose()
        {
            Value.gameObject.Unparent();
            Value.DestroyGameObject();
        }
    }
}
