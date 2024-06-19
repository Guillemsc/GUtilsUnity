using System;
using GUtilsUnity.Data;
using GUtilsUnity.Directions;
using UnityEngine;

namespace GUtilsUnity.Extensions
{
    public static class RectExtensions
    {
        [Obsolete("Use OriginMaxSize instead")]
        public static Rect PositiveInfinity => OriginMinSize;

        [Obsolete("Use OriginMinSize instead")]
        public static Rect NegativeInfinity => OriginMinSize;

        /// <summary>
        /// <see cref="Rect"/> with center <see cref="Vector2.zero"/> and size <see cref="Vector2Extensions.MaxValue"/>.
        /// </summary>
        public static readonly Rect OriginMaxSize = FromCenterAndSize(Vector2.zero, Vector2Extensions.MaxValue);

        /// <summary>
        /// <see cref="Rect"/> with center <see cref="Vector2.zero"/> and size <see cref="Vector2Extensions.MinValue"/>.
        /// </summary>
        public static readonly Rect OriginMinSize = FromCenterAndSize(Vector2.zero, Vector2Extensions.MinValue);

        /// <summary>
        /// Creates a <see cref="Rect"/> with the provided center position and size.
        /// This method is useful because the default <see cref="Rect"/> constructor expects a lower left position and size.
        /// </summary>
        /// <param name="center">The center position of the rectangle.</param>
        /// <param name="size">The size of the rectangle.</param>
        /// <returns>A <see cref="Rect"/> with the provided center position and size.</returns>
        public static Rect FromCenterAndSize(Vector2 center, Vector2 size)
        {
            Vector2 halfSize = size * 0.5f;

            return new Rect(center - halfSize, size);
        }

        /// <summary>
        /// Sets the size of the <see cref="Rect"/> while keeping the center position.
        /// This method is useful because if you change the size of the <see cref="Rect"/> directly, the center position changes as well.
        /// </summary>
        /// <param name="rect">The <see cref="Rect"/> to modify.</param>
        /// <param name="size">The new size of the <see cref="Rect"/>.</param>
        public static void SetSizeKeepingCenterPosition(this ref Rect rect, Vector2 size)
        {
            Vector2 center = rect.center;
            rect.size = size;
            rect.center = center;
        }

        /// <summary>
        /// Combines two <see cref="Rect"/>s together by finding the smallest rectangle that contains both.
        /// </summary>
        /// <param name="rect1">The first <see cref="Rect"/> to combine.</param>
        /// <param name="rect2">The second <see cref="Rect"/> to combine.</param>
        /// <returns>A new <see cref="Rect"/> that contains both <paramref name="rect1"/> and <paramref name="rect2"/>.</returns>
        public static Rect CombineRect(this Rect rect1, Rect rect2)
        {
            float xMin = Mathf.Min(rect1.xMin, rect2.xMin);
            float yMin = Mathf.Min(rect1.yMin, rect2.yMin);
            float xMax = Mathf.Max(rect1.xMax, rect2.xMax);
            float yMax = Mathf.Max(rect1.yMax, rect2.yMax);

            float width = xMax - xMin;
            float height = yMax - yMin;

            return new Rect(xMin, yMin, width, height);
        }

        /// <summary>
        /// Calculates the offset required to contain a <see cref="Rect"/> within another <see cref="Rect"/>.
        /// If the <paramref name="container"/> is smaller than the <paramref name="contained"/>,
        /// the behaviour is undefined.
        /// </summary>
        /// <param name="container">The container <see cref="Rect"/>.</param>
        /// <param name="contained">The contained <see cref="Rect"/>.</param>
        /// <returns>
        /// A <see cref="Vector2"/> representing the offset needed to move the <paramref name="contained"/>
        /// rectangle to be fully contained within the <paramref name="container"/>.
        /// If the <paramref name="contained"/> is already inside the <paramref name="container"/>, returns Vector2.zero;
        /// </returns>
        public static Vector2 GetOffsetToBeContainedInsideContainer(this Rect container, Rect contained)
        {
            Vector2 offset = Vector2.zero;

            float leftOffset = Mathf.Max(container.xMin - contained.xMin, 0);
            offset.x += leftOffset;

            float rightOffset = Mathf.Max(contained.xMax - container.xMax, 0);
            offset.x -= rightOffset;

            float downOffset = Mathf.Max(container.yMin - contained.yMin, 0);
            offset.y += downOffset;

            float upOffset = Mathf.Max(contained.yMax - container.yMax, 0);
            offset.y -= upOffset;

            return offset;
        }

