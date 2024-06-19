using Coffee.UIEffects;
using DG.Tweening;
using GUtilsUnity.Tweening.Configuration;

namespace GUtilsUnity.Tweening.Extensions
{
    public static class UiShinyTweenExtensions
    {
        public static Tween DoShine(this UIShiny shiny, NoValueTweenConfiguration configuration)
        {
            void OnVirtualUpdate(float x) =>
                shiny.effectFactor = x;

            return DOVirtual.Float(
                    0f,
                    1f,
                    configuration.Duration,
                    OnVirtualUpdate
                )
                .SetEase(configuration.Easing)
                .SetDelay(configuration.Delay);
        }
    }
}
