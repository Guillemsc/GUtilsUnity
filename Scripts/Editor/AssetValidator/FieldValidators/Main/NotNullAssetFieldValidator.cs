using System.Reflection;
using GUtilsUnity.AssetValidation.Attributes;
using GUtilsUnity.AssetValidation.FieldValidators.Base;
using GUtilsUnity.Validation.Builder;
using UnityEngine;

namespace GUtilsUnity.AssetValidation.FieldValidators.Main
{
    [LinkValidator(typeof(NotNullFieldValidatorAttribute))]
    public sealed class NotNullAssetFieldValidator : IAssetFieldValidator
    {
        public void Validate(ref IValidationBuilder validationBuilder, Object asset, FieldInfo fieldInfo)
        {
            bool isUnityObject = typeof(Object).IsAssignableFrom(fieldInfo.FieldType);

            object referenceValue = fieldInfo.GetValue(asset);

            bool hasValue;

            if (isUnityObject)
            {
                Object referenceObject = referenceValue as Object;
                hasValue = referenceObject != null;
            }
            else
            {
                hasValue = referenceValue != null;
            }

            if (hasValue)
            {
                return;
            }

            validationBuilder.LogError("Reference is null");
        }
    }
}
