using System;
using System.Threading;
using System.Threading.Tasks;
using DG.Tweening;
using GUtilsUnity.Time.Timers;
using GUtilsUnity.TweenPlayers.Looping;

namespace GUtilsUnity.Extensions
{
    public static class TweenExtensions
    {
        public static async Task AsyncWaitForCompletion(this Tween tween)
        {
            if (!tween.active)
            {
                return;
            }

            while (tween.active && !tween.IsComplete())
            {
                await Task.Yield();
            }
        }

        public static async Task AsyncWaitForKill(this Tween t)
        {
            if (!t.active)
            {
                return;
            }

            while (t.active)
            {
                await Task.Yield();
            }
        }

        /// <summary>
        /// Plays the tween.
        /// </summary>
        /// <param name="instantly">If instantly is set to true, the tween will be completed instantly</param>
        public static void Play(this Tween tween, bool instantly)
        {
            tween.Play();

            if (instantly)
            {
                tween.Complete(withCallbacks: true);
            }
        }

        /// <summary>
        /// Plays the tween and awaits until it's completed or killed.
        /// </summary>
        /// <param name="instantly">If instantly is set to true, the tween will be completed instantly</param>
        /// <param name="cancellationToken">If cancellation is requested, the tween will be killed</param>
        public static Task PlayAsync(this Tween tween, bool instantly, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested) return Task.CompletedTask;

            tween.Play(instantly);

            if (!tween.IsActive() || !tween.IsPlaying())
            {
                return Task.CompletedTask;
            }

            cancellationToken.Register(() => tween.Kill());

            return tween.AwaitCompletitionOrKill(cancellationToken);
        }


        /// <summary>
        /// Plays the tween and awaits until it's completed or killed.
        /// </summary>
        /// <param name="cancellationToken">If cancellation is requested, tween will be killed</param>
        public static Task PlayAsync(this Tween tween, CancellationToken cancellationToken)
        {
            return tween.PlayAsync(instantly: false, cancellationToken);
        }

        /// <summary>
        /// Awaits until the tween is completed or killed.
        /// </summary>
        /// <param name="cancellationToken">If cancellation is requested, we stop waiting and exit immediately</param>
        public static Task AwaitCompletitionOrKill(this Tween tween, CancellationToken cancellationToken)
        {
            return Task.WhenAny(
                cancellationToken.AwaitCancellationRequested(),
                tween.AsyncWaitForCompletion(),
                tween.AsyncWaitForKill()
            );
        }

        /// <summary>
        /// Set the tween to loop infinitely. Has no effect if it is already started
        /// </summary>
        public static T SetLoopInfinitely<T>(this T tween) where T : Tween
        {
            return tween.SetLoops(-1);
        }

        /// <summary>
        /// Set the tween to loop infinitely. Has no effect if it is already started
        /// </summary>
        /// <param name="tween">The Tween instance that this method extends.</param>
        /// <param name="loopType">Loop behaviour type</param>
        public static T SetLoopInfinitely<T>(this T tween, LoopType loopType) where T : Tween
        {
            return tween.SetLoops(-1, loopType);
        }

        /// <summary>
        /// Sets int.MaxValue loops to the tween.
        /// </summary>
        public static T SetMaxLoops<T>(this T tween) where T : Tween
        {
            return tween.SetLoops(int.MaxValue);
        }

        /// <summary>
        /// Sets int.MaxValue loops to the tween.
        /// </summary>
        /// <param name="tween">The Tween instance that this method extends.</param>
        /// <param name="loopType">Loop behaviour type</param>
        public static T SetMaxLoops<T>(this T tween, LoopType loopType) where T : Tween
        {
            return tween.SetLoops(int.MaxValue, loopType);
        }

        /// <summary>
        /// Sets the full position of the tween to the current timer's time.
        /// </summary>
        /// <param name="timer">Timer used to get the current time</param>
        public static T SyncPosition<T>(this T tween, ITimer timer) where T : Tween
        {
            tween.fullPosition = (float)timer.Time.TotalSeconds;

            return tween;
        }

        /// <summary>
        /// Configures the loop behaviour of a Tween object based on a provided ILoopTweenConfiguration.
        /// </summary>
        /// <typeparam name="T">The type of the Tween.</typeparam>
        /// <param name="tween">The Tween instance that this method extends.</param>
        /// <param name="loopTweenConfiguration">The configuration for the loop behavior.</param>
        /// <returns>The original Tween instance with updated loop configuration.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when an unknown loop configuration is provided.</exception>
        public static T SetLoop<T>(this T tween, ILoopTweenConfiguration loopTweenConfiguration)
            where T : Tween
        {
            return loopTweenConfiguration switch
            {
                CountLoopTweenConfiguration countLoopTweenConfiguration => tween.SetLoops(countLoopTweenConfiguration.Count),
                InfinitelyLoopTweenConfiguration => tween.SetMaxLoops(),
                NoLoopTweenConfiguration => tween.SetLoops(1),
                _ => throw new ArgumentOutOfRangeException(nameof(loopTweenConfiguration), loopTweenConfiguration, null)
            };
        }

        /// <summary>
        /// Some time ago we added an extra sequence but we don't remember the exact reason why
        /// This flag disables this behaviour so we can test if we actually need to add it.
        /// If we can remove it it will be faster
        /// </summary>
        public static Tween ExtraSequenceFix(Tween tween)
        {
#if POPCORE_CORE_FL_139_AVOID_EXTRA_SEQUENCES
            return tween;
#else
            return DOTween.Sequence().Join(tween);
#endif
        }
    }
}
