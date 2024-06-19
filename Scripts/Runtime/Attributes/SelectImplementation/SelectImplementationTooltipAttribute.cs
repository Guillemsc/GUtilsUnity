using System;

namespace GUtilsUnity.Attributes.SelectImplementation
{
    public class SelectImplementationTooltipAttribute : Attribute
    {
        public string Tooltip { get; }

        public SelectImplementationTooltipAttribute(string tooltip)
        {
            Tooltip = tooltip;
        }
    }
}
