using System.Collections.Generic;
using System.Linq;
using GUtils.Extensions;
using UnityEngine;

namespace GUtilsUnity.Extensions
{
    public static class Vector2Extensions
    {
        /// <summary>
        /// Equivalent to x:<see cref="float.MaxValue"/> y:<see cref="float.MaxValue"/>.
        /// </summary>
        public static readonly Vector2 MaxValue = new(float.MaxValue, float.MaxValue);

        /// <summary>
        /// Equivalent to x:<see cref="float.MinValue"/> y:<see cref="float.MinValue"/>.
        /// </summary>
        public static readonly Vector2 MinValue = new(float.MinValue, float.MinValue);

        /// <summary>
        /// Equivalent to x:0.5 y:0.5.
        /// </summary>
        public static readonly Vector2 HalfOne = new(0.5f, 0.5f);

        /// <summary>
        /// Determines whether two Vector2 objects are approximately equal within a certain <see cref="Mathf.Epsilon"/> value.
        /// </summary>
        /// <param name="a">The first Vector2 object.</param>
        /// <param name="b">The second Vector2 object.</param>
        /// <returns><c>true</c> if the two Vector2 objects are approximately equal; otherwise, <c>false</c>.</returns>
        public static bool AreEpsilonEquals(Vector2 a, Vector2 b)
        {
            float distance = (a - b).sqrMagnitude;

            return  GUtils.Extensions.MathExtensions.IsEpsilonEqualsZero(distance);
        }

        /// <summary>
        /// Checks if the magnitude of the Vector2 object is approximately zero within a certain <see cref="Mathf.Epsilon"/> value.
        /// </summary>
        /// <param name="a">The Vector2 object.</param>
        /// <returns><c>true</c> if the magnitude of the Vector2 object is approximately zero; otherwise, <c>false</c>.</returns>
        public static bool IsMagnitudeEpsilonEqualsZero(Vector2 a)
        {
            return GUtils.Extensions.MathExtensions.IsEpsilonEqualsZero(a.sqrMagnitude);
        }

        public static Vector2 MinComponents(this IEnumerable<Vector2> vectors)
        {
            Vector2 minVector = vectors.Aggregate((currentMin, nextVector) =>
                new Vector2(Mathf.Min(currentMin.x, nextVector.x), Mathf.Min(currentMin.y, nextVector.y)));

            return minVector;
        }

        public static Vector2 MaxComponents(this IEnumerable<Vector2> vectors)
        {
            Vector2 maxVector = vectors.Aggregate((currentMin, nextVector) =>
                new Vector2(Mathf.Max(currentMin.x, nextVector.x), Mathf.Max(currentMin.y, nextVector.y)));

            return maxVector;
        }

        /// <summary>
        /// Deconstructs the Vector2 object into its individual x and y components.
        /// </summary>
        /// <param name="vector">The Vector2 object to deconstruct.</param>
        /// <param name="x">The output variable that will hold the x component of the Vector2.</param>
        /// <param name="y">The output variable that will hold the y component of the Vector2.</param>
        public static void Deconstruct(this Vector2 vector, out float x, out float y)
        {
            x = vector.x;
            y = vector.y;
        }

        /// <summary>
        /// Returns a new Vector2 object with the specified x component, while keeping the original y component.
        /// </summary>
        /// <param name="vector">The original Vector2 object.</param>
        /// <param name="x">The new x component.</param>
        /// <returns>A new Vector2 object with the specified x component and the original y component.</returns>
        public static Vector2 WithX(this Vector2 vector, float x) => new(x, vector.y);

        /// <summary>
        /// Returns a new Vector2 object with the specified y component, while keeping the original x component.
        /// </summary>
        /// <param name="vector">The original Vector2 object.</param>
        /// <param name="y">The new y component.</param>
        /// <returns>A new Vector2 object with the specified y component and the original x component.</returns>
        public static Vector2 WithY(this Vector2 vector, float y) => new(vector.x, y);

        [System.Obsolete("This method is obsolete. Use DirectionFromAngleDegrees instead")]
        public static Vector2 DirectionFromAngle(float degrees)
        {
            return DirectionFromAngleDegrees(degrees);
        }

        /// <summary>
        /// Returns the direction Vector2 from the specified angle in degrees.
        /// </summary>
        /// <param name="degrees">The angle in degrees.</param>
        /// <returns>The direction Vector2 corresponding to the specified angle in degrees.</returns>
        /// <example>.[0 deegres = (1, 0)] [90 deegres = (0, 1)] [180 deegres = (-1, 0)] [270 deegres = (0, -1)]</example>
        public static Vector2 DirectionFromAngleDegrees(float degrees)
        {
            float radians = degrees * Mathf.Deg2Rad;

            return new Vector2(Mathf.Cos(radians), Mathf.Sin(radians));
        }

