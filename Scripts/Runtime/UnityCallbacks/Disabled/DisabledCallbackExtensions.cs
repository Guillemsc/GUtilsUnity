using System.Threading;
using System.Threading.Tasks;
using GUtils.Extensions;
using GUtilsUnity.Extensions;
using UnityEngine;

namespace GUtilsUnity.UnityCallbacks.Disabled
{
    public static class DisabledCallbackExtensions
    {
        public static Task WaitDisabled(this GameObject gameObject, CancellationToken ct)
        {
            var disabledCallback = gameObject.GetOrAddComponent<DisabledCallback>();
            var taskCompletionSource = new TaskCompletionSource<object>();
            taskCompletionSource.LinkCancellationToken(ct);

            void OnDisabled()
            {
                disabledCallback.OnDisabled -= OnDisabled;
                taskCompletionSource.SetResult(default);
            }

            disabledCallback.OnDisabled += OnDisabled;
            return taskCompletionSource.Task;
        }
    }
}
