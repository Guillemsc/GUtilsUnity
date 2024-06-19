using System;
using System.Collections.Generic;
using GUtilsUnity.Validation.Builder;

namespace GUtilsUnity.AssetValidator.Data
{
    public sealed class AssetValidationData
    {
        public string AssetPath { get; }
        public string AssetName { get; }
        public Type AssetType { get; }
        public IValidationBuilder ValidationBuilder { get; } = new ValidationBuilder();
        public List<FieldValidationData> FieldsValidation { get; } = new();

        public AssetValidationData(
            string assetPath,
            string assetName,
            Type assetType
        )
        {
            AssetPath = assetPath;
            AssetName = assetName;
            AssetType = assetType;
        }
    }
}
