using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace GUtilsUnity.Extensions
{
    public static class AnimationExtensions
    {
        /// <summary>
        /// Calls Play on an <see cref="Animation"/>. Then awaits until is not playing anymore.
        /// If cancellation is requested, animation is stopped.
        /// </summary>
        public static async Task<bool> PlayAsync(this Animation animation, CancellationToken cancellationToken)
        {
            bool couldPlay = animation.Play();

            if (!couldPlay)
            {
                return false;
            }

            await TaskExtensions.AwaitUntil(() => !animation.isPlaying, cancellationToken);

            if (cancellationToken.IsCancellationRequested)
            {
                animation.Stop();
            }

            return true;
        }
    }
}
