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
    /// Shows the pivot by fading it out. While the animation is running, the pivot is not interactable.
    /// Should be used in conjunction <see cref="ShowUiFadingTweenClip"/>.
    /// </summary>
    public sealed class HideUiFadingTweenClip : MonoBehaviourTweenClip
    {
        [Header("References")]
        public RectTransform Pivot;

        [Header("Configuration")]
        [FindFirstDefaultAsset] public HideUiFadingTweenClipConfiguration Configuration;

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

            sequence.Append(Pivot.gameObject.DOSetVisibleFading(false, Configuration.PivotFadeOutTweenConfiguration));
        }
    }
}
