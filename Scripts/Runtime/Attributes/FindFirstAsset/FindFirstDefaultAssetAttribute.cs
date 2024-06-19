namespace GUtilsUnity.Attributes.FindFirstAsset
{
    /// <summary>
    /// Same as <see cref="FindFirstAssetAttribute"/>, but tries to find assets by using "default" as search string.
    /// </summary>
    public sealed class FindFirstDefaultAssetAttribute : FindFirstAssetAttribute
    {
        public FindFirstDefaultAssetAttribute() : base("default")
        {

        }
    }
}
