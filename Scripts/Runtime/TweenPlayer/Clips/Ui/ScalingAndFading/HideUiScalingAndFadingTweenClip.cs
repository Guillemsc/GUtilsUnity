using DG.Tweening;
using GUtilsUnity.Attributes.FindFirstAsset;
using GUtilsUnity.TweenPlayers.Clips.Base;
using GUtilsUnity.TweenPlayers.Configurations.Ui.ScalingAndFading;
using GUtilsUnity.Tweening.Extensions;
using UnityEngine;

namespace GUtilsUnity.TweenPlayers.Clips.Ui.ScalingAndFading
{
    /// <summary>
    /// Shows the pivot by scaling it up and fading it in. While the animation is running, the pivot is not interactable.
    /// Should be used in conjunction <see cref="ShowUiScalingAndFadingTweenClip"/>.
    /// </summary>
    public sealed class HideUiScalingAndFadingTweenClip : MonoBehaviourTweenClip
    {
        [Header("References")]
        public RectTransform Pivot;

        [Header("Configuration")]
        [FindFirstDefaultAsset] public HideUiScalingAndFadingTweenClipConfiguration Configuration;

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

            sequence.Append(Pivot.gameObject.DoHideScalingAndFading(
                Configuration.PivotScaleOutTweenConfiguration,
                Configuration.PivotFadeOutTweenConfiguration
            ));
        }
    }
}
