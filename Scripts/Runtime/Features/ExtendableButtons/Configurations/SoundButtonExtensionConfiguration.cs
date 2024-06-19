using GUtilsUnity.Audio.Configuration;
using UnityEngine;

namespace GUtilsUnity.Features.ExtendableButtons.Configurations
{
    [CreateAssetMenu(fileName = "SoundButtonExtensionConfiguration",
        menuName = "PopcoreCore/Features/ExtendableButtons/SoundButtonExtensionConfiguration")]
    public sealed class SoundButtonExtensionConfiguration : ScriptableObject
    {
        public SimpleSoundConfiguration Sound;
    }
}
