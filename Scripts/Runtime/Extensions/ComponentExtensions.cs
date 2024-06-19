using UnityEngine;

namespace GUtilsUnity.Extensions
{
    public static class ComponentExtensions
    {
        /// <summary>
        /// Casts the component's transform to a <see cref="RectTransform"/>.
        /// </summary>
        /// <exception cref="System.InvalidCastException"> if the transform is not a <see cref="RectTransform"/>.</exception>
        public static RectTransform GetRectTransform(this Component component)
            => (RectTransform)component.transform;

        public static void DestroyComponent(this UnityEngine.Component obj)
            => obj.DestroyObject();

        public static void DestroyComponentImmediate(this UnityEngine.Component obj)
            => obj.DestroyObjectImmediate();

        public static void DestroyGameObject(this UnityEngine.Component obj)
            => obj.gameObject.DestroyObject();

        public static void DestroyGameObjectImmediate(this UnityEngine.Component obj)
            => obj.gameObject.DestroyObjectImmediate();
    }
}
