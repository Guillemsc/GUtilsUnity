using UnityEngine;

namespace GUtilsUnity.Extensions
{
    public static class MaterialExtensions
    {
        /// <summary>
        /// Sets some color property's alpha value.
        /// </summary>
        public static void SetAlpha(this Material material, string propertyId, float alpha)
        {
            Color color = material.GetColor(propertyId);
            color.a = alpha;
            material.SetColor(propertyId, color);
        }
    }
}
