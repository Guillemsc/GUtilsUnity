using System.Collections.Generic;
using DG.Tweening;
using GUtilsUnity.TweenPlayers.Clips.Base;
using GUtilsUnity.Extensions;
using GUtilsUnity.Tweening.Extensions;
using UnityEngine;

namespace GUtilsUnity.TweenPlayers.Clips.Ui.MovingAndFading
{
    /// <summary>
    /// Hides some content elements instantly, preparing them to be shown by <see cref="ShowUiContentMovingAndFadingTweenClip"/>.
    /// </summary>
    public sealed class HideUiContentFadingInstantlyTweenClip : MonoBehaviourTweenClip
    {
        public List<RectTransform> Content;

        public override void Create(ref Sequence sequence)
        {
            Sequence hideSequence = DOTween.Sequence();

            foreach (RectTransform entry in Content)
            {
                if (entry == null)
                {
                    continue;
                }

                hideSequence.JoinGameObjectSetInteractable(entry.gameObject, false);
                hideSequence.Join(entry.gameObject.DOFade(0, 0f));
            }

            sequence.Append(hideSequence);
        }
    }
}
