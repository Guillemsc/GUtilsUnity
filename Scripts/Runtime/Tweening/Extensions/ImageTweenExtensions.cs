using DG.Tweening;
using GUtilsUnity.Extensions;
using GUtilsUnity.Tweening.Configuration;
using UnityEngine;
using UnityEngine.UI;
using TweenExtensions = GUtilsUnity.Extensions.TweenExtensions;

namespace GUtilsUnity.Tweening.Extensions
{
    public static class ImageTweenExtensions
    {
        public static Tween DOColor(this Image image, ColorTweenConfiguration configuration)
        {
            return TweenExtensions.ExtraSequenceFix(image
                .DOColor(configuration.Value, configuration.Duration)
                .SetEase(configuration.Easing)
                .SetDelay(configuration.Delay));
        }

        public static Tween DOColor(this Image image, Color value, NoValueTweenConfiguration configuration)
        {
            return TweenExtensions.ExtraSequenceFix(image
                .DOColor(value, configuration.Duration)
                .SetEase(configuration.Easing)
                .SetDelay(configuration.Delay));
        }

        public static Tween DOFade(this Image image, NormalizedFloatTweenConfiguration configuration)
        {
            return TweenExtensions.ExtraSequenceFix(image
                .DOFade(configuration.Value, configuration.Duration)
                .SetEase(configuration.Easing)
                .SetDelay(configuration.Delay));
        }

        public static Tween DOFade(this Image image, float value, NoValueTweenConfiguration configuration)
        {
            return TweenExtensions.ExtraSequenceFix(image
                .DOFade(value, configuration.Duration)
                .SetEase(configuration.Easing)
                .SetDelay(configuration.Delay));
        }

        public static Tween DOGradientColor(this Image image, GradientTweenConfiguration configuration)
        {
            return TweenExtensions.ExtraSequenceFix(image
                .DOGradientColor(configuration.Value, configuration.Duration)
                .SetEase(configuration.Easing)
                .SetDelay(configuration.Delay));
        }

        public static Tween DOGradientColor(this Image image, Gradient value, NoValueTweenConfiguration configuration)
        {
            return TweenExtensions.ExtraSequenceFix(image
                .DOGradientColor(value, configuration.Duration)
                .SetEase(configuration.Easing)
                .SetDelay(configuration.Delay));
        }

        public static Tween DOFillAmount(this Image image, NormalizedFloatTweenConfiguration configuration)
        {
            return TweenExtensions.ExtraSequenceFix(image
                .DOFillAmount(configuration.Value, configuration.Duration)
                .SetEase(configuration.Easing)
                .SetDelay(configuration.Delay));
        }

        public static Tween DOFillAmount(this Image image, float value, NoValueTweenConfiguration configuration)
        {
            return TweenExtensions.ExtraSequenceFix(image
                .DOFillAmount(value, configuration.Duration)
                .SetEase(configuration.Easing)
                .SetDelay(configuration.Delay));
        }
    }
}
