using UnityEngine;

namespace GUtilsUnity.Features.ExtendableButtons.ButtonExtensions
{
    public abstract class ButtonExtension : MonoBehaviour
    {
        public bool ExtensionEnabled = true;

        public virtual void WhenEnable() { }
        public virtual void WhenStart() { }
        public virtual void WhenDisable() { }
        public virtual void WhenDown() { }
        public virtual void WhenUp() { }
        public virtual void WhenClick() { }
        public virtual void WhenInteractable(bool isInteractable) { }
    }
}
