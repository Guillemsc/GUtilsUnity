using System.Reflection;
using GUtilsUnity.Validation.Builder;

namespace GUtilsUnity.AssetValidation.FieldValidators.Base
{
    public interface IAssetFieldValidator
    {
        void Validate(ref IValidationBuilder validationBuilder, UnityEngine.Object asset, FieldInfo fieldInfo);
    }
}
