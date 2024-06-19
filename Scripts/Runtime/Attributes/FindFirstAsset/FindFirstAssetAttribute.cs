using UnityEngine;

namespace GUtilsUnity.Attributes.FindFirstAsset
{
    /// <summary>
    /// When added to a <see cref="UnityEngine.Object"/> field marked as <see cref="SerializeField"/>, it tries
    /// to automatically fill the reference, by getting the first asset of the field type.
    /// It also works with prefabs.
    /// </summary>
    /// <example> Given the following assets named "Configuration" and "DefaultConfiguration",
    /// would prioritize "DefaultConfiguration".</example>
    public class FindFirstAssetAttribute : PropertyAttribute
    {
        public string SearchString { get; }

        public FindFirstAssetAttribute()
        {
            SearchString = string.Empty;
        }

        public FindFirstAssetAttribute(string searchString)
        {
            SearchString = searchString;
        }
    }
}
