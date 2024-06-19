using DG.Tweening;
using GUtilsUnity.Tweening.Configuration;
using UnityEngine;
using TweenExtensions = GUtilsUnity.Extensions.TweenExtensions;

namespace GUtilsUnity.Tweening.Extensions
{
    public static class CanvasGroupTweenExtensions
    {
        public static Tween DOFade(this CanvasGroup canvasGroup, NormalizedFloatTweenConfiguration configuration)
        {
            return TweenExtensions.ExtraSequenceFix(canvasGroup
                .DOFade(configuration.Value, configuration.Duration)
                .SetEase(configuration.Easing)
                .SetDelay(configuration.Delay));
        }
    }
}
