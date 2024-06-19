using UnityEngine;

namespace GUtilsUnity.ScreenBoundaries
{
    /// <summary>
    /// Represents an interface for calculating screen and world space bounds and aspect ratios.
    /// </summary>
    public interface IScreenBounds
    {
        /// <summary>
        /// Calculates the screen space rectangle.
        /// </summary>
        /// <returns>The screen space rectangle.</returns>
        Rect CalculateScreenSpaceRect();
        Bounds CalculateWorldSpaceBounds();
        Bounds CalculateWorldSpaceBounds(float cameraDistance);
        float GetVerticalAspectRatio();
        float GetHorizontalAspectRatio();
        float GetAspectRatioDifferenceFromCamera();
    }
}
