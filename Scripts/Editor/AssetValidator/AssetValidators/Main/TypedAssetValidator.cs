using System;
using GUtilsUnity.AssetValidation.Attributes;
using GUtilsUnity.AssetValidator.AssetValidators.Base;
using GUtilsUnity.Validation.Builder;
using UnityEngine;
using Object = UnityEngine.Object;

namespace GUtilsUnity.AssetValidator.AssetValidators.Main
{
    [LinkValidator(typeof(AssetValidatorAttribute))]
    public abstract class TypedAssetValidator<T> : IAssetValidator where T : ScriptableObject
    {
        public void Validate(ref IValidationBuilder validationBuilder, Object asset, AssetValidatorAttribute attribute)
        {
            Validate(ref validationBuilder, (T)asset, attribute);
        }

        protected abstract void Validate(ref IValidationBuilder validationBuilder, T asset, AssetValidatorAttribute attribute);
    }
}
