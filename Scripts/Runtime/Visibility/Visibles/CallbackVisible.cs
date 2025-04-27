using System;
using System.Threading;
using System.Threading.Tasks;
using GUtils.Visibility.Visibles;

namespace GUtilsUnity.Visibility.Visibles
{
    public sealed class CallbackVisible : IVisible
    {
        readonly Func<bool, bool, CancellationToken, Task> _callback;

        public CallbackVisible(Func<bool, bool, CancellationToken, Task> callback)
        {
            _callback = callback;
        }

        public Task SetVisible(bool visible, bool instantly, CancellationToken cancellationToken)
        {
            return _callback.Invoke(visible, instantly, cancellationToken);
        }
    }
}