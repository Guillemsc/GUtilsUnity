using System;
using GUtilsUnity.Attributes;
using GUtilsUnity.Tweening.Configuration;
using UnityEngine;
using UnityEngine.UI;

namespace GUtilsUnity.Extensions
{
    public static class ScrollRectExtensions
    {
        /// <summary>
        /// Scrolls the given ScrollRect component to the top, by setting <see cref="ScrollRect.verticalNormalizedPosition"/> to 1.
        /// </summary>
        public static void ScrollToTheTop(this ScrollRect scrollRect)
        {
            scrollRect.verticalNormalizedPosition = 1f;
        }

        /// <summary>
        /// Scrolls the given ScrollRect component to the bottom, by setting <see cref="ScrollRect.verticalNormalizedPosition"/> to 0.
        /// </summary>
        public static void ScrollToTheBottom(this ScrollRect scrollRect)
        {
            scrollRect.verticalNormalizedPosition = 0f;
        }

        /// <summary>
        /// Scrolls the given ScrollRect component to the left, by setting <see cref="ScrollRect.verticalNormalizedPosition"/> to 0.
        /// </summary>
        public static void ScrollToTheLeft(this ScrollRect scrollRect)
        {
            scrollRect.horizontalNormalizedPosition = 0f;
        }

        /// <summary>
        /// Scrolls the given ScrollRect component to the right, by setting <see cref="ScrollRect.verticalNormalizedPosition"/> to 1.
        /// </summary>
        public static void ScrollToTheRight(this ScrollRect scrollRect)
        {
            scrollRect.horizontalNormalizedPosition = 1f;
        }

        [Obsolete("This method is obsolete. Use GetElementScrollNormalizedVerticalPosition instead")]
        public static float GetElementNormalizedVerticalPosition(
            this ScrollRect scrollRect,
            RectTransform rectTransform,
            float normalizedViewportPosition = 0.5f
            )
        {
            return scrollRect.GetElementScrollNormalizedVerticalPosition(rectTransform, normalizedViewportPosition);
        }

        /// <summary>
        /// Get the normalized vertical position of an element in the ScrollRect.
        /// </summary>
        /// <param name="scrollRect">The ScrollRect where the RectTransform is located.</param>
        /// <param name="rectTransform">The RectTransform of the element to get its normalized position.</param>
        /// <param name="normalizedViewportPosition">The normalized position in the viewport. Where 0 is the bottom of the viewport and 1 is the top.</param>
        /// <returns>
        /// The normalized vertical position of the element. This will be a value between 0 and 1, where 0 represents the bottom
        /// of the ScrollRect and 1 represents the top.
        /// </returns>
        /// <remarks>
        /// This method calculates the normalized position of an element by finding its position in the ScrollRect's content
        /// and adjusting based on the specified viewport position. This can be used, for instance, to programmatically
        /// scroll the ScrollRect such that a particular element is visible in a specific position of the viewport.
        /// </remarks>
        /// <example>
        /// <code>
        /// ScrollRect scrollRect = ...;
        /// RectTransform element = ...;
        /// float normalizedPosition = scrollRect.GetElementNormalizedVerticalPosition(element, 0.75f);
        /// // The ScrollRect can now be scrolled to this normalized position to place the element 75% of the way down the viewport.
        /// scrollRect.verticalNormalizedPosition = normalizedPosition;
        /// </code>
        /// </example>
        public static float GetElementScrollNormalizedVerticalPosition(
            this ScrollRect scrollRect,
            RectTransform rectTransform,
            float normalizedViewportPosition = 0.5f
            )
        {
            float scrollableHeight = scrollRect.GetScrollableHeight();
            Vector2 localContentPosition = scrollRect.content.InverseTransformPoint(rectTransform.position);
            var rect = scrollRect.content.rect;
            float contentMin = rect.yMin;
            float scrollableMax = contentMin + scrollableHeight;

            float viewportDisplacement = scrollRect.viewport.rect.height * normalizedViewportPosition;
            float desiredPosition = localContentPosition.y - viewportDisplacement;

            float factor = GUtils.Extensions.MathExtensions.GetNormalizedFactor(desiredPosition, contentMin, scrollableMax);

            return factor;
        }

        [Obsolete("This method is obsolete. Use GetElementScrollNormalizedHorizontalPosition instead")]
        public static float GetElementNormalizedHorizontalPosition(
            this ScrollRect scrollRect,
            RectTransform rectTransform,
            float normalizedViewportPosition = 0.5f
            )
        {
            return scrollRect.GetElementScrollNormalizedHorizontalPosition(rectTransform, normalizedViewportPosition);
        }

