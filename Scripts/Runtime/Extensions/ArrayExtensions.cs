using System;
using UnityEngine;

namespace GUtilsUnity.Extensions
{
    public static class ArrayExtensions
    {
        /// <summary>
        /// Calculates the flattened index of a 2d array, based on the provided x and y coordinates and the width of the array.
        /// </summary>
        /// <param name="x">The x coordinate of the element in the 2d array.</param>
        /// <param name="y">The y coordinate of the element in the 2d array.</param>
        /// <param name="arrayWidth">The width of the 2d array.</param>
        public static int GetFlattened2dArrayIndex(int x, int y, int arrayWidth)
            => (arrayWidth * y) + x;

        /// <summary>
        /// Calculates the flattened index of a 2d array, based on the provided x and y coordinates and the width of the array.
        /// </summary>
        /// <param name="position">The x and y coordinate of the element in the 2d array.</param>
        /// <param name="arrayWidth">The width of the 2d array.</param>
        public static int GetFlattened2dArrayIndex(Vector2Int position, int arrayWidth)
        {
            return GetFlattened2dArrayIndex(position.x, position.y, arrayWidth);
        }

        /// <summary>
        /// Gets the element corresponding to the flattened index of a 2d array, based on the provided
        /// x and y coordinates and the width of the array.
        /// </summary>
        /// <param name="array">Array to get the element from.</param>
        /// <param name="position">The x and y coordinate of the element in the 2d array.</param>
        /// <param name="arrayWidth">The width of the 2d array.</param>
        public static T GetFlattened2dArrayElement<T>(this T[] array, Vector2Int position, int arrayWidth)
        {
            int index = GetFlattened2dArrayIndex(position, arrayWidth);
            return array[index];
        }

        /// <summary>
        /// Tries to get the element corresponding to the flattened index of a 2d array, based on the provided
        /// x and y coordinates and the width of the array.
        /// </summary>
        /// <param name="array">Array to get the element from.</param>
        /// <param name="position">The x and y coordinate of the element in the 2d array.</param>
        /// <param name="arrayWidth">The width of the 2d array.</param>
        public static bool TryGetFlattened2dArrayElement<T>(this T[] array, Vector2Int position, int arrayWidth, out T element)
        {
            int index = GetFlattened2dArrayIndex(position, arrayWidth);
            return array.TryGet(index, out element);
        }

        [Obsolete("This method is obsolete. Use GetFlattened2dArrayElement instead")]
        public static T GetFlattenedArrayElement<T>(this T[] array, Vector2Int position, Vector2Int size)
        {
            return array.GetFlattened2dArrayElement(position, size.x);
        }

        [Obsolete("This method is obsolete. Use TryGetFlattened2dArrayElement instead")]
        public static bool TryGetFlattenedArrayElement<T>(this T[] array, Vector2Int position, Vector2Int size, out T element)
        {
            return array.TryGetFlattened2dArrayElement(position, size.x, out element);
        }

        /// <summary>
        /// Tries to get the element corresponding to the provided index.
        /// If index is out of bounds, it returns false.
        /// If array is empty it returns false.
        /// </summary>
        public static bool TryGet<T>(this T[] array, int index, out T item)
        {
            bool outsideBounds = index < 0 || array.Length <= index;

            if (outsideBounds)
            {
                item = default;
                return false;
            }

            item = array[index];
            return true;
        }

        /// <summary>
        /// Clamps the index between 0 and the array lenght.
        /// </summary>
        public static int ClampIndex<T>(this T[] array, int index)
        {
            return Mathf.Clamp(index, 0, array.Length - 1);
        }

        /// <summary>
        /// Tries to get the element corresponding to the provided index.
        /// If index is out of bounds, it clamps it between 0 and the array lenght.
        /// If array is empty it returns false.
        /// </summary>
        public static bool TryGetClamped<T>(this T[] array, int index, out T item)
        {
            index = array.ClampIndex(index);

            return array.TryGet(index, out item);
        }

        /// <summary>
        /// Adds 1 to the provided index. If resulting index goes over array lenght, returns 0.
        /// </summary>
        public static int GetNextOrSmallestIndex<T>(this T[] array, int index)
        {
            int newIndex = index + 1;

            if (newIndex >= array.Length)
            {
                newIndex = 0;
            }

            return newIndex;
        }

        /// <summary>
        /// Substracts 1 to the provided index. If resulting index is smaller than zero, returns largest array index.
        /// </summary>
        public static int GetPreviousOrLargestIndex<T>(this T[] array, int index)
        {
            int newIndex = index - 1;

            if (newIndex < 0)
            {
                newIndex = array.Length - 1;
            }

            return newIndex;
        }

        /// <summary>
        /// Determines whether an element is in the array.
        /// </summary>
        public static bool Contains<T>(this T[] array, T value)
        {
            foreach (T element in array)
            {
                if (element.Equals(value))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Performs the specified action on each element of the array.
        /// </summary>
        public static void ForEach<T>(this T[] array, Action<T> action)
        {
            Array.ForEach(array, action);
        }
    }
}
