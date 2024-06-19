using DG.Tweening;
using GUtilsUnity.Attributes.FindFirstAsset;
using GUtilsUnity.TweenPlayers.Clips.Base;
using GUtilsUnity.TweenPlayers.Configurations.Popups;
using GUtilsUnity.Extensions;
using GUtilsUnity.Tweening.Extensions;
using UnityEngine;

namespace GUtilsUnity.TweenPlayers.Clips.Popups.SlideWithOverlay
{
    public sealed class HidePopupSlideWithOverlayTweenClip : MonoBehaviourTweenClip
    {
        [Header("References")]
        [Tooltip("Pivot that contains the Content and the Overlay. Used for active/interactable operations.")]
        public RectTransform Pivot;

        [Tooltip("Background overlay that will be used for fading.")]
        public RectTransform Overlay;

        [Tooltip("Content of the pivot itself. Will be the one that gets scaled.")]
        public RectTransform Content;

        [Header("Configuration")]
        [FindFirstDefaultAsset] public HidePopupSlideWithOverlayTweenClipConfiguration Configuration;

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
                Configuration.DisappearToSide,
                0f);
            var outOfScreenPosition = initialPosition + offsetToPlaceOutside;

            sequence.AppendCallback(() => Pivot.gameObject.SetInteractable(false));
            sequence.Append(Content.DOMove(outOfScreenPosition, Configuration.ContentSlideOutTweenConfiguration));
            sequence.Join(Content.gameObject.DOFade(0f, Configuration.ContentFadeOutTweenConfiguration));
            sequence.Join(Overlay.gameObject.DOFade(0f, Configuration.OverlayFadeOutTweenConfiguration));
            sequence.AppendCallback(() =>
            {
                Pivot.gameObject.SetActive(false);
                Content.position = initialPosition;
            });
        }
    }
}
