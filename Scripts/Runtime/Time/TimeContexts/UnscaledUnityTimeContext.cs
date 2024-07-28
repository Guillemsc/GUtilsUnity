using System;
using GUtils.Time.TimeContexts;

namespace GUtilsUnity.Time.TimeContexts
{
    /// <inheritdoc />
    /// <summary>
    /// Based on the UnityEngine.Time unscaled time source.
    /// </summary>
    public sealed class UnscaledUnityTimeContext : ITimeContext
    {
        public static readonly UnscaledUnityTimeContext Instance = new();

        UnscaledUnityTimeContext()
        {

        }

        public TimeSpan Time => TimeSpan.FromSeconds(UnityEngine.Time.unscaledTime);
        public float TimeScale
        {
            get => 1f;
            set {}
        }
        public float DeltaTime => UnityEngine.Time.unscaledDeltaTime;
        public event Action OnTimeScaleChanged;
    }
}
