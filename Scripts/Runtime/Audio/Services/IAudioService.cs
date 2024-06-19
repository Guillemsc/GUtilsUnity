using System.Collections.Generic;
using GUtilsUnity.Audio.Configuration;
using UnityEngine;

namespace GUtilsUnity.Audio.Services
{
    /// <summary>
    /// Service that provides functionality for playing general audio.
    /// </summary>
    public interface IAudioService
    {
        /// <summary>
        /// Plays an <see cref="AudioClip"/> with some volume and pitch.
        /// </summary>
        /// <param name="audioClip">Clip to be played.</param>
        /// <param name="volume">Volume set for playing the clip</param>
        /// <param name="pitch">Pitch set for playing the clip</param>
        void Play(AudioClip audioClip, float volume, float pitch);

        /// <summary>
        /// Plays an <see cref="SimpleSoundConfiguration"/> with its volume and pitch values.
        /// </summary>
        /// <param name="simpleSoundConfiguration">Configuration to be played.</param>
        void Play(SimpleSoundConfiguration simpleSoundConfiguration);

        /// <summary>
        /// From a list of <see cref="SimpleSoundConfiguration"/>, picks one randomly and plays it
        /// with its volume and pitch values.
        /// </summary>
        /// <param name="simpleSoundConfigurations">List of configurations that could be played.</param>
        void PlayRandom(IReadOnlyList<SimpleSoundConfiguration> simpleSoundConfigurations);

        /// <summary>
        /// Plays an <see cref="PitchRangeSoundConfiguration"/> with its volume and a random pitch selected from its range.
        /// </summary>
        /// <param name="pitchRangeSoundConfiguration">Configuration to be played.</param>
        void PlayWithRandomPitch(PitchRangeSoundConfiguration pitchRangeSoundConfiguration);
    }
}
