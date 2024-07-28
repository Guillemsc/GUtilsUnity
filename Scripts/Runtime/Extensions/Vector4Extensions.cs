using GUtils.Extensions;
using GUtilsUnity.Extensions;
using UnityEngine;

namespace GUtilsUnity.Extensions
{
    public static class Vector4Extensions
    {
        /// <summary>
        /// Equivalent to x:<see cref="float.MaxValue"/> y:<see cref="float.MaxValue"/> z:<see cref="float.MaxValue"/>
        /// w:<see cref="float.MaxValue"/>.
        /// </summary>
        public static readonly Vector4 MaxValue = new(float.MaxValue, float.MaxValue, float.MaxValue, float.MaxValue);

        /// <summary>
        /// Equivalent to x:<see cref="float.MinValue"/> y:<see cref="float.MinValue"/> z:<see cref="float.MinValue"/>
        /// w:<see cref="float.MinValue"/>.
        /// </summary>
        public static readonly Vector4 MinValue = new(float.MinValue, float.MinValue, float.MinValue, float.MinValue);

        /// <summary>
        /// Equivalent to x:0.5f y:0.5f  z:0.5f
        /// w:0.5f .
        /// </summary>
        public static readonly Vector4 HalfOne = new(0.5f, 0.5f, 0.5f, 0.5f);

        /// <summary>
        /// Determines whether two <see cref="Vector4"/> objects are approximately equal within a small
        /// <see cref="Mathf.Epsilon"/> value.
        /// </summary>
        /// <param name="a">The first <see cref="Vector4"/> object.</param>
        /// <param name="b">The second <see cref="Vector4"/> object.</param>
        /// <returns><see langword="true"/> if the two <see cref="Vector4"/> objects are
        /// approximately equal; otherwise, <see langword="false"/>.</returns>
        public static bool AreEpsilonEquals(Vector4 a, Vector4 b)
            => MathExtensions.IsEpsilonEqualsZero((a - b).sqrMagnitude);

        /// <summary>
        /// Determines whether a <see cref="Vector4"/> object is approximately zero within a small
        /// <see cref="Mathf.Epsilon"/> value.
        /// </summary>
        /// <param name="a">The <see cref="Vector4"/> object.</param>
        /// <returns><see langword="true"/> if the <see cref="Vector4"/> object is approximately zero;
        /// otherwise, <see langword="false"/>.</returns>
        public static bool IsEpsilonEqualsZero(Vector4 a)
            => MathExtensions.IsEpsilonEqualsZero(a.sqrMagnitude);

        /// <summary>
        /// Returns a new <see cref="Vector4"/> object with the absolute values of each component (x, y, z, w)
        /// of the input <see cref="Vector4"/>.
        /// </summary>
        /// <param name="vector">The input <see cref="Vector4"/> object.</param>
        /// <returns>A new <see cref="Vector4"/> object with the absolute values of each component.</returns>
        public static Vector4 Abs(this Vector4 vector)
            => new(vector.x.Abs(), vector.y.Abs(), vector.z.Abs(), vector.w.Abs());

        /// <summary>
        /// Returns a new <see cref="Vector4"/> object representing the reciprocal values of each component
        /// of the input <see cref="Vector4"/>.
        /// </summary>
        /// <param name="vector">The input <see cref="Vector4"/> object.</param>
        /// <returns>A new <see cref="Vector4"/> object representing the reciprocal values of each component.</returns>
        /// <remarks>Reciprocal means dividing 1 by some value (1 / 3) = 0.5</remarks>
        public static Vector4 Reciprocal(this Vector4 vector)
            => new(1f / vector.x, 1f / vector.y, 1f / vector.z, 1f / vector.w);

        /// <summary>
        /// Clamps each component of a <see cref="Vector4"/> object between a minimum and maximum value.
        /// </summary>
        /// <param name="value">The <see cref="Vector4"/> object to clamp.</param>
        /// <param name="minValue">The minimum <see cref="Vector4"/> object.</param>
        /// <param name="maxValue">The maximum <see cref="Vector4"/> object.</param>
        /// <returns>A new <see cref="Vector4"/> object with each component clamped between
        /// the corresponding minimum and maximum values.</returns>
        public static Vector4 Clamp(Vector4 value, Vector4 minValue, Vector4 maxValue)
            => Vector4.Min(Vector4.Max(minValue, value), maxValue);

        /// <summary>
        /// Returns the maximum component value of a <see cref="Vector4"/> object.
        /// </summary>
        /// <param name="value">The <see cref="Vector4"/> object.</param>
        /// <returns>The maximum component value of the <see cref="Vector4"/> object.</returns>
        public static float MaxComponent(this Vector4 value)
            => Mathf.Max(Mathf.Max(value.x, value.y), Mathf.Max(value.z, value.w));

        /// <summary>
        /// Returns the minimum component value of a <see cref="Vector4"/> object.
        /// </summary>
        /// <param name="value">The <see cref="Vector4"/> object.</param>
        /// <returns>The minimum component value of the <see cref="Vector4"/> object.</returns>
        public static float MinComponent(this Vector4 value)
            => Mathf.Min(Mathf.Min(value.x, value.y), Mathf.Min(value.z, value.w));

        /// <summary>
        /// Multiplies each corresponding components of two <see cref="Vector4"/> objects and returns the result
        /// as a new <see cref="Vector4"/> object.
        /// </summary>
        /// <param name="first">The first <see cref="Vector4"/> object.</param>
        /// <param name="second">The second <see cref="Vector4"/> object.</param>
        /// <returns>A new <see cref="Vector4"/> object with each component being the product of the
        /// corresponding components of the input vectors.</returns>
        public static Vector4 Multiply(this Vector4 first, Vector4 second)
            => new(
                first.x * second.x,
                first.y * second.y,
                first.z * second.z,
                first.w * second.w
            );
    }
}
