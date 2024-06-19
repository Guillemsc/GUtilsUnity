using UnityEngine;

namespace GUtilsUnity.Attributes.Self
{
    /// <summary>
    /// When added to a <see cref="Component"/> field marked as <see cref="SerializeField"/>, it tries
    /// to automatically fill the reference, trying to get it from the current <see cref="GameObject"/>.
    /// </summary>
    public class SelfAttribute : PropertyAttribute
    {
    }
}
