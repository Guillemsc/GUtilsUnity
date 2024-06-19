using UnityEngine;

namespace GUtilsUnity.Features.ConfetiUi.Interactors
{
    public interface IConfetiUiInteractor
    {
        void ResetPosition();
        void SetWorldPosition(Vector2 worldPosition);
        void Play();
        void ResetPositionAndPlay();
    }
}
