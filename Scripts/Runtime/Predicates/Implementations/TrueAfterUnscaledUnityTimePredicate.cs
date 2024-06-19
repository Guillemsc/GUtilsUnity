using System;
using GUtilsUnity.Time.Timers;

namespace GUtilsUnity.Predicates
{
    /// <summary>
    /// A <see cref="IPredicate"/> that is satisifed when some time has passed since the application start.
    /// </summary>
    /// <inheritdoc />
    public sealed class TrueAfterUnscaledUnityTimePredicate : IPredicate
    {
        readonly ITimer _timer;
        readonly TimeSpan _timeSpan;

        public TrueAfterUnscaledUnityTimePredicate(
            TimeSpan timeSpan
        )
        {
            _timeSpan = timeSpan;
            _timer = UnscaledUnityTimer.FromStarted();
        }

        public bool IsSatisfied()
        {
            return _timer.HasReached(_timeSpan);
        }
    }
}