        /// <summary>
        /// Calculates the offset needed to place one rectangle outside of another rectangle in the specified direction.
        /// </summary>
        /// <param name="toMoveRect">The rectangle that needs to be moved.</param>
        /// <param name="referenceRect">The rectangle that the other rectangle needs to be placed outside of.</param>
        /// <param name="direction">The direction in which the rectangle should be moved.</param>
        /// <param name="distance">The distance to move the rectangle.</param>
        /// <returns>A Vector2 representing the calculated offset.</returns>
        public static Vector2 GetOffsetToBePlacedOutsideRect(this Rect referenceRect, Rect toMoveRect, CardinalDirection direction, float distance)
        {
            var offset = direction switch
            {
                CardinalDirection.Up => new Vector2(0, referenceRect.yMax - toMoveRect.yMin + distance),
                CardinalDirection.Down => new Vector2(0, referenceRect.yMin - toMoveRect.yMax - distance),
                CardinalDirection.Left => new Vector2(referenceRect.xMin - toMoveRect.xMax - distance, 0),
                CardinalDirection.Right => new Vector2(referenceRect.xMax - toMoveRect.xMin + distance, 0),
                _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
            };

            return offset;
        }

        /// <summary>
        /// Calculates the margins between a rectangle contained inside another rectangle.
        /// </summary>
        /// <param name="container">The container rectangle.</param>
        /// <param name="contained">The contained rectangle.</param>
        /// <returns>
        /// <see cref="Margins"/> The calculated margins.
        /// If the contained rect sides are inside the container rect, the margins
        /// will be positive. If the contained rect sides are outside the container rect, the
        /// margins will be negative.
        /// </returns>
        public static Margins GetMarginsFromContainedInsideContainer(
            this Rect container,
            Rect contained
            )
        {
            float left = contained.xMin - container.xMin;
            float right = container.xMax - contained.xMax;
            float bottom = contained.yMin - container.yMin;
            float top = container.yMax - contained.yMax;

            return new Margins(left, right, top, bottom);
        }

        /// <summary>
        /// Returns <see cref="Rect"/> a <see cref="Vector2"/> with x: <see cref="Rect.xMin"/> and y: <see cref="Rect.yMax"/>.
        /// </summary>
        public static Vector2 GetUpperLeft(this Rect rect) => new(rect.xMin, rect.yMax);

        /// <summary>
        /// Returns <see cref="Rect"/> a <see cref="Vector2"/> with x: <see cref="Rect.xMax"/> and y: <see cref="Rect.yMax"/>.
        /// </summary>
        public static Vector2 GetUpperRight(this Rect rect) => new(rect.xMax, rect.yMax);

        /// <summary>
        /// Returns <see cref="Rect"/> a <see cref="Vector2"/> with x: <see cref="Rect.xMin"/> and y: <see cref="Rect.yMin"/>.
        /// </summary>
        public static Vector2 GetLowerLeft(this Rect rect) => new(rect.xMin, rect.yMin);

        /// <summary>
        /// Returns <see cref="Rect"/> a <see cref="Vector2"/> with x: <see cref="Rect.xMax"/> and y: <see cref="Rect.yMin"/>.
        /// </summary>
        public static Vector2 GetLowerRight(this Rect rect) => new(rect.xMax, rect.yMin);
    }
}
