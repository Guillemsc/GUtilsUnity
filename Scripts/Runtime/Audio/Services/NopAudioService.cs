using System.Collections.Generic;
using GUtilsUnity.Audio.Configuration;
using UnityEngine;

namespace GUtilsUnity.Audio.Services
{
    public sealed class NopAudioService : IAudioService
    {
        public static readonly NopAudioService Instance = new();

        NopAudioService()
        {

        }

        public void Play(AudioClip audioClip, float volume, float pitch) { }
        public void Play(SimpleSoundConfiguration simpleSoundConfiguration) { }
        public void PlayRandom(IReadOnlyList<SimpleSoundConfiguration> simpleSoundConfigurations) { }
        public void PlayWithRandomPitch(PitchRangeSoundConfiguration pitchRangeSoundConfiguration) { }
    }
}
