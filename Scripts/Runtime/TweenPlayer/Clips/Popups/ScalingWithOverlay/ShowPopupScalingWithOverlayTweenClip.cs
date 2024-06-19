using DG.Tweening;
using GUtilsUnity.Attributes.FindFirstAsset;
using GUtilsUnity.TweenPlayers.Clips.Base;
using GUtilsUnity.TweenPlayers.Configurations.Popups;
using GUtilsUnity.Extensions;
using GUtilsUnity.Tweening.Extensions;
using UnityEngine;

namespace GUtilsUnity.TweenPlayers.Clips.Popups.ScalingWithOverlay
{
    public sealed class ShowPopupScalingWithOverlayTweenClip : MonoBehaviourTweenClip
    {
        [Header("References")]
        [Tooltip("Pivot that contains the Content and the Overlay. Used for active/interactable operations.")]
        public GameObject Pivot;

        [Tooltip("Background overlay that will be used for fading.")]
        public RectTransform Overlay;

        [Tooltip("Content of the pivot itself. Will be the one that gets scaled.")]
        public RectTransform Content;

        [Header("Configuration")]
        [FindFirstDefaultAsset] public ShowPopupScalingWithOverlayTweenClipConfiguration Configuration;

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

            sequence.AppendCallback(() => Pivot.SetActive(true));
            sequence.Append(Content.DOScale(Vector3.one, Configuration.ContentScaleUpTweenConfiguration));
            sequence.Join(Content.gameObject.DOFade(1f, Configuration.ContentFadeInTweenConfiguration));
            sequence.Join(Overlay.gameObject.DOFade(1f, Configuration.OverlayFadeInTweenConfiguration));
            sequence.AppendCallback(() => Pivot.SetInteractable(true));
        }
    }
}
