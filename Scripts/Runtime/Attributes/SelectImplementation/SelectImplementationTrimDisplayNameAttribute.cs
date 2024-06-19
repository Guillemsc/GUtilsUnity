using System;

namespace GUtilsUnity.Attributes.SelectImplementation
{
    public class SelectImplementationTrimDisplayNameAttribute : Attribute
    {
        public string TrimDisplayNameValue { get; }

        public SelectImplementationTrimDisplayNameAttribute(string trimDisplayNameValue)
        {
            TrimDisplayNameValue = trimDisplayNameValue;
        }
    }
}
