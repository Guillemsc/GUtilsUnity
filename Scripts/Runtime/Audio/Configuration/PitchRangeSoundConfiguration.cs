using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace GUtilsUnity.Audio.Configuration
{
    /// <summary>
    /// Represents the necessary configuration for playing a sound, with some volume,
    /// and with a minimum and maximum pitch values.
    /// </summary>
    [Serializable]
    public sealed class PitchRangeSoundConfiguration
    {
        [FormerlySerializedAs("_audioClip")] public AudioClip AudioClip;
        [FormerlySerializedAs("_volume")] [Range(0, 1)] public float Volume = 0.5f;
        [FormerlySerializedAs("_minPitch")] [Range(0, 3)] public float MinPitch = 1.0f;
        [FormerlySerializedAs("_maxPitch")] [Range(0, 3)] public float MaxPitch = 1.5f;
    }
}
