using UnityEngine;

namespace GUtilsUnity.ResSelector //TODO: Rename to Popcore.Core.Attributes
{
    /// <summary>
    /// ResSelectorAttribute is a custom attribute used to mark string properties in a MonoBehaviour for selection
    /// from a list of public constant strings defined in a specific class (in this case, the 'Res' class).
    ///
    /// When a string property in a MonoBehaviour/ScriptableObject is decorated with this attribute, the Unity editor shows a dropdown list
    /// instead of a standard string input. This list is populated with the public constants defined in the 'Res' class.
    ///
    /// Usage:
    ///
    /// public class MyScript : MonoBehaviour
    /// {
    ///     [ResSelector]
    ///     public string myResource;
    /// }
    ///
    /// In this example, 'myResource' will be selectable from a dropdown menu in the Unity inspector, containing
    /// the public constant string values from the 'Res' class. If the string value does not match any of the options,
    /// the dropdown will default to the "Invalid" selection.
    /// </summary>
    public sealed class ResSelectorAttribute : PropertyAttribute
    {
    }
}
