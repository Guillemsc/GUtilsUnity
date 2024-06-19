using DG.Tweening;
using GUtilsUnity.Extensions;
using GUtilsUnity.Tweening.Configuration;
using UnityEngine;
using TweenExtensions = GUtilsUnity.Extensions.TweenExtensions;

namespace GUtilsUnity.Tweening.Extensions
{
    public static class MaterialTweenExtensions
    {
        public static Tween DOColor(this Material material, MaterialColorTweenConfiguration configuration)
        {
            return TweenExtensions.ExtraSequenceFix(material
                .DOColor(configuration.Value, configuration.Property, configuration.Duration)
                .SetEase(configuration.Easing)
                .SetDelay(configuration.Delay));
        }

        public static Tween DOFade(this Material material, MaterialNormalizedFloatTweenConfiguration configuration)
        {
            return TweenExtensions.ExtraSequenceFix(material
                .DOFade(configuration.Value, configuration.Property, configuration.Duration)
                .SetEase(configuration.Easing)
                .SetDelay(configuration.Delay));
        }

        public static Tween DOFade(this Material material, MaterialNormalizedFloatStartEndTweenConfiguration configuration)
        {
            return DOTween.Sequence()
                .Append(material.DOFade(configuration.StartMaterialNormalizedFloatTweenConfiguration))
                .AppendInterval(configuration.SustainTime)
                .Append(material.DOFade(configuration.EndMaterialNormalizedFloatTweenConfiguration));
        }

        public static Tween DOFloat(this Material material, MaterialNormalizedFloatTweenConfiguration configuration)
        {
            return TweenExtensions.ExtraSequenceFix(material
                .DOFloat(configuration.Value, configuration.Property, configuration.Duration)
                .SetEase(configuration.Easing)
                .SetDelay(configuration.Delay));
        }

        public static Tween DOFloat(this Material material, MaterialNormalizedFloatStartEndTweenConfiguration configuration)
        {
            return DOTween.Sequence()
                .Append(material.DOFloat(configuration.StartMaterialNormalizedFloatTweenConfiguration))
                .AppendInterval(configuration.SustainTime)
                .Append(material.DOFloat(configuration.EndMaterialNormalizedFloatTweenConfiguration));
        }

        public static Tween DOGradientColor(this Material material, MaterialGradientTweenConfiguration configuration)
        {
            return TweenExtensions.ExtraSequenceFix(material
                .DOGradientColor(configuration.Value, configuration.Property, configuration.Duration)
                .SetEase(configuration.Easing)
                .SetDelay(configuration.Delay));
        }
    }
}
