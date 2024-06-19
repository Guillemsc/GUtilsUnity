using System.Reflection;
using GUtilsUnity.AssetValidation.Attributes;
using GUtilsUnity.AssetValidation.FieldValidators.Base;
using GUtilsUnity.Validation.Builder;
using UnityEngine;

namespace GUtilsUnity.AssetValidation.FieldValidators.Main
{
    [LinkValidator(typeof(LogFieldValidatorAttribute))]
    public sealed class LogAssetFieldValidator : IAssetFieldValidator
    {
        public void Validate(ref IValidationBuilder validationBuilder, Object asset, FieldInfo fieldInfo)
        {
            validationBuilder.LogInfo("Log field :)");
        }
    }
}
