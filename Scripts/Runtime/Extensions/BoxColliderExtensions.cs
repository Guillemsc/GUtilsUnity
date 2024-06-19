using UnityEngine;

namespace GUtilsUnity.Extensions
{
    public static class BoxColliderExtensions
    {
        /// <summary>
        /// Calculates the pivot of the collider.
        /// The resulting Vector3 represents the pivot point of the collider,
        /// relative to its own local coordinates.
        /// </summary>
        public static Vector3 GetPivot(this BoxCollider boxCollider)
        {
            Vector3 size = boxCollider.size;
            Vector3 offsetPivot = boxCollider.center.Divide(size);

            return Vector3Extensions.HalfOne - offsetPivot;
        }
    }
}
