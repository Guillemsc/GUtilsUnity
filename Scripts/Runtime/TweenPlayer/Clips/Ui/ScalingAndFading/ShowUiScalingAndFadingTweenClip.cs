using DG.Tweening;
using GUtilsUnity.Attributes.FindFirstAsset;
using GUtilsUnity.TweenPlayers.Clips.Base;
using GUtilsUnity.TweenPlayers.Configurations.Ui.ScalingAndFading;
using GUtilsUnity.Tweening.Extensions;
using UnityEngine;

namespace GUtilsUnity.TweenPlayers.Clips.Ui.ScalingAndFading
{
    /// <summary>
    /// Hides the pivot by scaling it down and fading it out. While the animation is running, the pivot is not interactable.
    /// Should be used in conjunction with <see cref="HideUiScalingAndFadingTweenClip"/>.
    /// </summary>
    public sealed class ShowUiScalingAndFadingTweenClip : MonoBehaviourTweenClip
    {
        [Header("References")]
        public RectTransform Pivot;

        [Header("Configuration")]
        [FindFirstDefaultAsset] public ShowUiScalingAndFadingTweenClipConfiguration Configuration;

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

            sequence.Append(Pivot.gameObject.DoShowScalingAndFading(
                Configuration.PivotScaleInTweenConfiguration,
                Configuration.PivotFadeInTweenConfiguration
            ));
        }
    }
}
