using System;
using GUtils.Tick.Tickables;
using GUtils.Time.TimeContexts;

namespace GUtilsUnity.Time.TimeContexts
{
    /// <inheritdoc cref="ITimeContext" />
    /// <summary>
    /// Based on the UnityEngine.Time scaled time source, but provides an extra, independent TimeScale.
    /// </summary>
    public sealed class ScaledUnityBasedTimeContext : ITimeContext, ITickable
    {
        public TimeSpan Time { get; private set; }
        public float TimeScale { get; set; }
        public float DeltaTime { get; private set; }
        
        public event Action OnTimeScaleChanged;

        public void Tick()
        {
            DeltaTime = UnityEngine.Time.deltaTime * TimeScale;
            Time += TimeSpan.FromSeconds(DeltaTime);
        }
    }
}
