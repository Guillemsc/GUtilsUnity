using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace GUtilsUnity.Extensions
{
    public static class ParticleSystemExtensions
    {
        /// <summary>
        /// Plays the particle system, and awaits until is not playing anymore.
        /// </summary>
        public static Task PlayAsync(this ParticleSystem particleSystem, CancellationToken cancellationToken)
        {
            return PlayAsync(particleSystem, includeChildren: true, cancellationToken);
        }

        /// <summary>
        /// Plays the particle system, and awaits until is not playing anymore.
        /// </summary>
        public static async Task PlayAsync(this ParticleSystem particleSystem, bool includeChildren, CancellationToken cancellationToken)
        {
            particleSystem.Play(includeChildren);

            await particleSystem.AwaitUntilNotPlaying(cancellationToken);

            if (cancellationToken.IsCancellationRequested)
            {
                particleSystem.Stop(includeChildren);
            }
        }

        /// <summary>
        /// Awaits until particle system is not playing.
        /// </summary>
        public static Task AwaitUntilNotPlaying(this ParticleSystem particleSystem, CancellationToken cancellationToken)
        {
            return TaskExtensions.AwaitUntil(() => !particleSystem.isPlaying, cancellationToken);
        }
    }
}
