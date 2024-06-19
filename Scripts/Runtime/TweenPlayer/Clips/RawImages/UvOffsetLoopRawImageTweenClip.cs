using DG.Tweening;
using GUtilsUnity.Attributes.FindFirstAsset;
using GUtilsUnity.Attributes.Self;
using GUtilsUnity.TweenPlayers.Clips.Base;
using GUtilsUnity.TweenPlayers.Configurations.RawImages;
using GUtilsUnity.Extensions;
using GUtilsUnity.Tweening.Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace GUtilsUnity.TweenPlayers.Clips.RawImages
{
    /// <summary>
    /// Moves the uv x and y values of a RawImage.
    /// This clip already generates a looping animation.
    /// </summary>
    public sealed class UvOffsetLoopRawImageTweenClip : MonoBehaviourTweenClip
    {
        [Header("References")]
        [Self] public RawImage RawImage;

        [Header("Configuration")]
        [FindFirstDefaultAsset] public UvOffsetLoopRawImageTweenClipConfiguration Configuration;

        public override void Create(ref Sequence sequence)
        {
            if (RawImage == null)
            {
                return;
            }

            if (Configuration == null)
            {
                return;
            }

            Sequence loopSequence = DOTween.Sequence();
            loopSequence.Append(RawImage.DOUvRectX(0, 0));
            loopSequence.Join(RawImage.DOUvRectY(0, 0));
            loopSequence.Append(RawImage.DOUvRectX(Configuration.XFinalValue, Configuration.XDuration).SetEase(Ease.Linear)).SetMaxLoops();
            loopSequence.Join(RawImage.DOUvRectY(Configuration.YFinalValue, Configuration.YDuration).SetEase(Ease.Linear)).SetMaxLoops();

            sequence.Append(loopSequence);
        }
    }
}
