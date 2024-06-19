using UnityEngine;
using UnityEngine.UI;

namespace GUtilsUnity.Extensions
{
    public static class ImageExtensions
    {
        /// <summary>
        /// Sets the image's color alpha value.
        /// </summary>
        public static void SetAlpha(this Image image, float alpha)
        {
            Color color = image.color;
            color.a = alpha;
            image.color = color;
        }
    }
}
