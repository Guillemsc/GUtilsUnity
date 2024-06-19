using UnityEngine;

namespace GUtilsUnity.Extensions
{
    public static class ScreenExtensions
    {
        /// <summary>
        /// Vector2 containing <see cref="Screen.width"/> and <see cref="Screen.height"/>.
        /// </summary>
        public static Vector2 Size => new(Screen.width, Screen.height);

        /// <summary>
        /// Vector2 containing half <see cref="Screen.width"/> and half <see cref="Screen.height"/>.
        /// </summary>
        public static Vector2 HalfSize => new(Screen.width / 2f, Screen.height / 2f);

        /// <summary>
        /// Vector2 containing a third of <see cref="Screen.width"/> and a third of <see cref="Screen.height"/>.
        /// </summary>
        public static Vector2 ThirdSize => new(Screen.width / 3f, Screen.height / 3f);

        /// <summary>
        /// Vector2 containing fourth of <see cref="Screen.width"/> and fourth of <see cref="Screen.height"/>.
        /// </summary>
        public static Vector2 FourthSize => new(Screen.width / 4f, Screen.height / 4f);

        /// <summary>
        /// Converts a screen position to a viewport position.
        /// </summary>
        /// <param name="screenPosition">The position in screen coordinates to convert.</param>
        /// <returns>The position in viewport coordinates.</returns>
        public static Vector2 FromScreenToViewPortPosition(this Vector2 screenPosition)
            => new(screenPosition.x / Screen.width, screenPosition.y / Screen.height);

        /// <summary>
        /// Converts a viewport position to a screen position.
        /// </summary>
        /// <param name="viewportPosition">The position in viewport coordinates to convert.</param>
        /// <returns>The position in screen coordinates.</returns>
        public static Vector2 FromViewportToScreenPosition(this Vector2 viewportPosition)
            => new(viewportPosition.x * Screen.width, viewportPosition.y * Screen.height);
    }
}
