using UnityEngine;

namespace GUtilsUnity.Extensions
{
    public static class SpriteRendererExtensions
    {
        /// <summary>
        /// Sets the color's alpha value of a SpriteRenderer to a given normalized value.
        /// </summary>
        /// <param name="spriteRenderer">The SpriteRenderer whose alpha value will be changed.</param>
        /// <param name="normalizedAlpha">The normalized alpha value to set.</param>
        public static void SetAlpha(this SpriteRenderer spriteRenderer, float normalizedAlpha)
        {
            Color color = spriteRenderer.color;
            color.a = normalizedAlpha;
            spriteRenderer.color = color;
        }
    }
}
