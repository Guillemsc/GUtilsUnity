using UnityEngine;

namespace GUtilsUnity.Extensions
{
    public static class IntExtensions
    {
        /// <summary>
        /// Creates a <see cref="Vector3Int"/> with x, y, z as the value.
        /// </summary>
        public static Vector3Int ToVector3Int(this int value) => new(value, value, value);

        /// <summary>
        /// Creates a <see cref="Vector3"/> with x, y, z as the value.
        /// </summary>
        public static Vector3 ToVector3(this int value) => new(value, value, value);

        /// <summary>
        /// Creates a <see cref="Vector2Int"/> with x, y as the value.
        /// </summary>
        public static Vector2Int ToVector2Int(this int value) => new(value, value);

        /// <summary>
        /// Creates a <see cref="Vector2"/> with x, y as the value.
        /// </summary>
        public static Vector2 ToVector2(this int value) => new(value, value);

        /// <summary>
        /// Creates a <see cref="Vector2"/> with x as the value, and y as the provided y parameter.
        /// </summary>
        public static Vector2 ToVector2X(this int value, int y = 0) => new(value, y);

        /// <summary>
        /// Creates a <see cref="Vector2"/> with y as the value, and x as the provided x parameter.
        /// </summary>
        public static Vector2 ToVector2Y(this int value, int x = 0) => new(x, value);

        /// <summary>
        /// Creates a <see cref="Vector2Int"/> with x as the value, and y as the provided y parameter.
        /// </summary>
        public static Vector2Int ToVector2IntX(this int value, int y = 0) => new(value, y);

        /// <summary>
        /// Creates a <see cref="Vector2Int"/> with y as the value, and x as the provided x parameter.
        /// </summary>
        public static Vector2Int ToVector2IntY(this int value, int x = 0) => new(x, value);
    }
}
