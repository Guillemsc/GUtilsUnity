using System;
using System.Collections.Generic;
using UnityEngine;

namespace GUtilsUnity.Extensions
{
    public static class Vector2IntExtensions
    {
        /// <summary>
        /// Equivalent to x:<see cref="int.MaxValue"/> y:<see cref="int.MaxValue"/>.
        /// </summary>
        public static readonly Vector2Int MaxValue = new(int.MaxValue, int.MaxValue);

        /// <summary>
        /// Equivalent to x:<see cref="int.MinValue"/> y:<see cref="int.MinValue"/>.
        /// </summary>
        public static readonly Vector2Int MinValue = new(int.MinValue, int.MinValue);

        /// <summary>
        /// Enumerates all positions within a matrix defined by the dimensions of the input Vector2Int.
        /// </summary>
        /// <param name="vector">The input Vector2Int representing the dimensions of the matrix.</param>
        /// <returns>An IEnumerable of Vector2Int containing all positions within the matrix.</returns>
        /// <example>(1, 3) -> [(0, 0), (0, 1), (0, 2), (1, 0), (1, 1), (1, 2)]</example>
        public static IEnumerable<Vector2Int> EnumerateMatrixPositions(this Vector2Int vector)
        {
            for (int y = 0; y < vector.y; y++)
            {
                for (int x = 0; x < vector.x; x++)
                {
                    yield return new Vector2Int(x, y);
                }
            }
        }

        /// <summary>
        /// Checks if the input Vector2Int value is within the specified bounds.
        /// </summary>
        /// <param name="value">The input Vector2Int value to check.</param>
        /// <param name="min">The minimum bounds Vector2Int.</param>
        /// <param name="max">The maximum bounds Vector2Int.</param>
        /// <returns>True if the value is within the bounds, false otherwise.</returns>
        public static bool IsInBounds(this Vector2Int value, Vector2Int min, Vector2Int max)
        {
            return value.x >= min.x &&
                   value.x <= max.x &&
                   value.y >= min.y &&
                   value.y <= max.y;
        }

        /// <summary>
        /// Returns a Vector2Int that represents the input vector rotated 90 degrees clockwise.
        /// </summary>
        /// <param name="vector">The input Vector2Int to rotate.</param>
        /// <returns>A Vector2Int representing the rotated vector.</returns>
        public static Vector2Int PerpendicularClockwise(this Vector2Int vector)
            => new(vector.y, -vector.x);

        /// <summary>
        /// Returns a Vector2Int that represents the input vector rotated 90 degrees counter-clockwise.
        /// </summary>
        /// <param name="vector">The input Vector2Int to rotate.</param>
        /// <returns>A Vector2Int representing the rotated vector.</returns>
        public static Vector2Int PerpendicularCounterClockwise(this Vector2Int vector)
            => new(-vector.y, vector.x);

        /// <summary>
        /// Returns a normalized Vector2Int representing the input vector.
        /// </summary>
        /// <param name="vector">The input Vector2Int to normalize.</param>
        /// <returns>A normalized Vector2Int.</returns>
        /// <example>[(1, 3) -> (1, 1)] [(0, 3) -> (0, 1)] [(5, -3) -> (1, -1)]</example>
        public static Vector2Int Normalized(this Vector2Int vector)
            => new(Math.Sign(vector.x), Math.Sign(vector.y));

        /// <summary>
        /// Returns a Vector2Int with the X and Y axes swapped.
        /// </summary>
        /// <param name="vector">The input Vector2Int to swap axes.</param>
        /// <returns>A Vector2Int with the axes swapped.</returns>
        public static Vector2Int SwapAxis(this Vector2Int vector)
            => new(vector.y, vector.x);

        /// <summary>
        /// Converts the input Vector2Int to a Vector2.
        /// </summary>
        /// <param name="vector">The input Vector2Int to convert.</param>
        /// <returns>A Vector2 representation of the input vector.</returns>
        public static Vector2 ToVector2(this Vector2Int vector)
            => new(vector.x, vector.y);

