using System;
using UnityEngine;

namespace GUtilsUnity.Extensions
{
    public static class Vector3IntExtensions
    {
        /// <summary>
        /// Equivalent to x:<see cref="int.MaxValue"/> y:<see cref="int.MaxValue"/> z:<see cref="int.MaxValue"/>.
        /// </summary>
        public static readonly Vector3Int MaxValue = new(int.MaxValue, int.MaxValue, int.MaxValue);

        /// <summary>
        /// Equivalent to x:<see cref="int.MaxValue"/> y:<see cref="int.MaxValue"/> z:<see cref="int.MaxValue"/>.
        /// </summary>
        public static readonly Vector3Int MinValue = new(int.MinValue, int.MinValue, int.MinValue);

        /// <summary>
        /// Returns a new Vector3Int with each component (x, y, z) set to the
        /// absolute value of the corresponding.
        /// </summary>
        /// <param name="vector">The input Vector3Int.</param>
        /// <returns>A new Vector3Int with absolute values.</returns>
        public static Vector3Int Abs(this Vector3Int vector)
            => new(vector.x.Abs(), vector.y.Abs(), vector.z.Abs());

        /// <summary>
        /// Swaps the x and z components of a <see cref="Vector3Int"/> object.
        /// </summary>
        /// <param name="value">The input <see cref="Vector3Int"/> object.</param>
        /// <returns>A new <see cref="Vector3Int"/> object with the x and z components swapped.</returns>
        public static Vector3Int SwapXZ(this Vector3Int value)
            => new(value.z, value.y, value.x);

        /// <summary>
        /// Converts a <see cref="Vector3"/> object to a new <see cref="Vector3"/> object with the x and
        /// z components swapped, and the y component preserved.
        /// </summary>
        /// <param name="value">The input <see cref="Vector3"/> object.</param>
        /// <returns>A new <see cref="Vector3"/> object with the x and z components swapped, and the y component preserved.</returns>
        public static Vector3 ToVector3XZY(this Vector3 value)
            => new(value.x, value.z, value.y);

        /// <summary>
        /// For each axis, returns an integer that indicates the sign of a number.
        /// </summary>
        public static Vector3Int Sign(this Vector3Int vector)
            => new(Math.Sign(vector.x), Math.Sign(vector.x), Math.Sign(vector.x));

        /// <summary>
        /// Converts a <see cref="Vector3Int"/> object to a <see cref="Vector3"/> object.
        /// </summary>
        /// <param name="vector">The input <see cref="Vector3Int"/> object.</param>
        /// <returns>A new <see cref="Vector3"/> object with components copied from the input vector.</returns>
        public static Vector3 ToVector3(this Vector3Int vector)
            => new(vector.x, vector.y, vector.z);

        /// <summary>
        /// Converts a <see cref="Vector3Int"/> object to a <see cref="Vector2"/> object, discarding the z component.
        /// </summary>
        /// <param name="vector">The input <see cref="Vector3Int"/> object.</param>
        /// <returns>A new <see cref="Vector2"/> object with the x and y components copied from the input vector.</returns>
        public static Vector2 ToVector2XY(this Vector3Int vector)
            => new(vector.x, vector.y);

        /// <summary>
        /// Converts a <see cref="Vector3Int"/> object to a <see cref="Vector2Int"/> object, discarding the z component.
        /// </summary>
        /// <param name="vector">The input <see cref="Vector3Int"/> object.</param>
        /// <returns>A new <see cref="Vector2Int"/> object with the x and y components copied from the input vector.</returns>
        public static Vector2Int ToVector2IntXY(this Vector3Int vector)
            => new(vector.x, vector.y);

        /// <summary>
        /// Converts a <see cref="Vector3Int"/> object to a <see cref="Vector2"/> object, discarding the y component.
        /// </summary>
        /// <param name="vector">The input <see cref="Vector3Int"/> object.</param>
        /// <returns>A new <see cref="Vector2"/> object with the x and z components copied from the input vector.</returns>
        public static Vector2 ToVector2XZ(this Vector3Int vector)
            => new(vector.x, vector.z);

        /// <summary>
        /// Converts a <see cref="Vector3Int"/> object to a <see cref="Vector2Int"/> object, discarding the y component.
        /// </summary>
        /// <param name="vector">The input <see cref="Vector3Int"/> object.</param>
        /// <returns>A new <see cref="Vector2Int"/> object with the x and z components copied from the input vector.</returns>
        public static Vector2Int ToVector2IntXZ(this Vector3Int vector)
            => new(vector.x, vector.z);

        /// <summary>
        /// Calculates the absolute difference between two <see cref="Vector3Int"/> objects component-wise.
        /// </summary>
        /// <param name="a">The first <see cref="Vector3Int"/> object.</param>
        /// <param name="b">The second <see cref="Vector3Int"/> object.</param>
        /// <returns>A new <see cref="Vector3Int"/> object representing the absolute
        /// difference between the components of the input vectors.</returns>
        public static Vector3Int DistanceAbs(Vector3Int a, Vector3Int b) => (b - a).Abs();

        /// <summary>
        /// Calculates the Manhattan distance between two <see cref="Vector3Int"/> objects.
        /// </summary>
        /// <param name="a">The first <see cref="Vector3Int"/> object.</param>
        /// <param name="b">The second <see cref="Vector3Int"/> object.</param>
        /// <returns>The Manhattan distance between the two input <see cref="Vector3Int"/> objects.</returns>
        /// <remarks>Manhattan distance is calculated by
        /// taking the sum of absoulte distances between the x, the y and the z coordinates.</remarks>
        public static int ManhattanDistance(this Vector3Int a, Vector3Int b)
            => (a.x - b.x).Abs() + (a.y - b.y).Abs() + (a.z - b.z).Abs();
    }
}
