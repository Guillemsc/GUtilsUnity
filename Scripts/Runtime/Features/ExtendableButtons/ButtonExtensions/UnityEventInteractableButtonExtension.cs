using UnityEngine.Events;
using UnityEngine.Serialization;

namespace GUtilsUnity.Features.ExtendableButtons.ButtonExtensions
{
    public sealed class UnityEventInteractableButtonExtension : ButtonExtension
    {
        public UnityEvent OnInteractableEnabled;
        public UnityEvent OnInteractableDisabled;
        public UnityEvent<bool> OnInteractableChanged;

        public override void WhenInteractable(bool isInteractable)
        {
            if (isInteractable)
            {
                OnInteractableEnabled.Invoke();
            }
            else
            {
                OnInteractableDisabled.Invoke();
            }

            OnInteractableChanged.Invoke(isInteractable);
        }
    }
}
