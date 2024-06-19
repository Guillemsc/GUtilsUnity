using System;

namespace GUtilsUnity.AssetValidation.Attributes
{
    public sealed class LinkValidatorAttribute : Attribute
    {
        public Type ProjectValidatorAttributeType { get; }

        public LinkValidatorAttribute(Type projectValidatorAttributeType)
        {
            ProjectValidatorAttributeType = projectValidatorAttributeType;
        }
    }
}
