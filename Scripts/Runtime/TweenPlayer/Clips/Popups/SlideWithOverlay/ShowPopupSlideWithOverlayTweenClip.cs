using DG.Tweening;
using GUtilsUnity.Attributes.FindFirstAsset;
using GUtilsUnity.TweenPlayers.Clips.Base;
using GUtilsUnity.TweenPlayers.Configurations.Popups;
using GUtilsUnity.Extensions;
using GUtilsUnity.Tweening.Extensions;
using UnityEngine;

namespace GUtilsUnity.TweenPlayers.Clips.Popups.SlideWithOverlay
{
    public sealed class ShowPopupSlideWithOverlayTweenClip : MonoBehaviourTweenClip
    {
        [Header("References")]
        [Tooltip("Pivot that contains the Content and the Overlay. Used for active/interactable operations.")]
        public RectTransform Pivot;

        [Tooltip("Background overlay that will be used for fading.")]
        public RectTransform Overlay;

        [Tooltip("Content of the pivot itself. Will be the one that gets scaled.")]
        public RectTransform Content;

        [Header("Configuration")]
        [FindFirstDefaultAsset] public ShowPopupSlideWithOverlayTweenClipConfiguration Configuration;

        public override void Create(ref Sequence sequence)
        {
            if (Pivot == null)
            {
                return;
            }

            if (Content == null)
            {
                return;
            }

            if (Overlay == null)
            {
                return;
            }

            if (Configuration == null)
            {
                return;
            }


            var initialPosition = Content.position;
            var offsetToPlaceOutside = Overlay.GetWorldOffsetToPlaceOutsideContainerRectTransform(
                Content,
                Configuration.AppearFromSide,
                0f);
            var outOfScreenPosition = initialPosition + offsetToPlaceOutside;

            sequence.AppendCallback(() =>
            {
                Pivot.gameObject.SetActive(true);
                Content.position = outOfScreenPosition;
            });
            sequence.Append(Content.DOMove(initialPosition, Configuration.ContentSlideInTweenConfiguration));
            sequence.Join(Content.gameObject.DOFade(1f, Configuration.ContentFadeInTweenConfiguration));
            sequence.Join(Overlay.gameObject.DOFade(1f, Configuration.OverlayFadeInTweenConfiguration));
            sequence.AppendCallback(() => Pivot.gameObject.SetInteractable(true));
        }
    }
}
