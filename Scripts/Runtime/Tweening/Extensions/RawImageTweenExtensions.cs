using DG.Tweening;
using GUtilsUnity.Extensions;
using UnityEngine.UI;

namespace GUtilsUnity.Tweening.Extensions
{
    public static class RawImageTweenExtensions
    {
        public static Tween DOUvRectX(this RawImage rawImage, float value, float duration)
        {
            return DOTween.To(
                () => rawImage.uvRect.x,
                rawImage.SetUvRectX,
                value,
                duration
            ).SetTarget(rawImage);
        }

        public static Tween DOUvRectY(this RawImage rawImage, float value, float duration)
        {
            return DOTween.To(
                () => rawImage.uvRect.y,
                rawImage.SetUvRectY,
                value,
                duration
            ).SetTarget(rawImage);
        }
    }
}