        /// <summary>
        /// Converts the input Vector2Int to a Vector3, with optional Z component.
        /// </summary>
        /// <param name="vector">The input Vector2Int to convert.</param>
        /// <param name="z">The optional Z component value (default: 0).</param>
        /// <returns>A Vector3 with the X and Y components from the input vector and the specified Z component.</returns>
        public static Vector3 ToVector3XY(this Vector2Int vector, float z = 0f)
            => new(vector.x, vector.y, z);

        /// <summary>
        /// Converts the input Vector2Int to a Vector3, with optional Y component.
        /// </summary>
        /// <param name="vector2Int">The input Vector2Int to convert.</param>
        /// <param name="y">The optional Y component value (default: 0).</param>
        /// <returns>A Vector3 with the X and Z components from the input vector and the specified Y component.</returns>
        public static Vector3 ToVector3XZ(this Vector2Int vector2Int, float y = 0f)
            => new(vector2Int.x, y, vector2Int.y);

        /// <summary>
        /// Converts the input Vector2Int to a Vector3, with optional X component.
        /// </summary>
        /// <param name="vector2Int">The input Vector2Int to convert.</param>
        /// <param name="x">The optional X component value (default: 0).</param>
        /// <returns>A Vector3 with the Y and Z components from the input vector and the specified X component.</returns>
        public static Vector3 ToVector3YZ(this Vector2Int vector2Int, float x = 0f)
            => new(x, vector2Int.y, vector2Int.y);

        /// <summary>
        /// Converts the input Vector2Int to a Vector3Int, with optional Z component.
        /// </summary>
        /// <param name="vector">The input Vector2Int to convert.</param>
        /// <param name="z">The optional Z component value (default: 0).</param>
        /// <returns>A Vector3Int with the X and Y components preserved from the input vector and the specified Z component.</returns>
        public static Vector3Int ToVector3IntXY(this Vector2Int vector, int z = 0)
            => new(vector.x, vector.y, z);

        /// <summary>
        /// Converts the input Vector2Int to a Vector3Int, with optional Y component.
        /// </summary>
        /// <param name="vector">The input Vector2Int to convert.</param>
        /// <param name="y">The optional Y component value (default: 0).</param>
        /// <returns>A Vector3Int with the X and Z components from the input vector and the specified Y component.</returns>
        public static Vector3Int ToVector3IntXZ(this Vector2Int vector, int y = 0)
            => new(vector.x, y, vector.y);

        /// <summary>
        /// Converts the input Vector2Int to a Vector3Int, with optional X component.
        /// </summary>
        /// <param name="vector">The input Vector2Int to convert.</param>
        /// <param name="x">The optional X component value (default: 0).</param>
        /// <returns>A Vector3Int with the X, Y, and Z components from the input vector and the specified X component.</returns>
        public static Vector3Int ToVector3IntYZ(this Vector2Int vector, int x = 0)
            => new(x, vector.x, vector.y);

        /// <summary>
        /// Returns a Vector2Int that represents the absolute values of the input vector's (x, y) components.
        /// </summary>
        /// <param name="vector">The input Vector2Int.</param>
        /// <returns>A Vector2Int with the absolute values of the input vector's components.</returns>
        public static Vector2Int Abs(this Vector2Int vector)
            => new(vector.x.Abs(), vector.y.Abs());

        /// <summary>
        /// Calculates the Manhattan distance between two Vector2Int points.
        /// </summary>
        /// <param name="a">The first Vector2Int point.</param>
        /// <param name="b">The second Vector2Int point.</param>
        /// <returns>The Manhattan distance between the two points.</returns>
        /// <remarks>Manhattan distance is calculated by
        /// taking the sum of absoulte distances between the x and y coordinates.</remarks>
        public static int ManhattanDistance(this Vector2Int a, Vector2Int b)
            => (a.x - b.x).Abs() + (a.y - b.y).Abs();
    }
}
