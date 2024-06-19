using System;
using System.Collections.Generic;
using UnityEngine;

namespace GUtilsUnity.Extensions
{
    public static class TransformExtensions
    {
        /// <summary>
        /// Adds one to the sibling index position relative to the parent's hierarchy.
        /// </summary>
        public static void SetAsNextSibling(this Transform transform)
        {
            transform.SetAsSiblingRelative(1);
        }

        /// <summary>
        /// Substracts one to the sibling index position relative to the parent's hierarchy.
        /// </summary>
        public static void SetAsPreviousSibling(this Transform transform)
        {
            transform.SetAsSiblingRelative(-1);
        }

        /// <summary>
        /// Sets the sibling index of the transform relative to its current index by the specified offset.
        /// </summary>
        /// <param name="transform">The transform to modify.</param>
        /// <param name="indexOffset">The amount to offset the current sibling index by.</param>
        public static void SetAsSiblingRelative(this Transform transform, int indexOffset)
        {
            int index = transform.GetSiblingIndex();
            transform.SetSiblingIndex(index + indexOffset);
        }

        /// <summary>
        /// Resets the position, rotation and scale to their default values.
        /// </summary>
        public static void Reset(this Transform transform)
        {
            transform.PositionReset();
            transform.RotationReset();
            transform.LocalScaleReset();
        }

        /// <summary>
        /// Resets the position, to its default value (0, 0, 0).
        /// </summary>
        public static void PositionReset(this Transform transform)
        {
            transform.position = Vector3.zero;
        }

        /// <summary>
        /// Resets the position XY values, to its default values (0, 0).
        /// </summary>
        public static void PositionXYReset(this Transform transform)
        {
            Vector3 position = transform.position;
            position.x = 0;
            position.y = 0;
            transform.position = position;
        }

        /// <summary>
        /// Resets the rotation, to its default value (Quaternion.identity).
        /// </summary>
        public static void RotationReset(this Transform transform)
        {
            transform.rotation = Quaternion.identity;
        }

        /// <summary>
        /// Resets the local position, local rotation and local scale to their default values.
        /// </summary>
        public static void LocalReset(this Transform transform)
        {
            transform.LocalPositionReset();
            transform.LocalRotationReset();
            transform.LocalScaleReset();
        }

        /// <summary>
        /// Resets the local position, to its default value (0, 0, 0).
        /// </summary>
        public static void LocalPositionReset(this Transform transform)
        {
            transform.localPosition = Vector3.zero;
        }

        /// <summary>
        /// Resets the local position XY values, to its default values (0, 0).
        /// </summary>
        public static void LocalPositionXYReset(this Transform transform)
        {
            Vector3 position = transform.localPosition;
            position.x = 0;
            position.y = 0;
            transform.localPosition = position;
        }

        /// <summary>
        /// Resets the local rotation, to its default value (Quaternion.identity).
        /// </summary>
        public static void LocalRotationReset(this Transform transform)
        {
            transform.localRotation = Quaternion.identity;
        }

        /// <summary>
        /// Resets the local scale, to its default value (1, 1, 1).
        /// </summary>
        public static void LocalScaleReset(this Transform transform)
        {
            transform.localScale = Vector3.one;
        }

        /// <summary>
        /// Same as <see cref="Transform.TransformPoint(Vector3)"/>, but with xy-axis only.
        /// </summary>
        public static Vector2 TransformPoint(this Transform transform, Vector2 position)
        {
            return transform.TransformPoint(position.x, position.y, 0);
        }

        /// <summary>
        /// Same as <see cref="Transform.InverseTransformPoint(Vector3)"/>, but with xy-axis only.
        /// </summary>
        public static Vector2 InverseTransformPoint(this Transform transform, Vector2 position)
        {
            return transform.InverseTransformPoint(position.x, position.y, 0);
        }

        /// <summary>
        /// Sets the x-axis position of the transform, while preserving the y and z values.
        /// </summary>
        public static void SetPositionX(this Transform transform, float x)
        {
            Vector3 currPosition = transform.position;
            transform.position = new Vector3(x, currPosition.y, currPosition.z);
        }

        /// <summary>
        /// Sets the y-axis position of the transform, while preserving the x and z values.
        /// </summary>
        public static void SetPositionY(this Transform transform, float y)
        {
            Vector3 currPosition = transform.position;
            transform.position = new Vector3(currPosition.x, y, currPosition.z);
        }

        /// <summary>
        /// Sets the z-axis position of the transform, while preserving the x and y values.
        /// </summary>
        public static void SetPositionZ(this Transform transform, float z)
        {
            Vector3 currPosition = transform.position;
            transform.position = new Vector3(currPosition.x, currPosition.y, z);
        }

