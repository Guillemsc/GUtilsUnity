using System;
using UnityEngine;

namespace GUtilsUnity.Extensions
{
    public static class FloatExtensions
    {
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
    }
}
