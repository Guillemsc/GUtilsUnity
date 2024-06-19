using UnityEngine;

namespace GUtilsUnity.Extensions
{
    public static class QuaternionExtensions
    {
        /// <summary>
        /// Rotates a point, some amount of Quaternion rotation, around a pivot.
        /// </summary>
        public static Vector3 RotatePointAroundPivot(this Quaternion quaternion, Vector3 point, Vector3 pivot)
        {
            // Get point direction relative to pivot
            Vector3 direction = point - pivot;

            // Rotate it
            direction = quaternion * direction;

            // Calculate rotated point
            point = direction + pivot;

            return point;
        }
    }
}