        /// <summary>
        /// Sets the xy-axis position of the transform, while preserving the z value.
        /// </summary>
        public static void SetPositionXY(this Transform transform, float x, float y)
        {
            Vector3 currPosition = transform.position;
            transform.position = new Vector3(x, y, currPosition.z);
        }

        /// <summary>
        /// Sets the xz-axis position of the transform, while preserving the y value.
        /// </summary>
        public static void SetPositionXZ(this Transform transform, float x, float z)
        {
            Vector3 currPosition = transform.position;
            transform.position = new Vector3(x, currPosition.y, z);
        }

        /// <summary>
        /// Sets the yz-axis position of the transform, while preserving the x value.
        /// </summary>
        public static void SetPositionYZ(this Transform transform, float y, float z)
        {
            Vector3 currPosition = transform.position;
            transform.position = new Vector3(currPosition.x, y, z);
        }

        /// <summary>
        /// Sets the xy-axis position of the transform, while preserving the z value.
        /// </summary>
        public static void SetPositionXY(this Transform transform, Vector2 position)
        {
            transform.SetPositionXY(position.x, position.y);
        }

        /// <summary>
        /// Sets the xz-axis position of the transform, while preserving the y value.
        /// </summary>
        public static void SetPositionXZ(this Transform transform, Vector2 position)
        {
            transform.SetPositionXZ(position.x, position.y);
        }

        /// <summary>
        /// Sets the yz-axis position of the transform, while preserving the z value.
        /// </summary>
        public static void SetPositionYZ(this Transform transform, Vector2 position)
        {
            transform.SetPositionYZ(position.x, position.y);
        }

        /// <summary>
        /// Sets the x-axis local position of the transform, while preserving the y and z values.
        /// </summary>
        public static void SetLocalPositionX(this Transform transform, float x)
        {
            Vector3 currPosition = transform.localPosition;
            transform.localPosition = new Vector3(x, currPosition.y, currPosition.z);
        }

        /// <summary>
        /// Sets the y-axis local position of the transform, while preserving the x and z values.
        /// </summary>
        public static void SetLocalPositionY(this Transform transform, float y)
        {
            Vector3 currPosition = transform.localPosition;
            transform.localPosition = new Vector3(currPosition.x, y, currPosition.z);
        }

        /// <summary>
        /// Sets the z-axis local position of the transform, while preserving the x and y values.
        /// </summary>
        public static void SetLocalPositionZ(this Transform transform, float z)
        {
            Vector3 currPosition = transform.localPosition;
            transform.localPosition = new Vector3(currPosition.x, currPosition.y, z);
        }

        /// <summary>
        /// Sets the xy-axis local position of the transform, while preserving the z value.
        /// </summary>
        public static void SetLocalPositionXY(this Transform transform, float x, float y)
        {
            Vector3 currPosition = transform.localPosition;
            transform.localPosition = new Vector3(x, y, currPosition.z);
        }

        /// <summary>
        /// Sets the xz-axis local position of the transform, while preserving the y value.
        /// </summary>
        public static void SetLocalPositionXZ(this Transform transform, float x, float z)
        {
            Vector3 currPosition = transform.localPosition;
            transform.localPosition = new Vector3(x, currPosition.y, z);
        }

        /// <summary>
        /// Sets the yz-axis local position of the transform, while preserving the x value.
        /// </summary>
        public static void SetLocalPositionYZ(this Transform transform, float y, float z)
        {
            Vector3 currPosition = transform.localPosition;
            transform.localPosition = new Vector3(currPosition.x, y, z);
        }

        /// <summary>
        /// Sets the xy-axis local position of the transform, while preserving the z value.
        /// </summary>
        public static void SetLocalPositionXY(this Transform transform, Vector2 position)
        {
            transform.SetLocalPositionXY(position.x, position.y);
        }

        /// <summary>
        /// Sets the x-axis euler rotation of the transform, while preserving the y and z values.
        /// </summary>
        public static void SetRotationX(this Transform transform, float angleDegrees)
        {
            Vector3 currRotation = transform.rotation.eulerAngles;
            transform.rotation = Quaternion.Euler(angleDegrees, currRotation.y, currRotation.z);
        }

        /// <summary>
        /// Sets the y-axis euler rotation of the transform, while preserving the x and z values.
        /// </summary>
        public static void SetRotationY(this Transform transform, float angleDegrees)
        {
            Vector3 currRotation = transform.rotation.eulerAngles;
            transform.rotation = Quaternion.Euler(currRotation.x, angleDegrees, currRotation.z);
        }

        /// <summary>
        /// Sets the z-axis euler rotation of the transform, while preserving the x and y values.
        /// </summary>
        public static void SetRotationZ(this Transform transform, float angleDegrees)
        {
            Vector3 currRotation = transform.rotation.eulerAngles;
            transform.rotation = Quaternion.Euler(currRotation.x, currRotation.y, angleDegrees);
        }

