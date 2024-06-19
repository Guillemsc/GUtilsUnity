using System;
using UnityEngine;

namespace GUtilsUnity.Attributes.SelectImplementation
{
    /// <summary>
    /// Unity inspector attribute that draws an editor that allows for the selection of child implementations
    /// of the type affected by the attribute.
    /// </summary>
    public sealed class SelectImplementationAttribute : PropertyAttribute
    {
        public bool DisplayLabel { get; }
        public bool ForceExpanded { get; }
        public Type[] IgnoreTypes { get; }

        public SelectImplementationAttribute()
            : this(true, false, Array.Empty<Type>())
        {
        }

        public SelectImplementationAttribute(
            bool displayLabel = true,
            bool forceExpanded = false,
            Type[] ignoreTypes = null)
        {
            DisplayLabel = displayLabel;
            ForceExpanded = forceExpanded;
            IgnoreTypes = ignoreTypes ?? Array.Empty<Type>();
        }
    }
}
