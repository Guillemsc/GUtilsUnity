namespace GUtilsUnity.AssetValidation.Attributes
{
    public sealed class InsideFolderAssetValidatorAttribute : AssetValidatorAttribute
    {
        public string AssetsFolder { get; }

        public InsideFolderAssetValidatorAttribute(string assetsFolder)
        {
            AssetsFolder = assetsFolder;
        }
    }
}
