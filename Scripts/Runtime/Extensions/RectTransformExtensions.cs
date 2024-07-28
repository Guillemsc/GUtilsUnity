using System;
using GUtils.Directions;
using GUtilsUnity.Attributes;
using GUtilsUnity.Ui;
using UnityEngine;

namespace GUtilsUnity.Extensions
{
    public static class RectTransformExtensions
    {
        /// <summary>
        /// Sets the anchoredPosition value x, and keeps the previous y value.
        /// </summary>
        public static void SetAnchoredPositionX(this RectTransform transform, float x)
        {
            Vector2 currPosition = transform.anchoredPosition;
            transform.anchoredPosition = new Vector2(x, currPosition.y);
        }

        /// <summary>
        /// Sets the anchoredPosition value y, and keeps the previous x value.
        /// </summary>
        public static void SetAnchoredPositionY(this RectTransform transform, float y)
        {
            Vector2 currPosition = transform.anchoredPosition;
            transform.anchoredPosition = new Vector2(currPosition.x, y);
        }

        /// <summary>
        /// Adds some value to the current anchoredPosition x value, and keeps the previous y value.
        /// </summary>
        public static void AddAnchoredPositionX(this RectTransform transform, float x)
        {
            Vector2 currPosition = transform.anchoredPosition;
            transform.anchoredPosition = new Vector2(currPosition.x + x, currPosition.y);
        }

        /// <summary>
        /// Adds some value to the current anchoredPosition y value, and keeps the previous x value.
        /// </summary>
        public static void AddAnchoredPositionY(this RectTransform transform, float y)
        {
            Vector2 currPosition = transform.anchoredPosition;
            transform.anchoredPosition = new Vector2(currPosition.x, currPosition.y + y);
        }

        /// <summary>
        /// Sets the pivot value x, and keeps the previous y value.
        /// </summary>
        public static void SetPivotX(this RectTransform transform, float x)
        {
            Vector2 currPosition = transform.pivot;
            transform.pivot = new Vector3(x, currPosition.y);
        }

        /// <summary>
        /// Sets the pivot value y, and keeps the previous x value.
        /// </summary>
        public static void SetPivotY(this RectTransform transform, float y)
        {
            Vector2 currPosition = transform.pivot;
            transform.pivot = new Vector3(currPosition.x, y);
        }

        /// <summary>
        /// Sets the sizeDelta value x, and keeps the previous y value.
        /// </summary>
        public static void SetSizeDeltaX(this RectTransform transform, float x)
        {
            Vector2 currSizeDelta = transform.sizeDelta;
            transform.sizeDelta = new Vector2(x, currSizeDelta.y);
        }

        /// <summary>
        /// Sets the sizeDelta value y, and keeps the previous x value.
        /// </summary>
        public static void SetSizeDeltaY(this RectTransform transform, float y)
        {
            Vector2 currSizeDelta = transform.sizeDelta;
            transform.sizeDelta = new Vector2(currSizeDelta.x, y);
        }

        /// <summary>
        /// Sets the anchorMax value x, and keeps the previous y value.
        /// </summary>
        public static void SetAnchorMaxX(this RectTransform transform, float x)
        {
            Vector2 currAnchor = transform.anchorMax;
            transform.anchorMax = new Vector3(x, currAnchor.x);
        }

        /// <summary>
        /// Sets the anchorMax value y, and keeps the previous x value.
        /// </summary>
        public static void SetAnchorMaxY(this RectTransform transform, float y)
        {
            Vector2 currAnchor = transform.anchorMax;
            transform.anchorMax = new Vector3(currAnchor.x, y);
        }

        /// <summary>
        /// Gets a rect with the anchoredPosition as center, and the sizeDelta as size.
        /// </summary>
        public static Rect GetAnchoredRect(this RectTransform transform)
        {
            Vector2 halfSize = transform.sizeDelta * 0.5f;
            Vector2 anchoredPosition = transform.anchoredPosition;

            Rect bounds = new Rect
            {
                min = anchoredPosition - halfSize,
                max = anchoredPosition + halfSize,
            };

            return bounds;
        }


