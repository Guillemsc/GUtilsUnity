using System;

namespace GUtilsUnity.Disposing.Disposables
{
    public interface IAsyncDisposable<out T> : IAsyncDisposable
    {
        public T Value { get; }
    }
}
