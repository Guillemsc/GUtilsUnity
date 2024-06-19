using UnityEngine;

namespace GUtilsUnity.Attributes
{
    /// <summary>
    /// Unity inspector attribute that, when placed on a <see cref="Component"/> member,
    /// tries to automatically set the member value use UnityEngine.Object.FindObjectOfType
    /// </summary>
    public sealed class FindObjectOfTypeAttribute : PropertyAttribute
    {
    }
}