        /// <summary>
        /// Returns the angle in radians from the Vector2 direction.
        /// </summary>
        /// <param name="direction">The Vector2 direction.</param>
        /// <returns>The angle in radians from the Vector2 direction.</returns>
        public static float AngleRadiansFromDirection(this Vector2 direction)
        {
            return Mathf.Atan2(direction.y, direction.x);
        }

        [System.Obsolete("This method is obsolete. Use AngleDegreesFromDirection instead")]
        public static float AngleFromDirection(this Vector2 direction)
        {
            return direction.AngleDegreesFromDirection();
        }

        /// <summary>
        /// Returns the angle in degrees from the Vector2 direction.
        /// </summary>
        /// <param name="direction">The Vector2 direction.</param>
        /// <returns>The angle in degrees from the Vector2 direction.</returns>
        public static float AngleDegreesFromDirection(this Vector2 direction)
        {
            float radians = AngleRadiansFromDirection(direction);

            return radians * Mathf.Rad2Deg;
        }

        /// <summary>
        /// Returns the angle in radians between Vector2 a and Vector2 b.
        /// </summary>
        /// <param name="a">The first Vector2.</param>
        /// <param name="b">The second Vector2.</param>
        /// <returns>The angle in radians between Vector2 a and Vector2 b.</returns>
        public static float AngleRadiansBetween(this Vector2 a, Vector2 b)
        {
            return Mathf.Atan2(b.y - a.y, b.x - a.x);
        }

        [System.Obsolete("This method is obsolete. Use AngleDegreesBetween instead")]
        public static float AngleBetween(this Vector2 a, Vector2 b)
        {
            return a.AngleDegreesBetween(b);
        }

        /// <summary>
        /// Returns the angle in degrees between Vector2 a and Vector2 b.
        /// </summary>
        /// <param name="a">The first Vector2.</param>
        /// <param name="b">The second Vector2.</param>
        /// <returns>The angle in degrees between Vector2 a and Vector2 b.</returns>
        public static float AngleDegreesBetween(this Vector2 a, Vector2 b)
        {
            float radians = a.AngleRadiansBetween(b);

            return radians * Mathf.Rad2Deg;
        }

        [System.Obsolete("This method is obsolete. Use RotateDegrees instead")]
        public static Vector2 Rotate(this Vector2 vector, float degrees)
        {
            return vector.RotateDegrees(degrees);
        }

        /// <summary>
        /// Rotates the Vector2 by the specified degrees.
        /// </summary>
        /// <param name="vector">The original Vector2 object.</param>
        /// <param name="degrees">The rotation angle in degrees.</param>
        /// <returns>The rotated Vector2.</returns>
        public static Vector2 RotateDegrees(this Vector2 vector, float degrees)
        {
            float sin = Mathf.Sin(degrees * Mathf.Deg2Rad);
            float cos = Mathf.Cos(degrees * Mathf.Deg2Rad);

            vector.x = (cos * vector.x) - (sin * vector.y);
            vector.y = (sin * vector.x) + (cos * vector.y);

            return vector;
        }

        /// <summary>
        /// Returns a Vector2 that is a spherical linear interpolation between Vector2 a and Vector2 b by the specified factor.
        /// </summary>
        /// <param name="a">The start Vector2.</param>
        /// <param name="b">The end Vector2.</param>
        /// <param name="factor">The interpolation factor. Value is clamped between 0 and 1.</param>
        /// <returns>The interpolated Vector2.</returns>
        public static Vector2 Slerp(Vector2 a, Vector2 b, float factor)
        {
            float angle = Vector2.Angle(a, b);
            float angleFactor = angle * factor;
            float magnitude = Mathf.Lerp(a.magnitude, b.magnitude, factor);

            Vector2 vector = a.RotateDegrees(angleFactor).normalized;
            Vector2 vectorWithMagnitude = vector * magnitude;

            return vectorWithMagnitude;
        }

        /// <summary>
        /// Returns a new Vector2 with each component (x, y) set to the maximum value
        /// between the corresponding components of Vector2 a and Vector2 b.
        /// </summary>
        /// <param name="a">The first Vector2.</param>
        /// <param name="b">The second Vector2.</param>
        /// <returns>A new Vector2 with the maximum elements.</returns>
        public static Vector2 MaxElements(Vector2 a, Vector2 b)
            => new(Mathf.Max(a.x, b.x), Mathf.Max(a.y, b.y));

