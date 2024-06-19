using UnityEngine;

namespace GUtilsUnity.Features.ConfetiUi.Interactors
{
    public sealed class NopConfetiUiInteractor : IConfetiUiInteractor
    {
        public static readonly NopConfetiUiInteractor Instance = new();

        NopConfetiUiInteractor()
        {

        }

        public void ResetPosition() { }
        public void SetWorldPosition(Vector2 worldPosition) { }
        public void Play() { }
        public void ResetPositionAndPlay() { }
    }
}
