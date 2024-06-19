using UnityEngine;

namespace GUtilsUnity.Attributes
{
    // TODO: I don't think this is working properly :)
    public sealed class NoFoldoutAttribute : PropertyAttribute
    {
        public bool DisplayName { get; }

        public NoFoldoutAttribute(bool displayName = true)
        {
            DisplayName = displayName;
        }
    }
}
