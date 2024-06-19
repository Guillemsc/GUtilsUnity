using UnityEngine;
using UnityEngine.UI;

namespace GUtilsUnity.Extensions
{
    public static class RawImageExtensions
    {
        public static void SetUvRectX(this RawImage rawImage, float value)
        {
            Rect rect = rawImage.uvRect;
            rect.x = value;
            rawImage.uvRect = rect;
        }

        public static void SetUvRectY(this RawImage rawImage, float value)
        {
            Rect rect = rawImage.uvRect;
            rect.y = value;
            rawImage.uvRect = rect;
        }

        public static void SetUvRectWidth(this RawImage rawImage, float value)
        {
            Rect rect = rawImage.uvRect;
            rect.width = value;
            rawImage.uvRect = rect;
        }

        public static void SetUvRectHeight(this RawImage rawImage, float value)
        {
            Rect rect = rawImage.uvRect;
            rect.height = value;
            rawImage.uvRect = rect;
        }
    }
}
