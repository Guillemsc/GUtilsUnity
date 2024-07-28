using System;
using System.Threading;
using System.Threading.Tasks;
using GUtils.Time.Extensions;
using GUtils.Time.Timers;
using GUtilsUnity.Time.TimeContexts;

namespace GUtilsUnity.Time.Timers
{
    /// <inheritdoc />
    /// <summary>
    /// Timer that uses the UnityEngine.Time.time as time source.
    /// </summary>
    public sealed class ScaledUnityTimer : TimeSourceTimer
    {
        public ScaledUnityTimer() : base(ScaledUnityTimeContext.Instance)
        {
        }

        public static ITimer FromStarted()
        {
            ITimer timer = new ScaledUnityTimer();
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
