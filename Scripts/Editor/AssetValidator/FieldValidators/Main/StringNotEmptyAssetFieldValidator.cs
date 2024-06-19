using System.Reflection;
using GUtilsUnity.AssetValidation.Attributes;
using GUtilsUnity.AssetValidation.FieldValidators.Base;
using GUtilsUnity.Validation.Builder;
using UnityEngine;

namespace GUtilsUnity.AssetValidation.FieldValidators.Main
{
    [LinkValidator(typeof(StringNotEmptyFieldValidatorAttribute))]
    public sealed class StringNotEmptyAssetFieldValidator : IAssetFieldValidator
    {
        public void Validate(ref IValidationBuilder validationBuilder, Object asset, FieldInfo fieldInfo)
        {
            bool isString = typeof(string).IsAssignableFrom(fieldInfo.FieldType);

            if (!isString)
            {
                validationBuilder.LogWarning($"Trying to use a {typeof(string)} validator on a {fieldInfo.FieldType.Name} field");
                return;
            }

            string stringValue = fieldInfo.GetValue(asset) as string;

            if (string.IsNullOrEmpty(stringValue))
            {
                validationBuilder.LogError($"String should not be empty");
            }
        }
    }
}
