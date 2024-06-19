using UnityEngine;

namespace GUtilsUnity.Attributes
{
    /// <summary>
    /// Unity inspector attribute that, when placed on a <see cref="Component"/>,
    /// tries to automatically set it from the current GameObject, or its children.
    /// </summary>
    public sealed class ChildComponentOfAttribute : PropertyAttribute
    {
        public string FieldName { get; }

        public ChildComponentOfAttribute(
            string fieldName)
        {
            FieldName = fieldName;
        }
    }
}
