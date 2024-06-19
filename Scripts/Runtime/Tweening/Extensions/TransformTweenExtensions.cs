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
    public static class TransformTweenExtensions
    {
        public static TweenerCore<Vector3, Vector3[], Vector3ArrayOptions> DoShake(this Transform transform, ShakeTweenConfiguration configuration)
        {
            return transform.DOShake(
                configuration.Duration,
                configuration.Strength,
                configuration.Vibrato,
                configuration.Randomness,
                configuration.IgnoreZAxis,
                configuration.FadeOut);
        }

        public static Tweener DOShakePosition(this Transform transform, ShakePositionTweenConfiguration configuration)
        {
            return transform.DOShakePosition(
                configuration.Duration,
                configuration.Strength,
                configuration.Vibrato,
                configuration.Randomness,
                configuration.Snapping,
                configuration.FadeOut);
        }

        /// <summary>Shakes a Transform's localRotation.</summary>
        /// <param name="duration">The duration of the tween</param>
        /// <param name="strength">The shake strength</param>
        /// <param name="vibrato">Indicates how much will the shake vibrate</param>
        /// <param name="randomness">Indicates how much the shake will be random (0 to 180 - values higher than 90 kind of suck, so beware).
        /// Setting it to 0 will shake along a single direction.</param>
        /// <param name="fadeOut">If TRUE the shake will automatically fadeOut smoothly within the tween's duration, otherwise it will not</param>
        public static Tweener DOShakeRotation(this Transform transform, ShakeRotationTweenConfiguration configuration)
        {
            return transform.DOShakeRotation(
                configuration.Duration,
                configuration.Strength,
                configuration.Vibrato,
                configuration.Randomness,
                configuration.FadeOut);
        }

        public static Tween DOMove(this Transform transform, Vector3TweenConfiguration configuration)
        {
            return GUtilsUnity.Extensions.TweenExtensions.ExtraSequenceFix(transform
                .DOMove(configuration.Value, configuration.Duration)
                .SetEase(configuration.Easing)
                .SetDelay(configuration.Delay));
        }

        public static Tween DOMove(this Transform transform, Vector3 value, NoValueTweenConfiguration configuration)
        {
            return GUtilsUnity.Extensions.TweenExtensions.ExtraSequenceFix(transform
                .DOMove(value, configuration.Duration)
                .SetEase(configuration.Easing)
                .SetDelay(configuration.Delay));
        }

        public static Tween DOLocalMove(this Transform transform, Vector3TweenConfiguration configuration)
        {
            return GUtilsUnity.Extensions.TweenExtensions.ExtraSequenceFix(transform
                .DOLocalMove(configuration.Value, configuration.Duration)
                .SetEase(configuration.Easing)
                .SetDelay(configuration.Delay));
        }

        public static Tween DOMoveX(this Transform transform, FloatTweenConfiguration configuration)
        {
            return GUtilsUnity.Extensions.TweenExtensions.ExtraSequenceFix(transform
                .DOMoveX(configuration.Value, configuration.Duration)
                .SetEase(configuration.Easing)
                .SetDelay(configuration.Delay));
        }

        public static Tween DOLocalMoveX(this Transform transform, FloatTweenConfiguration configuration)
        {
            return GUtilsUnity.Extensions.TweenExtensions.ExtraSequenceFix(transform
                .DOLocalMoveX(configuration.Value, configuration.Duration)
                .SetEase(configuration.Easing)
                .SetDelay(configuration.Delay));
        }

        public static Tween DOMoveY(this Transform transform, FloatTweenConfiguration configuration)
        {
            return GUtilsUnity.Extensions.TweenExtensions.ExtraSequenceFix(transform
                .DOMoveY(configuration.Value, configuration.Duration)
                .SetEase(configuration.Easing)
                .SetDelay(configuration.Delay));
        }

        public static Tween DOMoveY(this Transform transform, float value, NoValueTweenConfiguration configuration)
        {
            return GUtilsUnity.Extensions.TweenExtensions.ExtraSequenceFix(transform
                .DOMoveY(value, configuration.Duration)
                .SetEase(configuration.Easing)
                .SetDelay(configuration.Delay));
        }

        public static Tween DOLocalMoveY(this Transform transform, FloatTweenConfiguration configuration)
        {
            return GUtilsUnity.Extensions.TweenExtensions.ExtraSequenceFix(transform
                .DOLocalMoveY(configuration.Value, configuration.Duration)
                .SetEase(configuration.Easing)
                .SetDelay(configuration.Delay));
        }

        public static Tween DOMoveZ(this Transform transform, FloatTweenConfiguration configuration)
        {
            return GUtilsUnity.Extensions.TweenExtensions.ExtraSequenceFix(transform
                .DOMoveZ(configuration.Value, configuration.Duration)
                .SetEase(configuration.Easing)
                .SetDelay(configuration.Delay));
        }

        public static Tween DOLocalMoveZ(this Transform transform, FloatTweenConfiguration configuration)
        {
            return GUtilsUnity.Extensions.TweenExtensions.ExtraSequenceFix(transform
                .DOLocalMoveZ(configuration.Value, configuration.Duration)
                .SetEase(configuration.Easing)
                .SetDelay(configuration.Delay));
        }

        public static Tween DORotate(this Transform transform, RotationVector3TweenConfiguration configuration)
        {
            return GUtilsUnity.Extensions.TweenExtensions.ExtraSequenceFix(transform
                .DORotate(configuration.Value, configuration.Duration, configuration.RotateMode)
                .SetEase(configuration.Easing)
                .SetDelay(configuration.Delay));
        }

        public static Tween DORotateX(this Transform transform, RotationFloatTweenConfiguration configuration)
        {
            Vector3 eulerAngles = transform.eulerAngles;

            return GUtilsUnity.Extensions.TweenExtensions.ExtraSequenceFix(transform
                .DORotate(new Vector3(configuration.Value, eulerAngles.y, eulerAngles.z), configuration.Duration, configuration.RotateMode)
                .SetEase(configuration.Easing)
                .SetDelay(configuration.Delay));
        }

        public static Tween DORotateY(this Transform transform, RotationFloatTweenConfiguration configuration)
        {
            Vector3 eulerAngles = transform.eulerAngles;

            return GUtilsUnity.Extensions.TweenExtensions.ExtraSequenceFix(transform
                .DORotate(new Vector3(eulerAngles.x, configuration.Value, eulerAngles.z), configuration.Duration, configuration.RotateMode)
                .SetEase(configuration.Easing)
                .SetDelay(configuration.Delay));
        }

        public static Tween DORotateZ(this Transform transform, RotationFloatTweenConfiguration configuration)
        {
            Vector3 eulerAngles = transform.eulerAngles;

            return GUtilsUnity.Extensions.TweenExtensions.ExtraSequenceFix(transform
                .DORotate(new Vector3(eulerAngles.x, eulerAngles.y, configuration.Value), configuration.Duration, configuration.RotateMode)
                .SetEase(configuration.Easing)
                .SetDelay(configuration.Delay));
        }

        public static Tween DOLocalRotate(this Transform transform, RotationVector3TweenConfiguration configuration)
        {
            return GUtilsUnity.Extensions.TweenExtensions.ExtraSequenceFix(transform
                .DOLocalRotate(configuration.Value, configuration.Duration, configuration.RotateMode)
                .SetEase(configuration.Easing)
                .SetDelay(configuration.Delay));
        }

        public static Tween DOLocalRotate(this Transform transform, RotationVector3StartEndTweenConfiguration configuration)
        {
            return DOTween.Sequence()
                .Join(transform.DOLocalRotate(configuration.StartRotationVector3TweenConfiguration))
                .AppendInterval(configuration.SustainTime)
                .Append(transform.DOLocalRotate(configuration.EndRotationVector3TweenConfiguration));
        }

        public static Tween DOLocalRotateX(this Transform transform, float value, float duration, RotateMode rotateMode = RotateMode.Fast)
        {
            Vector3 eulerAngles = transform.eulerAngles;

            return transform.DOLocalRotate(
                new Vector3(value, eulerAngles.y, eulerAngles.z),
                duration,
                rotateMode
            );
        }

        public static Tween DOLocalRotateX(this Transform transform, RotationFloatTweenConfiguration configuration)
        {
            return GUtilsUnity.Extensions.TweenExtensions.ExtraSequenceFix(transform
                .DOLocalRotateX(configuration.Value, configuration.Duration, configuration.RotateMode)
                .SetEase(configuration.Easing)
                .SetDelay(configuration.Delay));
        }

        public static Tween DOLocalRotateY(this Transform transform, float value, float duration, RotateMode rotateMode = RotateMode.Fast)
        {
            Vector3 eulerAngles = transform.eulerAngles;

            return transform.DOLocalRotate(
                new Vector3(eulerAngles.x, value, eulerAngles.z),
                duration,
                rotateMode
            );
        }

        public static Tween DOLocalRotateY(this Transform transform, RotationFloatTweenConfiguration configuration)
        {
            return GUtilsUnity.Extensions.TweenExtensions.ExtraSequenceFix(transform
                .DOLocalRotateY(configuration.Value, configuration.Duration, configuration.RotateMode)
                .SetEase(configuration.Easing)
                .SetDelay(configuration.Delay));
        }

        public static Tween DOLocalRotateZ(this Transform transform, float value, float duration, RotateMode rotateMode = RotateMode.Fast)
        {
            Vector3 eulerAngles = transform.eulerAngles;

            return transform.DOLocalRotate(
                new Vector3(eulerAngles.x, eulerAngles.y, value),
                duration,
                rotateMode
            );
        }

        public static Tween DOLocalRotateZ(this Transform transform, RotationFloatTweenConfiguration configuration)
        {
            return GUtilsUnity.Extensions.TweenExtensions.ExtraSequenceFix(transform
                .DOLocalRotateZ(configuration.Value, configuration.Duration, configuration.RotateMode)
                .SetEase(configuration.Easing)
                .SetDelay(configuration.Delay));
        }

        public static Tween DOScale(this Transform transform, Vector3TweenConfiguration configuration)
        {
            return GUtilsUnity.Extensions.TweenExtensions.ExtraSequenceFix(transform
                .DOScale(configuration.Value, configuration.Duration)
                .SetEase(configuration.Easing)
                .SetDelay(configuration.Delay));
        }

        public static Tween DOScale(this Transform transform, Vector3StartEndTweenConfiguration configuration)
        {
            return DOTween.Sequence()
                .Join(transform.DOScale(configuration.StartVector3TweenConfiguration))
                .AppendInterval(configuration.SustainTime)
                .Append(transform.DOScale(configuration.EndVector3TweenConfiguration));
        }

        public static Tween DOScale(this Transform transform, Vector3 value, NoValueTweenConfiguration configuration)
        {
            return GUtilsUnity.Extensions.TweenExtensions.ExtraSequenceFix(transform
                .DOScale(value, configuration.Duration)
                .SetEase(configuration.Easing)
                .SetDelay(configuration.Delay));
        }

        public static Tween DOScale(this Transform transform, float value, NoValueTweenConfiguration configuration)
        {
            return GUtilsUnity.Extensions.TweenExtensions.ExtraSequenceFix(transform
                .DOScale(value.ToVector3(), configuration.Duration)
                .SetEase(configuration.Easing)
                .SetDelay(configuration.Delay));
        }

        public static Tween DOScale(this Transform transform, FloatTweenConfiguration configuration)
        {
            return GUtilsUnity.Extensions.TweenExtensions.ExtraSequenceFix(transform
                .DOScale(configuration.Value.ToVector3(), configuration.Duration)
                .SetEase(configuration.Easing)
                .SetDelay(configuration.Delay));
        }

        public static Tween DOScaleX(this Transform transform, FloatTweenConfiguration configuration)
        {
            return GUtilsUnity.Extensions.TweenExtensions.ExtraSequenceFix(transform
                .DOScaleX(configuration.Value, configuration.Duration)
                .SetEase(configuration.Easing)
                .SetDelay(configuration.Delay));
        }

        public static Tween DOScaleX(this Transform transform, float value, NoValueTweenConfiguration configuration)
        {
            return GUtilsUnity.Extensions.TweenExtensions.ExtraSequenceFix(transform
                .DOScaleX(value, configuration.Duration)
                .SetEase(configuration.Easing)
                .SetDelay(configuration.Delay));
        }

        public static Tween DOScaleY(this Transform transform, FloatTweenConfiguration configuration)
        {
            return GUtilsUnity.Extensions.TweenExtensions.ExtraSequenceFix(transform
                .DOScaleY(configuration.Value, configuration.Duration)
                .SetEase(configuration.Easing)
                .SetDelay(configuration.Delay));
        }

        public static Tween DOScaleY(this Transform transform, float value, NoValueTweenConfiguration configuration)
        {
            return GUtilsUnity.Extensions.TweenExtensions.ExtraSequenceFix(transform
                .DOScaleY(value, configuration.Duration)
                .SetEase(configuration.Easing)
                .SetDelay(configuration.Delay));
        }

        public static Tween DOScaleZ(this Transform transform, FloatTweenConfiguration configuration)
        {
            return GUtilsUnity.Extensions.TweenExtensions.ExtraSequenceFix(transform
                .DOScaleZ(configuration.Value, configuration.Duration)
                .SetEase(configuration.Easing)
                .SetDelay(configuration.Delay));
        }

        public static Tween DOScaleZ(this Transform transform, float value, NoValueTweenConfiguration configuration)
        {
            return GUtilsUnity.Extensions.TweenExtensions.ExtraSequenceFix(transform
                .DOScaleZ(value, configuration.Duration)
                .SetEase(configuration.Easing)
                .SetDelay(configuration.Delay));
        }

        public static TweenerCore<Vector3, Vector3[], Vector3ArrayOptions> DOShake(
            this Transform transform,
            float duration,
            float strength = 3f,
            int vibrato = 10,
            float randomness = 90f,
            bool ignoreZAxis = true,
            bool fadeOut = true)
        {
            return DOTween.Shake(
                () => transform.position,
                x => transform.position = x,
                duration,
                strength,
                vibrato,
                randomness,
                ignoreZAxis,
                fadeOut
            );
        }


        /// <summary>
        /// Transitions an object from its current position to a target position using a Bezier curve
        /// </summary>
        /// <param name="transform">The object's transform to move</param>
        /// <param name="to">The target position for the object</param>
        /// <param name="referenceDirection">Determines the desired up vector; movements that happens on from left to right will do a bezier from left to right and the contrary on the right</param>
        /// <param name="referencePlane">This vector together with the referenceDirection parameter define the plane where the operation will take place</param>
        /// <param name="duration">Duration of the transition in seconds</param>
        /// <param name="intensityOrigin">Intensity of the Bezier curve at the starting point; higher values create a larger curve</param>
        /// <param name="accentOrigin">Value in the range [0,1] that adjusts the Bezier curve's movement; closer to 0 makes the curve more perpendicular to the direction, closer to 1 places the control point in the opposite direction of the movement from start to end</param>
        /// <param name="intensityDestiny">Intensity of the Bezier curve at the target position; higher values create a larger curve</param>
        /// <param name="accentDestiny">Value in the range [0,1] that adjusts the Bezier curve's movement; closer to 0 makes the curve more perpendicular to the direction, closer to 1 places the control point in the opposite direction of the movement from start to end</param>
        public static TweenerCore<Vector3, Path, PathOptions> DoAnticipationBezierPath(
            this Transform transform,
            Vector3 to,
            Vector3 referencePlane,
            float duration,
            float intensityOrigin,
            float accentOrigin,
            float intensityDestiny,
            float accentDestiny
        )
        {
            var originPosition = transform.position;
            var controlPoints = CalculateAnticipationBezierControlPoints(
                originPosition,
                to,
                referencePlane,
                intensityOrigin,
                accentOrigin,
                intensityDestiny,
                accentDestiny
            );

            // Create the Bezier path
            Vector3[] path = { to, controlPoints.controlPoint1, controlPoints.controlPoint2 };

            return transform.DOPath(
                path,
                duration,
                PathType.CubicBezier
            );
        }

        public static TweenerCore<Vector3, Path, PathOptions> DoAnticipationBezierPath(
            this Transform transform,
            Vector3 to,
            Vector3 referencePlane,
            NoDestinyAnticipationBezierPathTweenConfiguration noDestinyAnticipationBezierPathTweenConfiguration
            )
        {
            return transform.DoAnticipationBezierPath(
                    to,
                    referencePlane,
                    noDestinyAnticipationBezierPathTweenConfiguration.Duration,
                    noDestinyAnticipationBezierPathTweenConfiguration.IntensityOrigin,
                    noDestinyAnticipationBezierPathTweenConfiguration.AccentOrigin,
                    noDestinyAnticipationBezierPathTweenConfiguration.IntensityDestiny,
                    noDestinyAnticipationBezierPathTweenConfiguration.AccentDestiny)
                .SetEase(noDestinyAnticipationBezierPathTweenConfiguration.Easing)
                .SetDelay(noDestinyAnticipationBezierPathTweenConfiguration.Delay);
        }

        public static (Vector3 controlPoint1, Vector3 controlPoint2) CalculateAnticipationBezierControlPoints(
            Vector3 from,
            Vector3 to,
            Vector3 referenceRight,
            float intensityOrigin,
            float accentOrigin,
            float intensityDestiny,
            float accentDestiny
        )
        {
            referenceRight = referenceRight.normalized;


            var direction = (to - from).normalized;
            var perpendicular = TrigonometryExtensions.GetPerpendicular(direction, referenceRight).normalized;
            //var realRight = TrigonometryExtensions.GetPerpendicular(perpendicular, direction);
            var referenceUp = Quaternion.AngleAxis(-90f, perpendicular) * referenceRight;

            // var dot = Vector3.Dot(referenceDirection, perpendicular);
            // if (dot <= 0)
            // {
            //     perpendicular *= -1;
            // }

            //Vector3 controlPoint1Offset = (perpendicular * (1f - accentOrigin) - direction * accentOrigin).normalized * intensityOrigin;

            var rotationAngleOrigin = accentOrigin * -180f;
            var rotationAngleDestiny = accentDestiny * 180f;

            var angle = Vector3.SignedAngle(referenceUp, direction, perpendicular);

            if (angle <= 0)
            {
                rotationAngleOrigin *= -1;
                rotationAngleDestiny *= -1;
            }

            var rotationOrigin = Quaternion.AngleAxis(rotationAngleOrigin, perpendicular);
            var rotationDestiny = Quaternion.AngleAxis(rotationAngleDestiny, perpendicular);

            // var accentOriginSignedRange = accentOrigin.FromNormalizedRangeToInvertedSignedRange();
            // var accentOriginBouncingRange = accentOrigin.FromNormalizedRangeToBouncingSignedRange();
            //
            // Vector3 controlPoint1Offset = ((direction * accentOriginSignedRange) + (realRight * accentOriginBouncingRange)).normalized * intensityOrigin;
            Vector3 controlPoint1Offset = (rotationOrigin * direction) * intensityOrigin;
            Vector3 controlPoint1 = from + controlPoint1Offset;

            // var accentDestinySignedRange = accentDestiny.FromNormalizedRangeToInvertedSignedRange();
            // var accentDestinyBouncingRange = accentDestiny.FromNormalizedRangeToBouncingSignedRange();
            //
            //Vector3 controlPoint2Offset = ((direction * accentDestinySignedRange) + (realRight * accentDestinyBouncingRange)).normalized * intensityDestiny;
            Vector3 controlPoint2Offset = (rotationDestiny * -direction) * intensityDestiny;
            Vector3 controlPoint2 = to + controlPoint2Offset;

            return (controlPoint1, controlPoint2);
        }
    }
}
