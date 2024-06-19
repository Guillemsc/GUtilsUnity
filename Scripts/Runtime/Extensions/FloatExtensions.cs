using System;
using UnityEngine;

namespace GUtilsUnity.Extensions
{
    public static class FloatExtensions
    {
        /// <summary>
        /// Checks if the value is in-between the provided range.
        /// </summary>
        public static bool IsBetween(this float value, float minIncluding, float maxIncluding)
            => value >= minIncluding && value <= maxIncluding;

        /// <summary>
        /// Equivalent to <see cref="TimeSpan.FromSeconds"/>.
        /// </summary>
        public static TimeSpan ToSeconds(this float value) => TimeSpan.FromSeconds(value);

        /// <summary>
        /// Gets the decimals of the floating value.
        /// For example when called on 1.245 will return 0.245
        /// </summary>
        public static float GetDecimals(this float value) => value % 1;

        /// <summary>
        /// Equivalent to <see cref="Mathf.FloorToInt"/>.
        /// </summary>
        public static int ToIntFloor(this float value) => Mathf.FloorToInt(value);

        /// <summary>
        /// Equivalent to <see cref="Mathf.CeilToInt"/>.
        /// </summary>
        public static int ToIntCeil(this float value) => Mathf.CeilToInt(value);

        /// <summary>
        /// Equivalent to <see cref="Mathf.RoundToInt"/>.
        /// </summary>
        /// <example>[2.9f => 3] [5.3f => 5]</example>
        public static int ToIntRounded(this float value) => Mathf.RoundToInt(value);

        /// <summary>
        /// Equivalent to (int)value. Truncates to the current integer value.
        /// </summary>
        /// <example>[2.9f => 2] [5.3f => 5]</example>
        public static int ToIntTruncated(this float value) => (int)value;

        /// <summary>
        /// Creates a <see cref="Vector3"/> with x, y, z as the value.
        /// </summary>
        public static Vector3 ToVector3(this float value) => new(value, value, value);

        /// <summary>
        /// Creates a <see cref="Vector2"/> with x, y as the value.
        /// </summary>
        public static Vector2 ToVector2(this float value) => new(value, value);

        /// <summary>
        /// Creates a <see cref="Vector2"/> with x as the value, and y as the provided y parameter.
        /// </summary>
        public static Vector2 ToVector2X(this float value, float y = 0) => new(value, y);

        /// <summary>
        /// Creates a <see cref="Vector2"/> with y as the value, and x as the provided x parameter.
        /// </summary>
        public static Vector2 ToVector2Y(this float value, float x = 0) => new(x, value);

        /// <summary>
        /// Equivalent to <see cref="Math.Round(double, int)"/>.
        /// </summary>
        public static float Round(this float value, int decimals) => (float)Math.Round(value, decimals);

        /// <summary>
        /// Returns the absolute value of a specified number. Equivalent to <see cref="Math.Abs(float)"/>.
        /// </summary>
        public static float Abs(this float value) => Math.Abs(value);

        /// <summary>
        /// Assuming the current is a normalized value, it multiplies it by 100 and rounds the result to
        /// X decimals.
        /// </summary>
        public static float NormalizedToPercentage(this float value, int decimals = 0)
        {
            return (value * 100f).Round(decimals);
        }

        /// <summary>
        /// Generates a string with the value + "%" in the end.
        /// </summary>
        /// <example>2.9f => "2.9%"</example>
        public static string ToPercentageString(this float value)
        {
            return $"{value}%";
        }

        /// <summary>
        /// Assuming the current is a normalized value, it multiplies it by 100 and rounds the result to
        /// X decimals. Then it generates a string with the value + "%" in the end.
        /// </summary>
        /// <example>0.092f => "2.9%"</example>
        public static string NormalizedToPercentageString(this float value, int decimals = 0)
        {
            return value.NormalizedToPercentage(decimals).ToPercentageString();
        }

        /// <summary>
        /// Converts a float value of range of [0,1] to the range [-1,1].
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The converted value.</returns>
        /// <example>0 => -1 | 0.5f => 0  | 0.75f => 0.5f |  1 => 1 </example>
        public static float FromNormalizedRangeToSignedRange(this float value)
        {
            return (value * 2f) - 1f;
        }

        /// <summary>
        /// Converts a float value of range of [0,1] to the range [1,-1].
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The converted value.</returns>
        /// <example>0 => 1 | 0.5f => 0  | 0.75f => -0.5f |  1 => -1 </example>
        public static float FromNormalizedRangeToInvertedSignedRange(this float value)
        {
            return -((value * 2f) - 1f);
        }

        /// <summary>
        /// Converts a float value of range of [0,1] to a boncing range with 4 slopes
        /// From [0, 0.5] it becomes [0,1]
        /// From [0.5, 1] it becomes [1,0]
        /// From [-1, -0.5] it becomes [0,-1]
        /// From [-0.5, 0] it becomes [-1,0]
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The converted value.</returns>
        public static float FromNormalizedRangeToBouncingSignedRange(this float value)
        {
            if (value > 0.5f)
            {
                var remainingNormalized = 1f - ((value - 0.5f) * 2f);
                return remainingNormalized;
            }

            if (value < -0.5f)
            {
                var remainingNormalized = -(((value + 0.5f) * 2f) + 1f);
                return remainingNormalized;
            }

            return (value + 0.5f).FromNormalizedRangeToSignedRange();
        }

        /// <summary>
        /// Mirrors the specified float value around 1.
        /// </summary>
        /// <param name="value">The float value to mirror.</param>
        /// <returns>The mirrored value.</returns>
        /// <example>[0.75 => 1.25] [1.4 => 0.6]</example>
        public static float MirrorAround1(this float value)
        {
            float difference = value - 1;
            float mirroredValue = 1 - difference;
            return mirroredValue;
        }
    }
}
