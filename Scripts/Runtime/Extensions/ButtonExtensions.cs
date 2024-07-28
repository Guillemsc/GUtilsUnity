using System.Threading;
using System.Threading.Tasks;
using GUtils.Extensions;
using GUtils.Tasks.CompletionSources;
using UnityEngine.UI;

namespace GUtilsUnity.Extensions
{
    public static class ButtonExtensions
    {
        /// <summary>
        /// Awaits until the button is clicked once, and then continues.
        /// </summary>
        public static async Task AwaitClick(this Button button, CancellationToken cancellationToken)
        {
            TaskCompletionSource taskCompletionSource = new();
            taskCompletionSource.LinkCancellationTokenAsComplete(cancellationToken);

            void Complete() => taskCompletionSource.TrySetResult();

            button.onClick.AddListener(Complete);

            await taskCompletionSource.Task;

            button.onClick.RemoveListener(Complete);
        }
    }
}
