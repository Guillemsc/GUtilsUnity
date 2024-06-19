using UnityEngine;

namespace GUtilsUnity.Extensions
{
    public static class BoundsExtensions
    {
        /// <summary>
        /// Bounds with zero as center, and <see cref="Vector3.positiveInfinity"/> as size;
        /// </summary>
        public static readonly Bounds PositiveInfinity = new(Vector3.zero, Vector3.positiveInfinity);

        /// <summary>
        /// Bounds with zero as center, and <see cref="Vector3.negativeInfinity"/> as size;
        /// </summary>
        public static readonly Bounds NegativeInfinity = new(Vector3.zero, Vector3.negativeInfinity);

        /// <summary>
        /// Returns the bounds x and y values as a RectInt.
        /// It is unsafe because it will lose all the float precision when converting the values to int.
        /// </summary>
        public static RectInt ToRectIntXYUnsafe(this Bounds bounds)
        {
            Vector3 position = bounds.min;
            Vector3 size = bounds.size;

            return new RectInt(
                new Vector2Int((int)position.x, (int)position.y),
                new Vector2Int((int)size.x, (int)size.y)
            );
        }

        /// <summary>
        /// Returns the bounds x and y values as a Rect.
        /// </summary>
        public static Rect ToRectXY(this Bounds bounds)
        {
            Vector3 position = bounds.min;
            Vector3 size = bounds.size;

            return new Rect(
                position.ToVector2XY(),
                size.ToVector2XY()
            );
        }

        /// <summary>
        /// Checks if target bounds are completely inside of this bounds.
        /// </summary>
        public static bool ContainsBounds(this Bounds bounds, Bounds target)
        {
            return bounds.Contains(target.min) && bounds.Contains(target.max);
        }

        /// <summary>
        /// Checks if target bounds are completely or partially inside of this bounds.
        /// </summary>
        public static bool IntersectsBounds(this Bounds bounds, Bounds target)
        {
            return bounds.Contains(target.min) || bounds.Contains(target.max);
        }

        /// <summary>
        /// Calculates the pivot of the bounds.
        /// The resulting Vector3 represents the pivot point of the bounds,
        /// relative to its own local coordinates.
        /// </summary>
        public static Vector3 GetPivot(this Bounds bounds)
        {
            return Vector3Extensions.HalfOne - bounds.center.Divide(bounds.size);
        }
    }
}