        /// <summary>
        /// Sets the x-axis euler local rotation of the transform, while preserving the y and z values.
        /// </summary>
        public static void SetLocalRotationX(this Transform transform, float angleDegrees)
        {
            Vector3 currRotation = transform.localRotation.eulerAngles;
            transform.localRotation = Quaternion.Euler(angleDegrees, currRotation.y, currRotation.z);
        }

        /// <summary>
        /// Sets the y-axis euler local rotation of the transform, while preserving the x and z values.
        /// </summary>
        public static void SetLocalRotationY(this Transform transform, float angleDegrees)
        {
            Vector3 currRotation = transform.localRotation.eulerAngles;
            transform.localRotation = Quaternion.Euler(currRotation.x, angleDegrees, currRotation.z);
        }

        /// <summary>
        /// Sets the z-axis euler local rotation of the transform, while preserving the x and y values.
        /// </summary>
        public static void SetLocalRotationZ(this Transform transform, float angleDegrees)
        {
            Vector3 currRotation = transform.localRotation.eulerAngles;
            transform.localRotation = Quaternion.Euler(currRotation.x, currRotation.y, angleDegrees);
        }

        /// <summary>
        /// Sets the x-axis local scale of the transform, while preserving the y and z values.
        /// </summary>
        public static void SetLocalScaleX(this Transform transform, float x)
        {
            Vector3 currScale = transform.localScale;
            transform.localScale = new Vector3(x, currScale.y, currScale.z);
        }

        /// <summary>
        /// Sets the y-axis local scale of the transform, while preserving the x and z values.
        /// </summary>
        public static void SetLocalScaleY(this Transform transform, float y)
        {
            Vector3 currScale = transform.localScale;
            transform.localScale = new Vector3(currScale.x, y, currScale.z);
        }

        /// <summary>
        /// Sets the z-axis local scale of the transform, while preserving the x and y values.
        /// </summary>
        public static void SetLocalScaleZ(this Transform transform, float z)
        {
            Vector3 currScale = transform.localScale;
            transform.localScale = new Vector3(currScale.x, currScale.y, z);
        }

        /// <summary>
        /// Sets the xy-axis local scale of the transform, while preserving the z value.
        /// </summary>
        public static void SetLocalScaleXY(this Transform transform, Vector2 xy)
        {
            Vector3 currScale = transform.localScale;
            transform.localScale = new Vector3(xy.x, xy.y, currScale.z);
        }

        /// <summary>
        /// Sets the xz-axis local scale of the transform, while preserving the y value.
        /// </summary>
        public static void SetLocalScaleXZ(this Transform transform, Vector2 xz)
        {
            Vector3 currScale = transform.localScale;
            transform.localScale = new Vector3(xz.x, currScale.y, xz.y);
        }

        /// <summary>
        /// Adds some xz-axis value to the position of the transform.
        /// </summary>
        public static void AddPositionXY(this Transform transform, Vector2 toAdd)
        {
            Vector3 position = transform.position;
            position.x += toAdd.x;
            position.y += toAdd.y;
            transform.position = position;
        }

        /// <summary>
        /// Gets a list with all the children of the Transform.
        /// </summary>
        public static List<T> GetChildren<T>(this T transform)
            where T : Transform
        {
            List<T> ret = new List<T>();

            for (int i = 0; i < transform.childCount; ++i)
            {
                ret.Add((T)transform.GetChild(i));
            }

            return ret;
        }