        /// <summary>
        /// Returns a new Vector2 with each component (x, y) set to the minimum value
        /// between the corresponding components of Vector2 a and Vector2 b.
        /// </summary>
        /// <param name="a">The first Vector2.</param>
        /// <param name="b">The second Vector2.</param>
        /// <returns>A new Vector2 with the minimum elements.</returns>
        public static Vector2 MinElements(Vector2 a, Vector2 b)
            => new(Mathf.Min(a.x, b.x), Mathf.Min(a.y, b.y));

        /// <summary>
        /// Returns the maximum value between x and y.
        /// </summary>
        /// <param name="value">The Vector2.</param>
        /// <returns>The maximum component value.</returns>
        public static float MaxComponent(this Vector2 value)
            => Mathf.Max(value.x, value.y);

        /// <summary>
        /// Returns the minimum value between x and y.
        /// </summary>
        /// <param name="value">The Vector2.</param>
        /// <returns>The minimum component value.</returns>
        public static float MinComponent(this Vector2 value)
            => Mathf.Min(value.x, value.y);

        /// <summary>
        /// Returns a new Vector2 with each component (x, y) set to the
        /// absolute value of the corresponding.
        /// </summary>
        /// <param name="vector">The input Vector2.</param>
        /// <returns>A new Vector2 with absolute values.</returns>
        public static Vector2 Abs(this Vector2 vector)
            => new(vector.x.Abs(), vector.y.Abs());

        /// <summary>
        /// Returns a new Vector2 with the x and y components swapped.
        /// </summary>
        /// <param name="vector">The input Vector2.</param>
        /// <returns>A new Vector2 with swapped axis.</returns>
        public static Vector2 SwapAxis(this Vector2 vector)
            => new (vector.y, vector.x);

        /// <summary>
        /// Returns a new Vector2 with the x component flipped.
        /// </summary>
        /// <param name="vector">The input Vector2.</param>
        /// <returns>A new Vector2 with flipped x component.</returns>
        public static Vector2 FlipX(this Vector2 vector)
            => new(-vector.x, vector.y);

        /// <summary>
        /// Returns a new Vector2 with the y component flipped.
        /// </summary>
        /// <param name="vector">The input Vector2.</param>
        /// <returns>A new Vector2 with flipped y component.</returns>
        public static Vector2 FlipY(this Vector2 vector)
            => new(vector.x, -vector.y);

        /// <summary>
        /// Returns a new Vector2 that is rotated 90 degrees clockwise relative to the input Vector2.
        /// </summary>
        /// <param name="vector">The input Vector2.</param>
        /// <returns>A new Vector2 rotated 90 degrees clockwise.</returns>
        public static Vector2 PerpendicularClockwise(this Vector2 vector)
            => new(vector.y, -vector.x);

        /// <summary>
        /// Returns a new Vector2 that is rotated 90 degrees counter-clockwise relative to the input Vector2.
        /// </summary>
        /// <param name="vector">The input Vector2.</param>
        /// <returns>A new Vector2 rotated 90 degrees counter-clockwise.</returns>
        public static Vector2 PerpendicularCounterClockwise(this Vector2 vector)
            => new(-vector.y, vector.x);

        /// <summary>
        /// Converts a Vector2 to a Vector3 with the x and y components of the Vector2 and a specified z component.
        /// </summary>
        /// <param name="vector">The input Vector2.</param>
        /// <param name="z">The z component of the resulting Vector3.</param>
        /// <returns>A new Vector3 with the x and y components of the input Vector2 and the specified z component.</returns>
        public static Vector3 ToVector3XY(this Vector2 vector, float z)
            => new(vector.x, vector.y, z);

        /// <summary>
        /// Converts a Vector2 to a Vector3 with the x and y components of the Vector2 and a z component of 0
        /// </summary>
        /// <param name="vector">The input Vector2.</param>
        /// <returns>A new Vector3 with the x and y components of the input Vector2 and the specified z component.</returns>
        public static Vector3 ToVector3XY(this Vector2 vector)
            => vector.ToVector3XY(0);


        /// <summary>
        /// Converts a Vector2 to a Vector3 with the x and z components of the Vector2 and a specified y component.
        /// </summary>
        /// <param name="vector">The input Vector2.</param>
        /// <param name="y">The y component of the resulting Vector3.</param>
        /// <returns>A new Vector3 with the x and z components of the input Vector2 and the specified y component.</returns>
        public static Vector3 ToVector3XZ(this Vector2 vector, float y)
            => new(vector.x, y, vector.y);

        /// <summary>
        /// Converts a Vector2 to a Vector3 with the x and z components of the Vector2 and y component of 0.
        /// </summary>
        /// <param name="vector">The input Vector2.</param>
        /// <returns>A new Vector3 with the x and z components of the input Vector2 and y component of 0.</returns>
        public static Vector3 ToVector3XZ(this Vector2 vector)
            => vector.ToVector3XZ(0);

