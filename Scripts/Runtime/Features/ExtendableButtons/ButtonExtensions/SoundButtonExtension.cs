using GUtilsUnity.Attributes.FindFirstAsset;
using GUtilsUnity.Audio.Services;
using GUtilsUnity.Features.ExtendableButtons.Configurations;
using GUtilsUnity.Features.ExtendableButtons.Enums;
using GUtilsUnity.Services.Locators;

namespace GUtilsUnity.Features.ExtendableButtons.ButtonExtensions
{
    public sealed class SoundButtonExtension : ButtonExtension
    {
        [FindFirstDefaultAsset] public SoundButtonExtensionConfiguration Configuration;
        public ButtonSoundLocation Location = ButtonSoundLocation.Down;

        public override void WhenDown()
        {
            TryPlaySound(ButtonSoundLocation.Down);
        }

        public override void WhenUp()
        {
            TryPlaySound(ButtonSoundLocation.Up);
        }

        public override void WhenClick()
        {
            TryPlaySound(ButtonSoundLocation.Click);
        }

        void TryPlaySound(ButtonSoundLocation location)
        {
            if (Location != location)
            {
                return;
            }

            if (Configuration == null)
            {
                return;
            }

            bool audioServiceFound = ServiceLocator.TryGet(out IAudioService audioService);

            if (!audioServiceFound)
            {
                return;
            }

            audioService.Play(Configuration.Sound);
        }
    }
}