        /// <summary>
        /// Gets an enumerable with all the children of the Transform.
        /// </summary>
        public static IEnumerable<T> GetChildrenEnumerable<T>(this T transform)
            where T : Transform
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                yield return (T)transform.GetChild(i);
            }
        }

        /// <summary>
        /// Executes the specified <paramref name="action"/> on each child of the <paramref name="transform"/>.
        /// </summary>
        /// <typeparam name="T">The type of the transform.</typeparam>
        /// <param name="transform">The transform whose children will be iterated over.</param>
        /// <param name="action">The action to execute on each child.</param>
        public static void ForEachChildren<T>(this T transform, Action<T> action)
            where T : Transform
        {
            for (int i = 0; i < transform.childCount; ++i)
            {
                action.Invoke((T)transform.GetChild(i));
            }
        }

        /// <summary>
        /// Executes the specified <paramref name="action"/> on each child of the <paramref name="transform"/>, with its index.
        /// </summary>
        /// <typeparam name="T">The type of the transform.</typeparam>
        /// <param name="transform">The transform whose children will be iterated over.</param>
        /// <param name="action">The action to execute on each child, which takes the child transform and its index as parameters.</param>
        public static void ForEachChildrenWithIndex<T>(this T transform, Action<T, int> action)
            where T : Transform
        {
            for (int i = 0; i < transform.childCount; ++i)
            {
                action.Invoke((T)transform.GetChild(i), i);
            }
        }

        /// <summary>
        /// Tries to retrieve the child transform at the specified <paramref name="childIndex"/>
        /// from the provided <paramref name="transform"/>.
        /// </summary>
        /// <param name="transform">The transform to retrieve the child from.</param>
        /// <param name="childIndex">The index of the child transform to retrieve.</param>
        /// <param name="child">The child transform at the specified <paramref name="childIndex"/>,
        /// if found; otherwise, <c>null</c>.</param>
        /// <returns><c>true</c> if the child transform was successfully retrieved; otherwise, <c>false</c>.</returns>
        public static bool TryGetChild(this Transform transform, int childIndex, out Transform child)
        {
            bool outsideBounds = childIndex < 0 || transform.childCount <= childIndex;

            if (outsideBounds)
            {
                child = default;
                return false;
            }

            child = transform.GetChild(childIndex);
            return true;
        }

        /// <summary>
        /// Destroys all the children from a Transform.
        /// </summary>
        public static void DestroyChildren(this Transform transform)
        {
            for (int i = transform.childCount - 1; i >= 0; i--)
            {
                Transform child = transform.GetChild(i);
                child.gameObject.RemoveParentAndDestroyGameObject();
            }
        }

        /// <summary>
        /// Immediatly destroys all the children from a Transform.
        /// </summary>
        public static void DestroyImmediateChildren(this Transform transform)
        {
            for (int i = transform.childCount - 1; i >= 0; i--)
            {
                Transform child = transform.GetChild(i);
                child.gameObject.DestroyGameObjectImmediate();
            }
        }

        /// <summary>
        /// Extension method to set the active sibling index of the specified Transform.
        /// </summary>
        /// <param name="transform">The Transform to modify.</param>
        /// <param name="siblingIndex">The desired active sibling index.</param>
        /// <remarks>
        /// This method sets the sibling index of the Transform to the specified active sibling index.
        /// The active sibling index represents the position of the active children in the parent's child list.
        /// If the specified sibling index is greater than the number of active children, the Transform will be placed at the end.
        /// </remarks>
        public static void SetActiveSiblingIndex(this Transform transform, int siblingIndex)
        {
            var activeSiblingIndex = transform.GetActiveSiblingIndex(siblingIndex);
            transform.SetSiblingIndex(activeSiblingIndex);
        }

        /// <summary>
        /// Gets the active sibling index for the specified Transform.
        /// </summary>
        /// <param name="transform">The Transform to query.</param>
        /// <param name="siblingIndex">The desired active sibling index.</param>
        /// <returns>
        /// The active sibling index based on the specified sibling index.
        /// If the parent of the Transform is null, it returns 0.
        /// </returns>
        /// <remarks>
        /// This method retrieves the active sibling index, which represents the position of the active children in the parent's child list,
        /// based on the specified sibling index.
        /// If the parent of the Transform is null (i.e., it has no parent), the method returns 0 as the active sibling index.
        /// </remarks>
        public static int GetActiveSiblingIndex(this Transform transform, int siblingIndex)
        {
            var parent = transform.parent;
            if (parent == null)
            {
                return 0;
            }

            return GetActiveChildIndex(parent, siblingIndex);
        }

        /// <summary>
        /// Gets the active child index for the specified Transform.
        /// </summary>
        /// <param name="transform">The Transform to query.</param>
        /// <param name="siblingIndex">The desired active sibling index.</param>
        /// <returns>
        /// The active child index based on the specified sibling index.
        /// If the specified sibling index is less than or equal to 0, it returns 0.
        /// If the specified sibling index is greater than the number of active children, it returns the child count.
        /// </returns>
        /// <remarks>
        /// This method retrieves the active child index, which represents the position of the active children in the parent's child list,
        /// based on the specified sibling index.
        /// If the specified sibling index is less than or equal to 0, it returns 0 as the active child index.
        /// If the specified sibling index is greater than the number of active children, it returns the total number of active children as the active child index.
        /// </remarks>
        public static int GetActiveChildIndex(this Transform transform, int siblingIndex)
        {
            int activeCount = 0;

            if (siblingIndex <= 0)
            {
                return 0;
            }

            var childCount = transform.childCount;
            if (siblingIndex >= childCount)
            {
                return childCount;
            }

            for (int i = 0; i < childCount; i++)
            {
                Transform child = transform.GetChild(i);

                if (child.gameObject.activeSelf)
                {
                    activeCount++;

                    if (activeCount == siblingIndex)
                    {
                        return i + 1;
                    }
                }
            }

            return childCount;
        }
    }
}
