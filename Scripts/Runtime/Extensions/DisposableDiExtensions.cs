using System;
using GUtilsUnity.Di.Builder;

namespace GUtilsUnity.Extensions
{
    public static class DisposableDiExtensions
    {
        public static IDiBindingActionBuilder<T> LinkDisposable<T>(this IDiBindingActionBuilder<T> actionBuilder)
            where T : IDisposable
        {
            return actionBuilder.WhenDispose(o => o.Dispose);
        }
    }
}
