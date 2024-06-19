using System.Linq;
using UnityEngine;

namespace GUtilsUnity.Extensions
{
    public static class Texture2DExtensions
    {
        /// <summary>
        /// Converts a <see cref="Texture2D"/> to a <see cref="Sprite"/> with a default pixelsPerUnit value of 100.
        /// </summary>
        /// <param name="texture">The texture to be converted to a sprite.</param>
        /// <returns>A new <see cref="Sprite"/> instance created from the input <paramref name="texture"/>.</returns>
        public static Sprite ToSprite(this Texture2D texture)
        {
            return texture.ToSprite(100f);
        }

        /// <summary>
        /// Converts a <see cref="Texture2D"/> to a <see cref="Sprite"/> using the specified <paramref name="pixelsPerUnit"/> value.
        /// </summary>
        /// <param name="texture">The texture to be converted to a sprite.</param>
        /// <param name="pixelsPerUnit">The number of pixels that correspond to one unit of world space.</param>
        /// <returns>A new <see cref="Sprite"/> instance created.</returns>
        public static Sprite ToSprite(this Texture2D texture, float pixelsPerUnit)
        {
            return Sprite.Create(
                texture,
                new Rect(0.0f, 0.0f, texture.width, texture.height),
                Vector2Extensions.HalfOne,
                pixelsPerUnit
            );
        }

        /// <summary>
        /// Converts a <see cref="Texture2D"/> to a <see cref="Sprite"/> using the specified <paramref name="rect"/>
        /// and <paramref name="pixelsPerUnit"/> values.
        /// </summary>
        /// <param name="texture">The texture to be converted to a sprite.</param>
        /// <param name="rect">The portion of the <paramref name="texture"/> to be used for the sprite.</param>
        /// <param name="pixelsPerUnit">The number of pixels that correspond to one unit of world space.</param>
        /// <returns>A new <see cref="Sprite"/> instance created.</returns>
        public static Sprite ToSprite(this Texture2D texture, Rect rect, float pixelsPerUnit)
        {
            return Sprite.Create(
                texture,
                rect,
                Vector2Extensions.HalfOne,
                pixelsPerUnit
            );
        }

        /// <summary>
        /// Returns the color of the pixel at the specified <paramref name="position"/> in a <see cref="Texture2D"/>.
        /// </summary>
        /// <param name="texture">The texture to retrieve the pixel color from.</param>
        /// <param name="position">The position of the pixel to retrieve.</param>
        /// <returns>The color of the pixel at the specified <paramref name="position"/> in the input <paramref name="texture"/>.</returns>
        public static Color GetPixel(this Texture2D texture, Vector2Int position)
        {
            return texture.GetPixel(position.x, position.y);
        }

        /// <summary>
        /// Updates all the pixels in the texture with the provided color
        /// </summary>
        /// <param name="texture">The texture tu update</param>
        /// <param name="color">The color to update the texture with</param>
        public static void SetAllPixelsAsColor(this Texture2D texture, Color color)
        {
            var pixels = Enumerable.Repeat(color, texture.width * texture.height).ToArray();
            texture.SetPixels(pixels);
        }
    }
}
