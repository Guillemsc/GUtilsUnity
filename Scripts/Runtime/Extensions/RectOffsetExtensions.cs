using UnityEngine;

namespace GUtilsUnity.Extensions
{
    public static class RectOffsetExtensions
    {
        /// <summary>
        /// Adds the values of another <see cref="RectOffset"/> to this one.
        /// </summary>
        /// <param name="rectOffset">The original <see cref="RectOffset"/>.</param>
        /// <param name="otherRectOffset">The <see cref="RectOffset"/> to add.</param>
        public static void Add(
            this RectOffset rectOffset,
            RectOffset otherRectOffset)
        {
            rectOffset.top += otherRectOffset.top;
            rectOffset.bottom += otherRectOffset.bottom;
            rectOffset.left += otherRectOffset.left;
            rectOffset.right += otherRectOffset.right;
        }

        /// <summary>
        /// Subtracts the values of another <see cref="RectOffset"/> from this one.
        /// </summary>
        /// <param name="rectOffset">The original <see cref="RectOffset"/>.</param>
        /// <param name="otherRectOffset">The <see cref="RectOffset"/> to subtract.</param>
        public static void Remove(
            this RectOffset rectOffset,
            RectOffset otherRectOffset)
        {
            rectOffset.top -= otherRectOffset.top;
            rectOffset.bottom -= otherRectOffset.bottom;
            rectOffset.left -= otherRectOffset.left;
            rectOffset.right -= otherRectOffset.right;
        }
    }
}