        public static Vector3 GetWorldOffsetToPlaceOutsideContainerRectTransform(
            this RectTransform toMove,
            RectTransform reference,
            CardinalDirection cardinalDirection,
            float extraOffset)
        {
            Rect referenceRect = reference.rect;
            Rect containedRect = toMove.GetLocalCornersRectRelativeToTransform(reference);

            Vector3 localOffset = referenceRect.GetOffsetToBePlacedOutsideRect(
                containedRect,
                cardinalDirection,
                extraOffset);

            Vector3 worldOffsetWithoutCorrection = reference.TransformPoint(localOffset);
            Vector3 worldOffset = worldOffsetWithoutCorrection - reference.position;
            return worldOffset;
        }

        /// <summary>
        /// Gets te screen position of the RectTransform.
        /// </summary>
        public static Vector2 GetScreenPosition(this RectTransform transform, Camera camera = null)
        {
            return transform.GetScreenPositionFromLocalPosition(transform.localPosition.ToVector2XY(), camera);
        }

        /// <summary>
        /// Returns the screen space <see cref="Rect"/> of a <see cref="RectTransform"/>.
        /// </summary>
        /// <param name="transform">The <see cref="RectTransform"/> to get the screen rectangle of.</param>
        /// <returns>A <see cref="Rect"/> that represents the screen space rectangle of the <see cref="RectTransform"/> in pixels.</returns>
        public static Rect GetScreenRect(this RectTransform transform)
        {
            Bounds bounds = transform.GetWorldCornersBounds();

            return RectExtensions.FromCenterAndSize(bounds.center.ToVector2XY(), bounds.size.ToVector2XY());
        }

        [Experimental]
        public static Rect GetCanvasRect(this RectTransform transform, Canvas canvas = null)
        {
            canvas ??= transform.GetComponentInParent<Canvas>();
            if (canvas == null)
            {
                throw new InvalidOperationException("RectTransform is not inside a canvas");
            }

            transform.GetWorldCorners(CornersBuffer);

            for (int i = 0; i < 4; ++i)
            {
                RectTransformUtility.ScreenPointToLocalPointInRectangle(
                    canvas.GetComponent<RectTransform>(),
                    CornersBuffer[i],
                    canvas.worldCamera,
                    out Vector2 localPos);
                CornersBuffer[i] = localPos;
            }

            Vector3 min = CornersBuffer[0];
            Vector3 max = CornersBuffer[0];
            for (int i = 1; i < 4; ++i)
            {
                min = Vector3.Min(min, CornersBuffer[i]);
                max = Vector3.Max(max, CornersBuffer[i]);
            }

            return Rect.MinMaxRect(min.x, min.y, max.x, max.y);
        }

        /// <summary>
        /// Retrieves the local position of a RectTransform relative to its parent RectTransform based on a screen position.
        /// </summary>
        /// <param name="transform">The RectTransform to retrieve the local position from.</param>
        /// <param name="screenPosition">The screen position to calculate the local position from.</param>
        /// <param name="camera">The camera used for the screen-to-world conversion. If null, the main camera will be used.</param>
        /// <returns>A Vector2 representing the local position of the RectTransform relative to its parent.</returns>
        public static Vector2 GetLocalPositionRelativeToParentFromScreenPosition(
            this RectTransform transform,
            Vector2 screenPosition,
            Camera camera = null)
        {
            RectTransform parentRectTransform = transform.parent as RectTransform;

            if (parentRectTransform == null)
            {
                return screenPosition;
            }

            return parentRectTransform.GetLocalPositionFromScreenPosition(screenPosition, camera);
        }

        /// <summary>
        /// Gets a position on the local space of a RectTransform, from a screen position.
        /// </summary>
        /// <param name="transform">The RectTransform to get the local position relative to.</param>
        /// <param name="screenPosition">The point on the screen to get the local position of.</param>
        /// <param name="camera">The camera to use for the screen point conversion. If null, uses the main camera.</param>
        /// <returns>The local position of the RectTransform.</returns>
        public static Vector2 GetLocalPositionFromScreenPosition(
            this RectTransform transform,
            Vector2 screenPosition,
            Camera camera = null)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                transform,
                screenPosition,
                camera,
                out Vector2 localPoint
            );

