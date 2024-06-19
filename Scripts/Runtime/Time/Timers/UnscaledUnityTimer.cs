using System;
using System.Threading;
using System.Threading.Tasks;
using GUtilsUnity.Time.TimeContexts;

namespace GUtilsUnity.Time.Timers
{
    /// <inheritdoc />
    /// <summary>
    /// Timer that uses the UnityEngine.Time.unscaledTime as time source.
    /// </summary>
    public sealed class UnscaledUnityTimer : TimeSourceTimer
    {
        public UnscaledUnityTimer() : base(UnscaledUnityTimeContext.Instance)
        {
        }

        public static ITimer FromStarted()
        {
            ITimer timer = new UnscaledUnityTimer();
            timer.Start();
            return timer;
        }

        public static Task Await(TimeSpan timeSpan, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return Task.CompletedTask;
            }

            return FromStarted().AwaitReach(timeSpan, cancellationToken);
        }
    }
}
