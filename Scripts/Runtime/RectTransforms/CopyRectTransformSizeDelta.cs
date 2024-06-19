using UnityEngine;

namespace GUtilsUnity.RectTransforms
{
    /// <summary>
    /// Copies the size delta of a RectTransform from one object to another, with optional padding.
    /// </summary>
    [ExecuteAlways]
    public sealed class CopyRectTransformSizeDelta : MonoBehaviour
    {
        [Header("References")]
        public RectTransform Origin;
        public RectTransform Destination;

        [Header("Values")]
        public bool UseHorizontal = true;
        [Min(0)] public float HorizontalPadding;
        public bool UseVertical = true;
        [Min(0)] public float VerticalPadding;

        void Update()
        {
            CopySize();
        }

        void CopySize()
        {
            if (Origin == null)
            {
                return;
            }

            if (Destination == null)
            {
                return;
            }

            Vector2 originSize = Origin.sizeDelta;
            Vector2 finalSize = Destination.sizeDelta;

            if (UseHorizontal)
            {
                finalSize.x = originSize.x + HorizontalPadding;
            }

            if (UseVertical)
            {
                finalSize.y = originSize.y + VerticalPadding;
            }

            Destination.sizeDelta = finalSize;
        }
    }
}
