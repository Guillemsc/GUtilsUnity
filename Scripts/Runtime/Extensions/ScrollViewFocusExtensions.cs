using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace GUtilsUnity.Extensions
{
    [Obsolete("Use ScrollRectExtensions")]
    public static class ScrollViewFocusExtensions
    {
        [Obsolete("This method is obsolete. Use ScrollRectExtensions.GetElementNormalizedVerticalPosition")]
        public static Vector2 CalculateFocusedScrollPosition(this ScrollRect scrollView, Vector2 focusPoint)
        {
            Vector2 contentSize = scrollView.content.rect.size;
            Vector2 viewportSize = ((RectTransform)scrollView.content.parent).rect.size;
            Vector2 contentScale = scrollView.content.localScale;

            contentSize.Scale(contentScale);
            focusPoint.Scale(contentScale);

            Vector2 scrollPosition = scrollView.normalizedPosition;
            // if (scrollView.horizontal && contentSize.x > viewportSize.x)
            //     scrollPosition.x = Mathf.Clamp01((focusPoint.x - viewportSize.x * 0.5f) / (contentSize.x - viewportSize.x));
            // if (scrollView.vertical && contentSize.y > viewportSize.y)
            //     scrollPosition.y = Mathf.Clamp01((focusPoint.y - viewportSize.y * 0.5f) / (contentSize.y - viewportSize.y));

            if (scrollView.horizontal)
                scrollPosition.x = Mathf.Clamp01((focusPoint.x - viewportSize.x * 0.5f) / (contentSize.x - viewportSize.x));
            if (scrollView.vertical)
                scrollPosition.y = Mathf.Clamp01((focusPoint.y - viewportSize.y * 0.5f) / (contentSize.y - viewportSize.y));

            return scrollPosition;
        }

        [Obsolete("This method is obsolete. Use ScrollRectExtensions.GetElementNormalizedVerticalPosition")]
        public static Vector2 CalculateFocusedScrollPosition(this ScrollRect scrollView, RectTransform item)
        {
            Vector2 itemCenterPoint = scrollView.content.InverseTransformPoint(item.transform.TransformPoint(item.rect.center));

            Vector2 contentSizeOffset = scrollView.content.rect.size;
            contentSizeOffset.Scale(scrollView.content.pivot);

            var focusedScrollPointPosition=  scrollView.CalculateFocusedScrollPosition(itemCenterPoint + contentSizeOffset);
            return focusedScrollPointPosition;
        }

        [Obsolete("This method is obsolete. Use ScrollRectExtensions.GetElementNormalizedVerticalPosition")]
        public static void FocusAtPoint(this ScrollRect scrollView, Vector2 focusPoint)
        {
            scrollView.normalizedPosition = scrollView.CalculateFocusedScrollPosition(focusPoint);
        }

        [Obsolete("This method is obsolete. Use ScrollRectExtensions.GetElementNormalizedVerticalPosition")]
        public static void FocusOnItem(this ScrollRect scrollView, RectTransform item)
        {
            scrollView.normalizedPosition = scrollView.CalculateFocusedScrollPosition(item);
        }

        [Obsolete("This method is obsolete. Use ScrollRectExtensions.GetElementNormalizedVerticalPosition")]
        static IEnumerator LerpToScrollPositionCoroutine(this ScrollRect scrollView, Vector2 targetNormalizedPos, float speed)
        {
            Vector2 initialNormalizedPos = scrollView.normalizedPosition;

            float t = 0f;
            while (t < 1f)
            {
                scrollView.normalizedPosition = Vector2.LerpUnclamped(
                    initialNormalizedPos,
                    targetNormalizedPos,
                    1f - (1f - t) * (1f - t)
                );

                yield return null;
                t += speed * UnityEngine.Time.unscaledDeltaTime;
            }

            scrollView.normalizedPosition = targetNormalizedPos;
        }

        [Obsolete("This method is obsolete. Use ScrollRectExtensions.GetElementNormalizedVerticalPosition")]
        public static IEnumerator FocusAtPointCoroutine(this ScrollRect scrollView, Vector2 focusPoint, float speed)
        {
            yield return scrollView.LerpToScrollPositionCoroutine(scrollView.CalculateFocusedScrollPosition(focusPoint), speed);
        }

        [Obsolete("This method is obsolete. Use ScrollRectExtensions.GetElementNormalizedVerticalPosition")]
        public static IEnumerator FocusOnItemCoroutine(this ScrollRect scrollView, RectTransform item, float speed)
        {
            yield return scrollView.LerpToScrollPositionCoroutine(scrollView.CalculateFocusedScrollPosition(item), speed);
        }
    }
}
