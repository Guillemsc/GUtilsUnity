using System;
using GUtils.Tick.Tickables;
using GUtils.Time.TimeContexts;

namespace GUtilsUnity.Time.TimeContexts
{
    /// <inheritdoc cref="ITimeContext" />
    /// <summary>
    /// Based on the UnityEngine.Time unscaled time source, but provides an extra, independent TimeScale.
    /// </summary>
    public sealed class UnscaledUnityBasedTimeContext : ITimeContext, ITickable
    {
        public TimeSpan Time { get; private set; }
        public float TimeScale { get; set; }
        public float DeltaTime { get; private set; }
        public event Action OnTimeScaleChanged;

        public void Tick()
        {
            DeltaTime = UnityEngine.Time.unscaledDeltaTime * TimeScale;
            Time += TimeSpan.FromSeconds(DeltaTime);
        }
    }
}
