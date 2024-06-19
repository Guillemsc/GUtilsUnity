using DG.Tweening;
using GUtilsUnity.Attributes.FindFirstAsset;
using GUtilsUnity.TweenPlayers.Clips.Base;
using GUtilsUnity.TweenPlayers.Configurations.Ui.Fading;
using GUtilsUnity.Extensions;
using GUtilsUnity.Tweening.Extensions;
using UnityEngine;

namespace GUtilsUnity.TweenPlayers.Clips.Ui.Fading
{
    /// <summary>
    /// Shows the pivot by fading it in. While the animation is running, the pivot is not interactable.
    /// Should be used in conjunction <see cref="HideUiFadingTweenClip"/>.
    /// </summary>
    public sealed class ShowUiFadingTweenClip : MonoBehaviourTweenClip
    {
        [Header("References")]
        public RectTransform Pivot;

        [Header("Configuration")]
        [FindFirstDefaultAsset] public ShowUiFadingTweenClipConfiguration Configuration;

        public override void Create(ref Sequence sequence)
        {
            if (Pivot == null)
            {
                return;
            }

            if (Configuration == null)
            {
                return;
            }

            sequence.Append(Pivot.gameObject.DOSetVisibleFading(
                true,
                Configuration.PivotFadeInTweenConfiguration
            ));
        }
    }
}
