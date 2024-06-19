using UnityEngine;

namespace GUtilsUnity.Extensions
{
    public static class ElementExtensions
    {
        /// <summary>
        /// From a series of elements, positioned one next to each other, get the element index of a normalized position.
        /// </summary>
        /// <param name="elementCount">The number of elements.</param>
        /// <param name="normalizedPosition">The normalized position of the element to get.</param>
        /// <returns>Element index.</returns>
        public static int GetElementFromNormalizedPosition(
            int elementCount,
            float normalizedPosition
            )
        {
            if (elementCount <= 0)
            {
                return 0;
            }

            float delta = 1f / (elementCount - 1);
            float clampedNormalizedPosition = Mathf.Clamp01(normalizedPosition);
            float result = clampedNormalizedPosition / delta + 0.5f;

            return (int)result;
        }


        /// <summary>
        /// Gets the center normalized position of an element from a series of elements positioned one next to each other.
        /// </summary>
        /// <param name="elementCount">The number of elements.</param>
        /// <param name="elementIndex">The element index to get the normalized position.</param>
        /// <returns>Normalized position.</returns>
        public static float GetNormalizedPositionCenterOfElement(
            int elementCount,
            int elementIndex
            )
        {
            if (elementCount <= 0)
            {
                return 0;
            }

            float delta = 1f / (elementCount - 1);
            float result = delta * elementIndex;

            return result;
        }
    }
}
