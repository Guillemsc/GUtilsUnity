using System.Threading;
using System.Threading.Tasks;

namespace GUtilsUnity.Extensions
{
    public static class CancellationTokenExtensions
    {
        /// <summary>
        /// Awaits until cancellation get's requested.
        /// </summary>
        public static Task AwaitCancellationRequested(this CancellationToken cancellationToken)
        {
            return GUtils.Extensions.TaskExtensions.AwaitUntil(() => cancellationToken.IsCancellationRequested);
        }
    }
}
