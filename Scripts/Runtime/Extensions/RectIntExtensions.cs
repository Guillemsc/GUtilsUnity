using UnityEngine;

namespace GUtilsUnity.Extensions
{
    public static class RectIntExtensions
    {
        /// <summary>
        /// <see cref="RectInt"/> with center <see cref="Vector2.zero"/> and size <see cref="Vector2IntExtensions.MaxValue"/>.
        /// </summary>
        public static readonly RectInt OriginMaxSize = FromCenterAndSize(Vector2Int.zero, Vector2IntExtensions.MaxValue);

        /// <summary>
        /// <see cref="RectInt"/> with center <see cref="Vector2.zero"/> and size <see cref="Vector2IntExtensions.MinValue"/>.
        /// </summary>
        public static readonly RectInt OriginMinSize = FromCenterAndSize(Vector2Int.zero, Vector2IntExtensions.MinValue);

        /// <summary>
        /// Creates a <see cref="RectInt"/> with the provided center position and size.
        /// This method is useful because the default <see cref="RectInt"/> constructor expects a lower left position and size.
        /// </summary>
        /// <param name="center">The center position of the rectangle.</param>
        /// <param name="size">The size of the rectangle.</param>
        /// <returns>A <see cref="RectInt"/> with the provided center position and size.</returns>
        public static RectInt FromCenterAndSize(Vector2Int center, Vector2Int size)
        {
            Vector2Int halfSize = size / 2;

            return new RectInt(center - halfSize, size);
        }

        /// <summary>
        /// Encapsulates the given <see cref="Vector2Int"/> so it is contained inside the <see cref="RectInt"/>,
        /// resulting in a new <see cref="RectInt"/>.
        /// The rectangle is expanded to include the vector's coordinates if they are outside the current bounds.
        /// </summary>
        /// <param name="rectInt">The original rectangle.</param>
        /// <param name="vector2Int">The vector to include in the rectangle.</param>
        /// <returns>A new <see cref="RectInt"/> that represents the encapsulated rectangle.</returns>
        public static RectInt Encapsulate(this RectInt rectInt, Vector2Int vector2Int)
        {
            int xMin = Mathf.Min(rectInt.xMin, vector2Int.x);
            int yMin = Mathf.Min(rectInt.yMin, vector2Int.y);
            int xMax = Mathf.Max(rectInt.xMax, vector2Int.x);
            int yMax = Mathf.Max(rectInt.yMax, vector2Int.y);

            int width = xMax - xMin;
            int height = yMax - yMin;

            return new RectInt(xMin, yMin, width, height);
        }

        /// <summary>
        /// Calculates the offset required to contain a <see cref="RectInt"/> within another <see cref="RectInt"/>.
        /// If the <paramref name="container"/> is smaller than the <paramref name="contained"/>,
        /// the behaviour is undefined.
        /// </summary>
        /// <param name="container">The container <see cref="RectInt"/>.</param>
        /// <param name="contained">The contained <see cref="RectInt"/>.</param>
        /// <returns>The offset required to contain the <paramref name="contained"/>
        /// A <see cref="Vector2Int"/> representing the offset needed to move the <paramref name="contained"/>
        /// rectangle to be fully contained within the <paramref name="container"/>.
        /// If the <paramref name="contained"/> is already inside the <paramref name="container"/>, returns Vector2Int.zero;
        /// </returns>
        public static Vector2Int GetOffsetToBeContainedInsideContainer(this RectInt container, RectInt contained)
        {
            Vector2Int offset = Vector2Int.zero;

            int leftOffset = Mathf.Max(container.xMin - contained.xMin, 0);
            offset.x += leftOffset;

            int rightOffset = Mathf.Max(contained.xMax - container.xMax, 0);
            offset.x -= rightOffset;

            int downOffset = Mathf.Max(container.yMin - contained.yMin, 0);
            offset.y += downOffset;

            int upOffset = Mathf.Max(contained.yMax - container.yMax, 0);
            offset.y -= upOffset;

            return offset;
        }
    }
}
