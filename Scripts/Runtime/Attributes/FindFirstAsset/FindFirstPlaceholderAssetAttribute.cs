namespace GUtilsUnity.Attributes.FindFirstAsset
{
    /// <summary>
    /// Same as <see cref="FindFirstAssetAttribute"/>, but tries to find assets by using "placeholder" as search string.
    /// </summary>
    /// <example> Given the following assets named "Configuration" and "PlaceholderConfiguration",
    /// would prioritize "PlaceholderConfiguration".</example>
    public sealed class FindFirstPlaceholderAssetAttribute : FindFirstAssetAttribute
    {
        public FindFirstPlaceholderAssetAttribute() : base("placeholder")
        {

        }
    }
}
