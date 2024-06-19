using System;
using GUtilsUnity.AssetValidation.Attributes;
using GUtilsUnity.AssetValidator.AssetValidators.Base;
using GUtilsUnity.Validation.Builder;
using Object = UnityEngine.Object;

namespace GUtilsUnity.AssetValidator.AssetValidators.Main
{
    [LinkValidator(typeof(LogAssetValidatorAttribute))]
    public sealed class LogAssetValidator : IAssetValidator
    {
        public void Validate(ref IValidationBuilder validationBuilder, Object asset, AssetValidatorAttribute attribute)
        {
            validationBuilder.LogInfo("Log Asset :)");
        }
    }
}