        /// <summary>
        /// Converts a Vector2 to a Vector3 with the y and z components of the Vector2 and a specified x component.
        /// </summary>
        /// <param name="vector">The input Vector2.</param>
        /// <param name="x">The x component of the resulting Vector3.</param>
        /// <returns>A new Vector3 with the y and z components of the input Vector2 and the specified x component.</returns>
        public static Vector3 ToVector3YZ(this Vector2 vector, float x)
            => new(x, vector.x, vector.y);

        /// <summary>
        /// Converts a Vector2 to a Vector3 with the y and z components of the Vector2 and x component of 0.
        /// </summary>
        /// <param name="vector">The input Vector2.</param>
        /// <returns>A new Vector3 with the y and z components of the input Vector2 and x component of 0.</returns>
        public static Vector3 ToVector3YZ(this Vector2 vector)
            => vector.ToVector3YZ(0);

        [System.Obsolete("Use ToVector2IntTruncated")]
        public static Vector2Int ToVector2Int(this Vector2 vector)
            => vector.ToVector2IntTruncated();

        /// <summary>
        /// Converts a Vector2 to a Vector2Int by truncating the decimal part of each component.
        /// </summary>
        /// <param name="vector">The input Vector2.</param>
        /// <returns>A new Vector2Int with truncated components of the input Vector2.</returns>
        /// <example>(2.9f, 4.4f) => (2, 4)</example>
        public static Vector2Int ToVector2IntTruncated(this Vector2 vector)
            => new(vector.x.ToIntTruncated(), vector.y.ToIntTruncated());

        /// <summary>
        /// Converts a Vector2 to a Vector2Int by rounding each component to the nearest integer.
        /// </summary>
        /// <param name="vector">The input Vector2.</param>
        /// <returns>A new Vector2Int with rounded components of the input Vector2.</returns>
        /// <example>(2.9f, 4.4f) => (3, 4)</example>
        public static Vector2Int ToVector2IntRounded(this Vector2 vector)
            => new (vector.x.ToIntRounded(), vector.y.ToIntRounded());

        /// <summary>
        /// Returns the reciprocal of the input Vector2 by taking the reciprocal of each component.
        /// </summary>
        /// <param name="vector">The input Vector2.</param>
        /// <returns>A new Vector2 with the reciprocal of each component of the input Vector2.</returns>
        /// <example>(2f, 3f) => (0.5, 0.33)</example>
        /// <remarks>Reciprocal means dividing 1 by some value (1 / 3) = 0.5</remarks>
        public static Vector2 Reciprocal(this Vector2 vector)
            => new(1f / vector.x, 1f / vector.y);

        /// <summary>
        /// Multiplies each component of the first Vector2 by the corresponding component of the second Vector2.
        /// </summary>
        /// <param name="first">The first Vector2.</param>
        /// <param name="second">The second Vector2.</param>
        /// <returns>A new Vector2 with the multiplied components of the input Vector2s.</returns>
        public static Vector2 Multiply(this Vector2 first, Vector2 second)
            => new(first.x * second.x, first.y * second.y);

        /// <summary>
        /// Clamps the input Vector2 between a minimum and maximum Vector2.
        /// </summary>
        /// <param name="value">The input Vector2 to be clamped.</param>
        /// <param name="minValue">The minimum Vector2.</param>
        /// <param name="maxValue">The maximum Vector2.</param>
        /// <returns>
        /// A new Vector2 where each component is clamped between the corresponding components of the minimum and maximum Vector2.
        /// </returns>
        public static Vector2 Clamp(Vector2 value, Vector2 minValue, Vector2 maxValue)
        {
            value = Vector2.Max(minValue, value);   
            value = Vector2.Min(maxValue, value);  
            
            return value;
        }

        /// <summary>
        /// Given a position, a pivot where this position should be, a delta between objects, a count of objects and
        /// the index of the current object, get the position where the current object should be placed in respect to the position.
        /// </summary>
        /// <example>
        /// Given a position of (1, 1) a pivot of (0.5, 0) a delta of (1, 0) and a count of 2.
        /// For index 0 the object will be placed at (0.5, 0) and for index 1 (1.5, 0).
        /// </example>
        public static Vector2 GetPositionWithPivotOffset(Vector2 position, Vector2 pivot, Vector2 delta, int count, int index)
        {
            Vector2 distance = delta * (count - 1);
            Vector2 deltaToFirstObject = -distance.Multiply(pivot);
            Vector2 currentObjectPosition = deltaToFirstObject + index * delta;
            return currentObjectPosition + position;
        }
    }
}
