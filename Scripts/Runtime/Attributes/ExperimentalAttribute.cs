using System;

namespace GUtilsUnity.Attributes
{
    /// <summary>
    /// This attribute is used to tag code that is work in progress and is subject to change.
    /// Feel free to use under the possibility of change or even removal.
    /// </summary>
    public sealed class ExperimentalAttribute : Attribute
    {
        public string Description { get; set; }
    }
}
