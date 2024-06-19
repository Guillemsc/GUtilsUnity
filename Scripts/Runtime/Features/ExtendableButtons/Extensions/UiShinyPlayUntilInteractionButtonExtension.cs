using Coffee.UIEffects;
using GUtilsUnity.Attributes.FindFirstAsset;
using GUtilsUnity.Features.ExtendableButtons.ButtonExtensions;
using GUtilsUnity.Features.ExtendableButtons.Configuration;

namespace Popcore.Utils.Features.ExtendableButtons.ButtonExtensions
{
    public sealed class UiShinyPlayUntilInteractionButtonExtension : ButtonExtension
    {
        [FindFirstDefaultAsset] public UiShinyPlayUntilInteractionButtonExtensionConfiguration Configuration;
        public UIShiny UiShiny;

        public override void WhenEnable()
        {
            if (UiShiny == null)
            {
                return;
            }

            if (Configuration == null)
            {
                return;
            }

            UiShiny.effectFactor = 0f;
            UiShiny.width = Configuration.Width;
            UiShiny.rotation = Configuration.Rotation;
            UiShiny.softness = Configuration.Softness;
            UiShiny.brightness = Configuration.Brightness;
            UiShiny.gloss = Configuration.Gloss;
            UiShiny.effectPlayer.initialPlayDelay = Configuration.InitialPlayDelay;
            UiShiny.effectPlayer.loopDelay = Configuration.LoopDelay;

            UiShiny.effectPlayer.loop = true;
            UiShiny.Play();
        }

        public override void WhenStart()
        {
            if (UiShiny == null)
            {
                return;
            }

            UiShiny.effectFactor = UnityEngine.Time.time % 1f;
        }

        public override void WhenDown()
        {
            if (UiShiny == null)
            {
                return;
            }

            UiShiny.Stop();
            UiShiny.effectFactor = 0f;
        }
    }
}
