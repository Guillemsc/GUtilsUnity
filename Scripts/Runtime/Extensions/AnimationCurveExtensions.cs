using UnityEngine;

namespace GUtilsUnity.Extensions
{
    public static class AnimationCurveExtensions
    {
        /// <summary>
        /// Simplest linear animation curve, which can be used as default/placeholder.
        /// </summary>
        public static AnimationCurve DefaultLinear => AnimationCurve.Linear(0.0f, 0.0f, 1.0f, 1.0f);

        /// <summary>
        /// Simplest easing animation curve, which can be used as default/placeholder.
        /// </summary>
        public static AnimationCurve DefaultEaseInOut => AnimationCurve.EaseInOut(0.0f, 0.0f, 1.0f, 1.0f);

        /// <summary>
        /// Simplest easing animation curve foor looping,
        /// which means that starts at value 0, has a point with value 1 in the center,
        /// and finishes again at value 0, creating a curve with a mountain shape.
        /// </summary>
        public static AnimationCurve DefaultEaseInOutLoop => new(
            new Keyframe(0f, 0f),
            new Keyframe(0.5f, 1f),
            new Keyframe(1f, 0f)
        );
    }
}
