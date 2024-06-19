using GUtilsUnity.AssetValidation.Attributes;
using GUtilsUnity.Validation.Builder;

namespace GUtilsUnity.AssetValidator.AssetValidators.Base
{
    public interface IAssetValidator
    {
        void Validate(ref IValidationBuilder validationBuilder, UnityEngine.Object asset, AssetValidatorAttribute attribute);
    }
}
