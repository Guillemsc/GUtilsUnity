using UnityEngine;

namespace GUtilsUnity.Extensions
{
    public static class TextureExtensions
    {
        /// <summary>
        /// Gets a Vector2Int represeting <see cref="Texture.width"/> and <see cref="Texture.height"/>.
        /// </summary>
        public static Vector2Int GetSize(this Texture texture)
        {
            return new Vector2Int(texture.width, texture.height);
        }
    }
}
