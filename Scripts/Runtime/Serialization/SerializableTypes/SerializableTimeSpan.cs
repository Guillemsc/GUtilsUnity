using System;
using UnityEngine;

namespace GUtilsUnity.Serialization.SerializableTypes
{
    [Serializable]
    public class SerializableTimeSpan
    {
        [Range(0, 9999)] public int Days = TimeSpan.MinValue.Days;
        [Range(0, 23)] public int Hours = TimeSpan.MinValue.Hours;
        [Range(0, 59)] public int Minutes = TimeSpan.MinValue.Minutes;
        [Range(0, 59)] public int Seconds = TimeSpan.MinValue.Seconds;
        [Range(0, 59)] public int Milliseconds = TimeSpan.MinValue.Milliseconds;

        public TimeSpan ToTimeSpan() => new(
            Days,
            Hours,
            Minutes,
            Seconds,
            Milliseconds);

        public static implicit operator TimeSpan(
            SerializableTimeSpan serializable)
            => serializable.ToTimeSpan();
    }
}