            return localPoint;
        }

        /// <summary>
        /// Sets the local position of a <see cref="RectTransform"/> based on a screen position.
        /// </summary>
        /// <param name="transform">The RectTransform whose anchored position will be set.</param>
        /// <param name="screenPosition">The screen position to set get anchored position from.</param>
        /// <param name="camera">The camera to use for the screen-to-world conversion. If null, uses the main camera.</param>
        /// <remarks>
        /// This method first calculates the local position of the given screen position (using <see cref="GetLocalPositionFromScreenPosition"/>)
        /// by using the provided or main camera to convert the screen position into local position coordinates relative to the
        /// parent <see cref="RectTransform"/>. It then sets the anchored position of the provided
        /// <see cref="RectTransform"/> using the local position.
        /// </remarks>
        public static void SetLocalPositionFromScreenPosition(
            this RectTransform transform,
            Vector2 screenPosition,
            Camera camera = null)
        {
            Vector2 localPosition = transform.GetLocalPositionRelativeToParentFromScreenPosition(screenPosition, camera);
            transform.SetLocalPositionXY(localPosition);
        }

        /// <summary>
        /// Returns the screen position of the given <paramref name="localPosition"/> relative to this <paramref name="transform"/>.
        /// </summary>
        /// <param name="transform">The <see cref="RectTransform"/> that the <paramref name="localPosition"/> is relative to.</param>
        /// <param name="localPosition">The local position to get the screen position from.</param>
        /// <param name="camera">The camera to calculate the screen position with. If not specified, uses the main camera.</param>
        /// <returns>The screen position of the given <paramref name="localPosition"/>.</returns>
        public static Vector2 GetScreenPositionFromLocalPosition(
            this RectTransform transform,
            Vector2 localPosition,
            Camera camera = null)
        {
            RectTransform parentRectTransform = transform.parent as RectTransform;

            if (parentRectTransform == null)
            {
                return localPosition;
            }

            Vector2 worldPosition = TransformExtensions.TransformPoint(parentRectTransform, localPosition);

            return RectTransformUtility.WorldToScreenPoint(camera, worldPosition.ToVector3XY());
        }

        /// <summary>
        /// Returns the anchored position of the specified <see cref="RectTransform"/> relative to another <see cref="RectTransform"/>.
        /// </summary>
        /// <param name="transform">The transform to get the anchored position from.</param>
        /// <param name="to">The transform to get the anchored position relative to.</param>
        /// <param name="camera">The camera used for screen point conversion. If null, uses the main camera.</param>
        /// <returns>The anchored position of the <paramref name="transform"/> relative to the <paramref name="to"/> <see cref="RectTransform"/>.</returns>
        /// <remarks>
        /// Can be useful in a situation where you have two UI elements that are children of different parent elements and you want to
        /// know the position of one element relative to the other.
        /// For example, let's say you have a button that is a child of a panel,
        /// and you want to know the position of the button relative to the position of an image
        /// that is a child of another panel. This method can be used to calculate the anchored position of the
        /// button relative to the anchored position of the image.
        /// </remarks>
        public static Vector2 GetAnchoredPositionRelativeToRectTransform(
            this RectTransform transform,
            RectTransform to,
            Camera camera = null)
        {
            Vector2 transformPivot = transform.pivot;
            Rect transformRect = transform.rect;

            Vector2 toPivot = to.pivot;
            Rect toRect = to.rect;

            Vector2 transformPivotDerivedOffset = new Vector2(
                transformRect.width * transformPivot.x + transformRect.xMin,
                transformRect.height * transformPivot.y + transformRect.yMin
            );

            Vector2 toPivotDerivedOffset = new Vector2(
                toRect.width * toPivot.x + toRect.xMin,
                toRect.height * toPivot.y + toRect.yMin
            );

            Vector2 screenPosition = RectTransformUtility.WorldToScreenPoint(camera, transform.position);
            screenPosition += transformPivotDerivedOffset;

            RectTransformUtility.ScreenPointToLocalPointInRectangle(to, screenPosition, camera, out Vector2 localPoint);

            return to.anchoredPosition + localPoint - toPivotDerivedOffset;
        }

        /// <summary>
        /// Copies the RectTransform values from one RectTransform to another RectTransform.
        /// </summary>
        /// <param name="from">The RectTransform to copy the values from.</param>
        /// <param name="to">The RectTransform to copy the values to.</param>
        /// <remarks>
        /// Copies <see cref="RectTransform.anchorMin"/>, <see cref="RectTransform.anchorMax"/>,
        /// <see cref="RectTransform.anchoredPosition"/>, <see cref="RectTransform.sizeDelta"/> and
        /// <see cref="RectTransform.pivot"/>.
        /// </remarks>
        public static void CopyValues(RectTransform from, RectTransform to)
        {
            to.anchorMin = from.anchorMin;
            to.anchorMax = from.anchorMax;
            to.anchoredPosition = from.anchoredPosition;
            to.sizeDelta = from.sizeDelta;
            to.pivot = from.pivot;
        }

        /// <summary>
        /// Sets the position of the left edge of a RectTransform in its parent's coordinate space.
        /// </summary>
        /// <param name="transform">The RectTransform to set the left edge position of.</param>
        /// <param name="left">The position of the left edge to set.</param>
        public static void SetLeft(this RectTransform transform, float left)
        {
            transform.offsetMin = new Vector2(left, transform.offsetMin.y);
        }

        /// <summary>
        /// Gets the position of the left edge of a RectTransform's rect relative to its parent's rect.
        /// </summary>
        /// <param name="transform">The RectTransform to get the value from.</param>
        /// <returns>The position of left edge of the RectTransform's rect.</returns>
        public static float GetLeft(this RectTransform transform)
        {
            return transform.offsetMin.x;
        }

        /// <summary>
        /// Adds some value to the position of the left edge of a RectTransform in its parent's coordinate space.
        /// </summary>
        /// <param name="transform">The RectTransform to set the left edge position of.</param>
        /// <param name="value">The amount to add to the position of the left edge.</param>
        public static float AddLeft(this RectTransform transform, float value)
        {
            var left = GetLeft(transform);
            var res = left + value;
            SetLeft(transform, res);
            return res;
        }

        /// <summary>
        /// Substracts some value to the position of the left edge of a RectTransform in its parent's coordinate space.
        /// </summary>
        /// <param name="transform">The RectTransform to set the left edge position of.</param>
        /// <param name="value">The amount to substract to the position of the left edge.</param>
        public static float RemoveLeft(this RectTransform transform, float value)
        {
            var left = GetLeft(transform);
            var res = left - value;
            SetLeft(transform, res);
            return res;
        }

        /// <summary>
        /// Sets the position of the right edge of a RectTransform in its parent's coordinate space.
        /// </summary>
        /// <param name="transform">The RectTransform to set the right edge position of.</param>
        /// <param name="right">The position of the right edge to set.</param>
        public static void SetRight(this RectTransform transform, float right)
        {
            transform.offsetMax = new Vector2(-right, transform.offsetMax.y);
        }

        /// <summary>
        /// Gets the position of the right edge of a RectTransform's rect relative to its parent's rect.
        /// </summary>
        /// <param name="transform">The RectTransform to get the value from.</param>
        /// <returns>The position of right edge of the RectTransform's rect.</returns>
        public static float GetRight(this RectTransform transform)
        {
            return -transform.offsetMax.x;
        }

        /// <summary>
        /// Adds some value to the position of the right edge of a RectTransform in its parent's coordinate space.
        /// </summary>
        /// <param name="transform">The RectTransform to set the right edge position of.</param>
        /// <param name="value">The amount to add to the position of the right edge.</param>
        public static float AddRight(this RectTransform transform, float value)
        {
            var right = GetRight(transform);
            var res = right + value;
            SetRight(transform, res);
            return res;
        }

        /// <summary>
        /// Substracts some value to the position of the right edge of a RectTransform in its parent's coordinate space.
        /// </summary>
        /// <param name="transform">The RectTransform to set the right edge position of.</param>
        /// <param name="value">The amount to substract to the position of the right edge.</param>
        public static float RemoveRight(this RectTransform transform, float value)
        {
            var right = GetRight(transform);
            var res = right - value;
            SetRight(transform, res);
            return res;
        }

        /// <summary>
        /// Sets the position of the top edge of a RectTransform in its parent's coordinate space.
        /// </summary>
        /// <param name="transform">The RectTransform to set the top edge position of.</param>
        /// <param name="top">The position of the top edge to set.</param>
        public static void SetTop(this RectTransform transform, float top)
        {
            transform.offsetMax = new Vector2(transform.offsetMax.x, -top);
        }

        /// <summary>
        /// Gets the position of the top edge of a RectTransform's rect relative to its parent's rect.
        /// </summary>
        /// <param name="transform">The RectTransform to get the value from.</param>
        /// <returns>The position of top edge of the RectTransform's rect.</returns>
        public static float GetTop(this RectTransform transform)
        {
            return -transform.offsetMax.y;
        }

        /// <summary>
        /// Adds some value to the position of the top edge of a RectTransform in its parent's coordinate space.
        /// </summary>
        /// <param name="transform">The RectTransform to set the top edge position of.</param>
        /// <param name="value">The amount to add to the position of the top edge.</param>
        public static float AddTop(this RectTransform transform, float value)
        {
            var top = GetTop(transform);
            var res = top + value;
            SetTop(transform, res);
            return res;
        }

        /// <summary>
        /// Substracts some value to the position of the top edge of a RectTransform in its parent's coordinate space.
        /// </summary>
        /// <param name="transform">The RectTransform to set the top edge position of.</param>
        /// <param name="value">The amount to substract to the position of the top edge.</param>
        public static float RemoveTop(this RectTransform transform, float value)
        {
            var top = GetTop(transform);
            var res = top - value;
            SetTop(transform, res);
            return res;
        }

        /// <summary>
        /// Sets the position of the bottom edge of a RectTransform in its parent's coordinate space.
        /// </summary>
        /// <param name="transform">The RectTransform to set the bottom edge position of.</param>
        /// <param name="bottom">The position of the bottom edge to set.</param>
        public static void SetBottom(this RectTransform transform, float bottom)
        {
            transform.offsetMin = new Vector2(transform.offsetMin.x, bottom);
        }

        /// <summary>
        /// Gets the position of the bottom edge of a RectTransform's rect relative to its parent's rect.
        /// </summary>
        /// <param name="transform">The RectTransform to get the value from.</param>
        /// <returns>The position of bottom edge of the RectTransform's rect.</returns>
        public static float GetBottom(this RectTransform transform)
        {
            return transform.offsetMin.y;
        }

        /// <summary>
        /// Adds some value to the position of the bottom edge of a RectTransform in its parent's coordinate space.
        /// </summary>
        /// <param name="transform">The RectTransform to set the bottom edge position of.</param>
        /// <param name="value">The amount to add to the position of the bottom edge.</param>
        public static float AddBottom(this RectTransform transform, float value)
        {
            var bottom = GetBottom(transform);
            var res = bottom + value;
            SetBottom(transform, res);
            return res;
        }

        /// <summary>
        /// Substracts some value to the position of the bottom edge of a RectTransform in its parent's coordinate space.
        /// </summary>
        /// <param name="transform">The RectTransform to set the bottom edge position of.</param>
        /// <param name="value">The amount to substract to the position of the bottom edge.</param>
        public static float RemoveBottom(this RectTransform transform, float value)
        {
            var bottom = GetBottom(transform);
            var res = bottom - value;
            SetBottom(transform, res);
            return res;
        }

        /// <summary>
        /// Adds the values of the given <paramref name="rectOffset"/> to the corresponding offsets of the <paramref name="transform"/> RectTransform.
        /// </summary>
        /// <param name="transform">The RectTransform to which the offsets will be added.</param>
        /// <param name="rectOffset">The RectOffset whose values will be added to the RectTransform offsets.</param>
        public static void AddRectOffsets(
            this RectTransform transform,
            RectOffset rectOffset)
        {
            transform.AddBottom(rectOffset.bottom);
            transform.AddTop(rectOffset.top);
            transform.AddLeft(rectOffset.left);
            transform.AddRight(rectOffset.right);
        }

        /// <summary>
        /// Removes the values of the given <paramref name="rectOffset"/> from the corresponding offsets of the <paramref name="transform"/> RectTransform.
        /// </summary>
        /// <param name="transform">The RectTransform from which the offsets will be removed.</param>
        /// <param name="rectOffset">The RectOffset whose values will be removed from the RectTransform offsets.</param>
        public static void RemoveRectOffsets(
            this RectTransform transform,
            RectOffset rectOffset)
        {
            transform.RemoveBottom(rectOffset.bottom);
            transform.RemoveTop(rectOffset.top);
            transform.RemoveLeft(rectOffset.left);
            transform.RemoveRight(rectOffset.right);
        }

        // Buffer used for operations with RectTransform corners.
        static readonly Vector3[] CornersBuffer = new Vector3[4];

        /// <summary>
        /// Returns the bounds of the RectTransform in world space by calculating the world positions of its corners.
        /// </summary>
        /// <param name="rectTransform">The RectTransform whose bounds to calculate.</param>
        /// <returns>The bounds of the RectTransform in world space.</returns>
        public static Bounds GetWorldCornersBounds(this RectTransform rectTransform)
        {
            rectTransform.GetWorldCorners(CornersBuffer);
            Bounds bounds = new Bounds(CornersBuffer[0], Vector3.zero);
            for (int i = 1; i < 4; ++i)
            {
                bounds.Encapsulate(CornersBuffer[i]);
            }

            return bounds;
        }

        /// <summary>
        /// Retrieves the world space coordinates of the four corners of a RectTransform and creates a Rect from them.
        /// </summary>
        /// <param name="rectTransform">The RectTransform to retrieve the corners from.</param>
        /// <returns>A Rect representing the bounding rectangle in world space.</returns>
        public static Rect GetWorldCornersRect(this RectTransform rectTransform)
        {
            rectTransform.GetWorldCorners(CornersBuffer);

            Rect rect = new Rect(
                CornersBuffer[0].x,
                CornersBuffer[0].y,
                CornersBuffer[2].x - CornersBuffer[0].x,
                CornersBuffer[2].y - CornersBuffer[0].y
            );

            return rect;
        }

        /// <summary>
        /// Retrieves the bounds of a <see cref="RectTransform"/> relative to a specified <see cref="Transform"/> local space.
        /// </summary>
        /// <param name="rectTransform">The source <see cref="RectTransform"/> to retrieve the local corners from.</param>
        /// <param name="relativeTo">The <see cref="Transform"/> to which the local corners should be relative.</param>
        /// <returns>The rectangle bounds of the <paramref name="rectTransform"/> relative to
        /// the <paramref name="relativeTo"/> <see cref="Transform"/> local space.</returns>
        public static Rect GetLocalCornersRectRelativeToTransform(
            this RectTransform rectTransform,
            Transform relativeTo
            )
        {
            rectTransform.GetWorldCorners(CornersBuffer);

            for (int i = 0; i < CornersBuffer.Length; i++)
            {
                CornersBuffer[i] = relativeTo.InverseTransformPoint(CornersBuffer[i]);
            }

            Rect rect = new Rect(
                CornersBuffer[0].x,
                CornersBuffer[0].y,
                CornersBuffer[2].x - CornersBuffer[0].x,
                CornersBuffer[2].y - CornersBuffer[0].y
            );

            return rect;
        }

        /// <summary>
        /// Returns the width of the rectangle that bounds the four corners of the RectTransform in world space.
        /// </summary>
        /// <param name="rectTransform">The RectTransform to calculate the width of.</param>
        /// <returns>The width of the world space bounding rectangle.</returns>
        public static float GetWorldCornersWidth(this RectTransform rectTransform)
        {
            rectTransform.GetWorldCorners(CornersBuffer);
            return Vector3.Distance(CornersBuffer[0], CornersBuffer[3]);
        }

        /// <summary>
        /// Returns the height of the rectangle that bounds the four corners of the RectTransform in world space.
        /// </summary>
        /// <param name="rectTransform">The RectTransform to calculate the width of.</param>
        /// <returns>The width of the world space bounding rectangle.</returns>
        public static float GetWorldCornersHeight(this RectTransform rectTransform)
        {
            rectTransform.GetWorldCorners(CornersBuffer);
            return Vector3.Distance(CornersBuffer[0], CornersBuffer[1]);
        }

        /// <summary>
        /// Returns the size of the rectangle that bounds the four corners of the RectTransform in world space.
        /// </summary>
        /// <param name="rectTransform">The RectTransform to calculate the width of.</param>
        /// <returns>The width of the world space bounding rectangle.</returns>
        public static Vector2 GetWorldCornersSize(this RectTransform rectTransform)
        {
            rectTransform.GetWorldCorners(CornersBuffer);
            return new Vector2(
                Vector3.Distance(CornersBuffer[0], CornersBuffer[3]),
                Vector3.Distance(CornersBuffer[0], CornersBuffer[1])
            );
        }

        /// <summary>
        /// Determines whether the specified screen position is contained within the bounds of this RectTransform.
        /// </summary>
        /// <param name="rectTransform">The RectTransform to check.</param>
        /// <param name="screenPosition">The screen position to check.</param>
        /// <returns>True if the specified screen position is within the RectTransform's bounds; otherwise, false.</returns>
        public static bool ContainsScreenPosition(this RectTransform rectTransform, Vector2 screenPosition)
        {
            Vector2 positionInLocalSpace = rectTransform.InverseTransformPoint(screenPosition);
            return rectTransform.rect.Contains(positionInLocalSpace);
        }

        /// <summary>
        /// Gets the world position of one of the corners of the <paramref name="rectTransform"/>.
        /// </summary>
        /// <param name="rectTransform">The RectTransform from which to get the corner position.</param>
        /// <param name="rectangleCorner">The corner of the rectangle to get the world position for.</param>
        /// <returns>The world position of the specified corner.</returns>
        public static Vector3 GetWorldCorner(this RectTransform rectTransform, RectangleCorner rectangleCorner)
        {
            rectTransform.GetWorldCorners(CornersBuffer);
            return rectangleCorner switch
            {
                RectangleCorner.UpperLeft => CornersBuffer[1],
                RectangleCorner.UpperRight => CornersBuffer[2],
                RectangleCorner.LowerLeft => CornersBuffer[0],
                RectangleCorner.LowerRight => CornersBuffer[3],
                _ => throw new ArgumentOutOfRangeException(nameof(rectangleCorner), rectangleCorner, null)
            };
        }

        /// <summary>
        /// Calculates the world position offset required to contain the full <see cref="RectTransform"/> rect
        /// inside another <see cref="RectTransform"/>'sº rect.
        /// </summary>
        /// <param name="contained">The contained <see cref="RectTransform"/>.</param>
        /// <param name="container">The container <see cref="RectTransform"/>.</param>
        /// <returns>The world offset required to contain the <paramref name="contained"/>
        /// <see cref="RectTransform"/> inside the <paramref name="container"/> <see cref="RectTransform"/>.</returns>
        public static Vector3 GetWorldOffsetToContainInsideContainerRectTransform(this RectTransform contained, RectTransform container)
        {
            Rect containerRect = container.rect;
            Rect containedRect = contained.GetLocalCornersRectRelativeToTransform(container);

            Vector3 localOffset = containerRect.GetOffsetToBeContainedInsideContainer(containedRect);

            Vector3 worldOffsetWithoutCorrection = container.TransformPoint(localOffset);
            Vector3 worldOffset = worldOffsetWithoutCorrection - container.position;
            return worldOffset;
        }

        /// <summary>
        /// Sets the world position of a <see cref="RectTransform"/> so its rect is contained inside
        /// another <see cref="RectTransform"/>'s rect.
        /// </summary>
        /// <param name="contained">The contained <see cref="RectTransform"/>.</param>
        /// <param name="container">The container <see cref="RectTransform"/>.</param>
        public static void SetWorldPositionToContainInsideContainerRectTransform(this RectTransform contained, RectTransform container)
        {
            Vector3 worldOffset = contained.GetWorldOffsetToContainInsideContainerRectTransform(container);
            contained.transform.position += worldOffset;
        }

        /// <summary>
        /// Expands the RectTransform anchors to cover the entire parent and sets its sizeDelta to zero.
        /// This makes the RectTransform have the same size as its parent.
        /// </summary>
        /// <param name="transform">The RectTransform to modify.</param>
        public static void SetAnchorsAndSizeToFillParent(this RectTransform transform)
        {
            transform.anchorMin = Vector2.zero;
            transform.anchorMax = Vector2.one;
            transform.sizeDelta = Vector2.zero;
        }
    }
}
