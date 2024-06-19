using System.Collections.Generic;
using UnityEngine.Pool;

namespace GUtilsUnity.Extensions
{
    public static class ObjectPoolExtensions
    {
        /// <summary>
        /// Releases a collection of elements from the object pool.
        /// </summary>
        public static void ReleaseRange<T>(this ObjectPool<T> objectPool, IEnumerable<T> enumerable)
            where T : class
        {
            foreach (T element in enumerable)
            {
                objectPool.Release(element);
            }
        }
    }
}
