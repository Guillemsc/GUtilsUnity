using System;

namespace GUtilsUnity.Attributes.SelectImplementation
{
    public class SelectImplementationCustomDisplayNameAttribute : Attribute
    {
        public string CustomDisplayName { get; }

        public SelectImplementationCustomDisplayNameAttribute(string customDisplayName)
        {
            CustomDisplayName = customDisplayName;
        }
    }
}
