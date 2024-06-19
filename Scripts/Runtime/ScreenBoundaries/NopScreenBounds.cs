using UnityEngine;

namespace GUtilsUnity.ScreenBoundaries
{
    public sealed class NopScreenBounds : IScreenBounds
    {
        public static readonly NopScreenBounds Instance = new();

        NopScreenBounds() { }

        public Rect CalculateScreenSpaceRect() => default;
        public Bounds CalculateWorldSpaceBounds() => default;
        public Bounds CalculateWorldSpaceBounds(float cameraDistance) => default;
        public float GetVerticalAspectRatio() => default;
        public float GetHorizontalAspectRatio() => default;
        public float GetAspectRatioDifferenceFromCamera() => default;
    }
}
