using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Core.PathCore;
using DG.Tweening.Plugins.Options;
using GUtilsUnity.Extensions;
using GUtilsUnity.Tweening.Configuration;
using UnityEngine;
using TweenExtensions = GUtilsUnity.Extensions.TweenExtensions;

namespace GUtilsUnity.Tweening.Extensions
{
    public static class RectTransformTweenExtensions
    {
        public static Tween DOAnchorPos(this RectTransform transform, Vector2TweenConfiguration configuration)
        {
            return TweenExtensions.ExtraSequenceFix(transform
                .DOAnchorPos(configuration.Value, configuration.Duration)
                .SetEase(configuration.Easing)
                .SetDelay(configuration.Delay));
        }

        public static Tween DOAnchorPos(this RectTransform transform, Vector2 value, NoValueTweenConfiguration configuration)
        {
            return TweenExtensions.ExtraSequenceFix(transform
                .DOAnchorPos(value, configuration.Duration)
                .SetEase(configuration.Easing)
                .SetDelay(configuration.Delay));
        }

        public static Tween DOAnchorPosX(this RectTransform transform, FloatTweenConfiguration configuration)
        {
            return TweenExtensions.ExtraSequenceFix(transform
                .DOAnchorPosX(configuration.Value, configuration.Duration)
                .SetEase(configuration.Easing)
                .SetDelay(configuration.Delay));
        }

        public static Tween DOAnchorPosX(this RectTransform transform, float value, NoValueTweenConfiguration configuration)
        {
            return TweenExtensions.ExtraSequenceFix(transform
                .DOAnchorPosX(value, configuration.Duration)
                .SetEase(configuration.Easing)
                .SetDelay(configuration.Delay));
        }

        public static Tween DOAnchorPosY(this RectTransform transform, FloatTweenConfiguration configuration)
        {
            return TweenExtensions.ExtraSequenceFix(transform
                .DOAnchorPosY(configuration.Value, configuration.Duration)
                .SetEase(configuration.Easing)
                .SetDelay(configuration.Delay));
        }

        public static Tween DOAnchorPosY(this RectTransform transform, float value, NoValueTweenConfiguration configuration)
        {
            return TweenExtensions.ExtraSequenceFix(transform
                .DOAnchorPosY(value, configuration.Duration)
                .SetEase(configuration.Easing)
                .SetDelay(configuration.Delay));
        }

        public static Tween DOSizeDeltaX(this RectTransform target, float value, float duration)
        {
            return DOTween.To(
                () => target.sizeDelta.x,
                target.SetSizeDeltaX,
                value,
                duration
            );
        }

        public static Tween DOSizeDeltaX(this RectTransform transform, FloatTweenConfiguration configuration)
        {
            return TweenExtensions.ExtraSequenceFix(transform
                .DOSizeDeltaX(configuration.Value, configuration.Duration)
                .SetEase(configuration.Easing)
                .SetDelay(configuration.Delay));
        }

        public static Tween DOSizeDeltaX(this RectTransform transform, float value, NoValueTweenConfiguration configuration)
        {
            return TweenExtensions.ExtraSequenceFix(transform
                .DOSizeDeltaX(value, configuration.Duration)
                .SetEase(configuration.Easing)
                .SetDelay(configuration.Delay));
        }

        public static Tween DOSizeDeltaY(this RectTransform target, float value, float duration)
        {
            return DOTween.To(
                () => target.sizeDelta.y,
                target.SetSizeDeltaY,
                value,
                duration
            );
        }

        public static Tween DOSizeDeltaY(this RectTransform transform, FloatTweenConfiguration configuration)
        {
            return TweenExtensions.ExtraSequenceFix(transform
                .DOSizeDeltaY(configuration.Value, configuration.Duration)
                .SetEase(configuration.Easing)
                .SetDelay(configuration.Delay));
        }

        public static Tween DOSizeDeltaY(this RectTransform transform, float value, NoValueTweenConfiguration configuration)
        {
            return TweenExtensions.ExtraSequenceFix(transform
                .DOSizeDeltaY(value, configuration.Duration)
                .SetEase(configuration.Easing)
                .SetDelay(configuration.Delay));
        }

        public static Tween DOPivot(this RectTransform transform, Vector2TweenConfiguration configuration)
        {
            return TweenExtensions.ExtraSequenceFix(transform
                .DOPivot(configuration.Value, configuration.Duration)
                .SetEase(configuration.Easing)
                .SetDelay(configuration.Delay));
        }

        public static Tween DOPivot(this RectTransform transform, Vector2StartEndTweenConfiguration configuration)
        {
            return DOTween.Sequence()
                .Join(transform.DOPivot(configuration.StartVector2TweenConfiguration))
                .AppendInterval(configuration.SustainTime)
                .Append(transform.DOPivot(configuration.EndVector2TweenConfiguration));
        }

        public static Tween DOPivotX(this RectTransform transform, FloatTweenConfiguration configuration)
        {
            return TweenExtensions.ExtraSequenceFix(transform
                .DOPivotX(configuration.Value, configuration.Duration)
                .SetEase(configuration.Easing)
                .SetDelay(configuration.Delay));
        }

        public static Tween DOPivotY(this RectTransform transform, FloatTweenConfiguration configuration)
        {
            return TweenExtensions.ExtraSequenceFix(transform
                .DOPivotY(configuration.Value, configuration.Duration)
                .SetEase(configuration.Easing)
                .SetDelay(configuration.Delay));
        }

        public static Tween DOPivotScaleLocalRotate(this RectTransform transform, RectTransformPivotScaleLocalRotateTweenConfiguration configuration)
        {
            return DOTween.Sequence()
                .Join(transform.DOPivot(configuration.PivotVector2StartEndTweenConfiguration))
                .Join(transform.DOScale(configuration.ScaleVector3StartEndTweenConfiguration))
                .Join(transform.DOLocalRotate(configuration.RotationVector3StartEndTweenConfiguration));
        }

        public static (Vector2 controlPoint1, Vector2 controlPoint2) CalculateAnticipationBezierControlPoints(
            Vector2 from,
            Vector2 to,
            Vector2 referenceDirection,
            float intensityOrigin,
            float accentOrigin,
            float intensityDestiny,
            float accentDestiny
        )
        {
            var delta = to - from;
            var deltaPerpendicular = delta.PerpendicularClockwise();

            var dot = Vector2.Dot(referenceDirection, deltaPerpendicular);
            if (dot <= 0)
            {
                deltaPerpendicular *= -1;
            }

            // Calculate control points
            var controlPoint1Offset = (deltaPerpendicular.normalized * (1f - accentOrigin) - delta.normalized * accentOrigin).normalized * intensityOrigin;
            Vector2 controlPoint1 = from + controlPoint1Offset;

            var controlPoint2Offset = (deltaPerpendicular.normalized * (1f - accentDestiny) - delta.normalized * accentDestiny).normalized * intensityDestiny;
            Vector2 controlPoint2 = to + controlPoint2Offset;

            return (controlPoint1, controlPoint2);
        }
    }
}
