using System;

namespace GUtilsUnity.Extensions
{
    public static class UnityObjectExtensions
    {
        /// <summary>
        /// Destroys a GameObject, component or asset.
        /// </summary>
        public static void DestroyObject(this UnityEngine.Object obj)
        {
            if (obj == null)
            {
                return;
            }

            UnityEngine.Object.Destroy(obj);
        }

        /// <summary>
        /// Destroys a GameObject, component or asset, immediately. It's are strongly recommended to use <see cref="DestroyObject"/> instead.
        /// </summary>
        public static void DestroyObjectImmediate(this UnityEngine.Object obj)
        {
            if (obj == null)
            {
                return;
            }

            UnityEngine.Object.DestroyImmediate(obj);
        }

        [Obsolete("Use DestroyObject instead")]
        public static void Destroy(this UnityEngine.Object obj)
            => obj.DestroyObject();

        [Obsolete("Use DestroyObjectImmediate instead")]
        public static void DestroyImmediate(this UnityEngine.Object obj)
            => obj.DestroyObjectImmediate();
    }
}
