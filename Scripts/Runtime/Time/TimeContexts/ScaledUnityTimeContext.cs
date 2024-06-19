using System;

namespace GUtilsUnity.Time.TimeContexts
{
    /// <inheritdoc />
    /// <summary>
    /// Based on the UnityEngine.Time scaled time source.
    /// </summary>
    public sealed class ScaledUnityTimeContext : ITimeContext
    {
        public static readonly ScaledUnityTimeContext Instance = new();

        ScaledUnityTimeContext()
        {

        }

        public TimeSpan Time => TimeSpan.FromSeconds(UnityEngine.Time.time);
        public float TimeScale
        {
            get => UnityEngine.Time.timeScale;
            set => UnityEngine.Time.timeScale = value;
        }
        public float DeltaTime => UnityEngine.Time.deltaTime;
    }
}
