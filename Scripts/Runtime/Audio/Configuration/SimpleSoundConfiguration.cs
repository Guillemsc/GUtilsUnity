using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace GUtilsUnity.Audio.Configuration
{
    /// <summary>
    /// Represents the necessary configuration for playing a sound, with some volume and some pitch.
    /// </summary>
    [Serializable]
    public sealed class SimpleSoundConfiguration
    {
        [FormerlySerializedAs("_audioClip")] public AudioClip AudioClip;
        [FormerlySerializedAs("_volume")] [Range(0, 1)] public float Volume = 0.5f;
        [FormerlySerializedAs("_pitch")] [Range(0, 3)] public float Pitch = 1.0f;
    }
}
