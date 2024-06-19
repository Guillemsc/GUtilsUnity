using System;
using GUtilsUnity.Time.Timers;

namespace GUtilsUnity.Time.Extensions
{
    public static class ITimerExtensions
    {
        public static bool HasReachedOrNotStarted(this ITimer timer, TimeSpan timeSpan)
        {
            return !timer.Started || timer.HasReached(timeSpan);
        }
    }
}
