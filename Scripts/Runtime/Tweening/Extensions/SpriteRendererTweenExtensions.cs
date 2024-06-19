using DG.Tweening;
using GUtilsUnity.Extensions;
using GUtilsUnity.Tweening.Configuration;
using UnityEngine;
using TweenExtensions = GUtilsUnity.Extensions.TweenExtensions;

namespace GUtilsUnity.Tweening.Extensions
{
    public static class SpriteRendererTweenExtensions
    {
        public static Tween DOColor(this SpriteRenderer spriteRenderer, ColorTweenConfiguration configuration)
        {
            return TweenExtensions.ExtraSequenceFix(spriteRenderer
                .DOColor(configuration.Value, configuration.Duration)
                .SetEase(configuration.Easing)
                .SetDelay(configuration.Delay));
        }

        public static Tween DOFade(this SpriteRenderer spriteRenderer, NormalizedFloatTweenConfiguration configuration)
        {
            return TweenExtensions.ExtraSequenceFix(spriteRenderer
                .DOFade(configuration.Value, configuration.Duration)
                .SetEase(configuration.Easing)
                .SetDelay(configuration.Delay));
        }

        public static Tween DOFade(this SpriteRenderer spriteRenderer, float value, NoValueTweenConfiguration configuration)
        {
            return TweenExtensions.ExtraSequenceFix(spriteRenderer
                .DOFade(value, configuration.Duration)
                .SetEase(configuration.Easing)
                .SetDelay(configuration.Delay));
        }

        public static Tween DOGradientColor(this SpriteRenderer spriteRenderer, GradientTweenConfiguration configuration)
        {
            return TweenExtensions.ExtraSequenceFix(spriteRenderer
                .DOGradientColor(configuration.Value, configuration.Duration)
                .SetEase(configuration.Easing)
                .SetDelay(configuration.Delay));
        }
    }
}
