using GUtilsUnity.AssetValidation.Attributes;
using GUtilsUnity.AssetValidator.AssetValidators.Main;
using GUtilsUnity.Validation.Builder;
using UnityEngine;

namespace Tests
{
    public sealed class ValidableMonoBehaviourValidator : TypedAssetValidator<ScriptableObject>
    {
        protected override void Validate(ref IValidationBuilder validationBuilder, ScriptableObject asset, AssetValidatorAttribute attribute)
        {
            validationBuilder.LogInfo("Hello world");
        }
    }
}
