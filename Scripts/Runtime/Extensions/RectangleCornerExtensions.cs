using GUtils.Extensions;
using GUtilsUnity.Ui;
using UnityEngine;

namespace GUtilsUnity.Extensions
{
    public static class RectangleCornerExtensions
    {
        /// <summary>
        /// Extension method that converts a <see cref="RectangleCorner"/> enum value into a <see cref="Vector2"/> direction vector.
        /// </summary>
        /// <param name="rectangleCorner">The corner of the rectangle to be converted.</param>
        /// <returns>A <see cref="Vector2"/> direction vector.</returns>
        public static Vector2 ToVector2(this RectangleCorner rectangleCorner)
        {
            var isRight = rectangleCorner is RectangleCorner.LowerRight or RectangleCorner.UpperRight;
            var isUp = rectangleCorner is RectangleCorner.UpperLeft or RectangleCorner.UpperRight;

            return new Vector2(
                isRight.ToPositiveNegativeOneInt(),
                isUp.ToPositiveNegativeOneInt()
            );
        }
    }
}
