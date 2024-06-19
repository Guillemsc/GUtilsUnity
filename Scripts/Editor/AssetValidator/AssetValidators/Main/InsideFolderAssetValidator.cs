using System.IO;
using GUtilsUnity.AssetValidation.Attributes;
using GUtilsUnity.AssetValidator.AssetValidators.Base;
using GUtilsUnity.Validation.Builder;
using UnityEditor;
using Object = UnityEngine.Object;

namespace GUtilsUnity.AssetValidator.AssetValidators.Main
{
    [LinkValidator(typeof(InsideFolderAssetValidatorAttribute))]
    public sealed class InsideFolderAssetValidator : IAssetValidator
    {
        public void Validate(ref IValidationBuilder validationBuilder, Object asset, AssetValidatorAttribute attribute)
        {
            InsideFolderAssetValidatorAttribute actualAttribute = (InsideFolderAssetValidatorAttribute)attribute;

            string assetPath = AssetDatabase.GetAssetPath(asset);
            string assetDirectory = Path.GetDirectoryName(assetPath);

            bool isCorrectDirectory = string.Equals(assetDirectory, actualAttribute.AssetsFolder);

            if (!isCorrectDirectory)
            {
                validationBuilder.LogError($"Asset on incorrect directory. Is at {assetDirectory}, and should be at {actualAttribute.AssetsFolder}");
            }
        }
    }
}
