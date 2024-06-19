using UnityEngine;
using UnityEngine.UI;

namespace GUtilsUnity.Features.ExtendableButtons.ButtonExtensions
{
    public sealed class ChangeColorInteractableButtonExtension : ButtonExtension
    {
        public Image Image;
        public Color ActiveColor;
        public Color InactiveColor;

        public override void WhenInteractable(bool isInteractable)
        {
            Image.color = isInteractable
                ? ActiveColor
                : InactiveColor;
        }
    }
}