        /// <summary>
        /// Get the normalized horizontal position of an element in the ScrollRect.
        /// </summary>
        /// <param name="scrollRect">The ScrollRect where the RectTransform is located.</param>
        /// <param name="rectTransform">The RectTransform of the element to get its normalized position.</param>
        /// <param name="normalizedViewportPosition">The normalized position in the viewport. Where 0 is the left of the viewport and 1 is the right.</param>
        /// <returns>
        /// The normalized horizontal position of the element. This will be a value between 0 and 1, where 0 represents the left
        /// of the ScrollRect and 1 represents the right.
        /// </returns>
        /// <remarks>
        /// This method calculates the normalized position of an element by finding its position in the ScrollRect's content
        /// and adjusting based on the specified viewport position. This can be used, for instance, to programmatically
        /// scroll the ScrollRect such that a particular element is visible in a specific position of the viewport.
        /// </remarks>
        /// <example>
        /// <code>
        /// ScrollRect scrollRect = ...;
        /// RectTransform element = ...;
        /// float normalizedPosition = scrollRect.GetElementNormalizedHorizontalPosition(element, 0.75f);
        /// // The ScrollRect can now be scrolled to this normalized position to place the element 75% of the way down the viewport.
        /// scrollRect.horizontalNormalizedPosition = normalizedPosition;
        /// </code>
        /// </example>
        public static float GetElementScrollNormalizedHorizontalPosition(
            this ScrollRect scrollRect,
            RectTransform rectTransform,
            float normalizedViewportPosition = 0.5f
            )
        {
            float scrollableWidth = scrollRect.GetScrollableWidth();
            Vector2 localContentPosition = scrollRect.content.InverseTransformPoint(rectTransform.position);
            var rect = scrollRect.content.rect;
            float contentMin = rect.xMin;
            float scrollableMax = contentMin + scrollableWidth;

            float viewportDisplacement = scrollRect.viewport.rect.width * normalizedViewportPosition;
            float desiredPosition = localContentPosition.x - viewportDisplacement;

            float factor = GUtils.Extensions.MathExtensions.GetNormalizedFactor(desiredPosition, contentMin, scrollableMax);

            return factor;
        }

        [Obsolete("This method is obsolete. Use GetElementScrollNormalizedPosition instead")]
        public static Vector2 GetElementNormalizedPosition(
            this ScrollRect scrollRect,
            RectTransform rectTransform,
            Vector2 normalizedViewportPosition
            )
        {
            return scrollRect.GetElementScrollNormalizedPosition(rectTransform, normalizedViewportPosition);
        }

                /// <summary>
        /// Get the normalized position of an element in the ScrollRect.
        /// </summary>
        /// <param name="scrollRect">The ScrollRect where the RectTransform is located.</param>
        /// <param name="rectTransform">The RectTransform of the element to get its normalized position.</param>
        /// <param name="normalizedViewportPosition">The normalized position in the viewport.
        /// On the x axis 0 is the left of the viewport and 1 is the right.
        /// On the y axis 0 is the bottom of the viewport and 1 is the top
        /// </param>
        /// <returns>
        /// The normalized position of the element. This will be a Vector2 with components in range of 0 and 1, where 0
        /// </returns>
        /// <remarks>
        /// This method calculates the normalized position of an element by finding its position in the ScrollRect's content
        /// and adjusting based on the specified viewport position. This can be used, for instance, to programmatically
        /// scroll the ScrollRect such that a particular element is visible in a specific position of the viewport.
        /// </remarks>
        /// <example>
        /// <code>
        /// ScrollRect scrollRect = ...;
        /// RectTransform element = ...;
        /// float normalizedPosition = scrollRect.GetElementNormalizedHorizontalPosition(element, new Vector2(0.75f, 0.75%));
        /// // The ScrollRect can now be scrolled to this normalized position to place the element 75% of the way down the viewport.
        /// scrollRect.horizontalNormalizedPosition = normalizedPosition;
        /// </code>
        /// </example>
        public static Vector2 GetElementScrollNormalizedPosition(
            this ScrollRect scrollRect,
            RectTransform rectTransform,
            Vector2 normalizedViewportPosition
            )
        {
            float horizontal = GetElementScrollNormalizedHorizontalPosition(scrollRect, rectTransform, normalizedViewportPosition.x);
            float vertical = GetElementScrollNormalizedVerticalPosition(scrollRect, rectTransform, normalizedViewportPosition.y);
            return new Vector2(horizontal, vertical);
        }

        /// <summary>
        /// The scrollable part that you can set in range of [0,1] of a scroll rect is not the same as the viewport size
        /// Use this extension to calculate this part to offset your calculations
        /// </summary>
        public static Vector2 GetScrollableSize(this ScrollRect scrollRect)
        {
            Vector2 viewPortSize = scrollRect.viewport.rect.size;
            Vector2 scrollSize = scrollRect.content.rect.size;
            return Vector2.Max(Vector2.zero, scrollSize - viewPortSize);
        }

        /// <summary>
        /// The scrollable part that you can set in range of [0,1] of a scroll rect is not the same as the viewport size
        /// Use this extension to calculate this part to offset your calculations
        /// </summary>
        public static float GetScrollableWidth(this ScrollRect scrollRect)
        {
            var viewPortSize = scrollRect.viewport.rect.width;
            var scrollSize = scrollRect.content.rect.width;
            return Mathf.Max(0f, scrollSize - viewPortSize);
        }

        /// <summary>
        /// The scrollable part that you can set in range of [0,1] of a scroll rect is not the same as the viewport size
        /// Use this extension to calculate this part to offset your calculations
        /// </summary>
        public static float GetScrollableHeight(this ScrollRect scrollRect)
        {
            var viewPortSize = scrollRect.viewport.rect.height;
            var scrollSize = scrollRect.content.rect.height;
            return Mathf.Max(0f, scrollSize - viewPortSize);
        }
    }
}
