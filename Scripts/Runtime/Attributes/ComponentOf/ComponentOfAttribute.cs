using UnityEngine;

namespace GUtilsUnity.Attributes
{
    /// <summary>
    /// Unity inspector attribute that, when placed on a <see cref="Component"/> member,
    /// tries to automatically set it from the current GameObject.
    /// </summary>
    public sealed class ComponentOfAttribute : PropertyAttribute
    {
        public string FieldName { get; }

        public ComponentOfAttribute(
            string fieldName)
        {
            FieldName = fieldName;
        }
    }
}
