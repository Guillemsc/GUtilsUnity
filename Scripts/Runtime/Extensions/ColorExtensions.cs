using UnityEngine;

namespace GUtilsUnity.Extensions
{
    public static class ColorExtensions
    {
        /// <summary>
        /// Fully transparent color.
        /// </summary>
        public static readonly Color Transparent = new(0f, 0f, 0f, 0f);

        /// <summary>
        /// <see cref="Color.magenta"/> color that can be used as placeolder.
        /// </summary>
        public static readonly Color Placeholder = Color.magenta;

        /// <summary>
        /// Checks if provided html color string is the same as this color.
        /// </summary>
        public static bool IsHtmlColor(this Color color, string htmlColor)
        {
            string pixelHtmlColor = ColorUtility.ToHtmlStringRGB(color);

            string FormatHtmlColor(string htmlColorString)
            {
                if (string.IsNullOrEmpty(htmlColorString))
                {
                    return htmlColorString;
                }

                return htmlColorString.Replace("#", "").ToLower();
            }

            return string.Equals(FormatHtmlColor(pixelHtmlColor), FormatHtmlColor(htmlColor));
        }

        /// <summary>
        /// Generates a color from an html color string.
        /// </summary>
        public static Color FromHtmlColor(string htmlColor)
        {
            bool couldParse = ColorUtility.TryParseHtmlString(htmlColor, out Color color);

            if (!couldParse)
            {
                return Placeholder;
            }

            return color;
        }

        /// <summary>
        /// Generates html color string from a color.
        /// </summary>
        public static string ToHtmlColor(this Color color)
        {
            return ColorUtility.ToHtmlStringRGB(color);
        }

        /// <summary>
        /// Generates a color from RGBA values ranging from 0 to 255.
        /// </summary>
        public static Color FromRGBA255(float r, float g, float b, float a)
        {
            const float delta = 1f / 255f;

            return new Color(delta * r, delta * g, delta * b, delta * a);
        }

        /// <summary>
        /// Generates a color from RGB values ranging from 0 to 255.
        /// </summary>
        public static Color FromRGB255(float r, float g, float b)
        {
            const float delta = 1f / 255f;

            return new Color(delta * r, delta * g, delta * b, 1);
        }

        /// <summary>
        /// Generates a greyscale (black and white) color from this color.
        /// </summary>
        // TODO: Could be nice to have a version with grayscale strenght
        public static Color ToGrayscale(this Color color)
        {
            float greyscale = color.r * 0.3f + color.g * 0.59f + color.b * 0.11f;

            return new Color(greyscale, greyscale, greyscale, color.a);
        }

        /// <summary>
        /// Keeping the current color alpha, generates a new color with the provided color RGB values.
        /// </summary>
        public static Color ToColorKeepingAlpha(this Color color, Color newColor)
        {
            newColor.a = color.a;
            return newColor;
        }
    }
}
