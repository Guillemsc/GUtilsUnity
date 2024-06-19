using DG.Tweening;
using GUtilsUnity.Extensions;
using GUtilsUnity.Tweening.Configuration;
using UnityEngine.UI;
using TweenExtensions = GUtilsUnity.Extensions.TweenExtensions;

namespace GUtilsUnity.Tweening.Extensions
{
    public static class ScrollRectTweenExtensions
    {
        public static Tween DOHorizontalNormalizedPos(this ScrollRect scrollRect, NormalizedFloatTweenConfiguration configuration)
        {
            return TweenExtensions.ExtraSequenceFix(scrollRect
                .DOHorizontalNormalizedPos(configuration.Value, configuration.Duration)
                .SetEase(configuration.Easing)
                .SetDelay(configuration.Delay));
        }

        public static Tween DOHorizontalNormalizedPos(this ScrollRect scrollRect, float value, NoValueTweenConfiguration configuration)
        {
            return TweenExtensions.ExtraSequenceFix(scrollRect
                .DOHorizontalNormalizedPos(value, configuration.Duration)
                .SetEase(configuration.Easing)
                .SetDelay(configuration.Delay));
        }

        public static Tween DOVerticalNormalizedPos(this ScrollRect scrollRect, NormalizedFloatTweenConfiguration configuration)
        {
            return TweenExtensions.ExtraSequenceFix(scrollRect
                .DOVerticalNormalizedPos(configuration.Value, configuration.Duration)
                .SetEase(configuration.Easing)
                .SetDelay(configuration.Delay));
        }

        public static Tween DOVerticalNormalizedPos(this ScrollRect scrollRect, float value, NoValueTweenConfiguration configuration)
        {
            return TweenExtensions.ExtraSequenceFix(scrollRect
                .DOVerticalNormalizedPos(value, configuration.Duration)
                .SetEase(configuration.Easing)
                .SetDelay(configuration.Delay));
        }
    }
}
