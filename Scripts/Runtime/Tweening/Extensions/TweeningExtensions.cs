using System;
using System.Collections.Generic;
using DG.Tweening;
using GUtilsUnity.Tweening.Configuration;
using GUtilsUnity.Extensions;
using UnityEngine;

namespace GUtilsUnity.Tweening.Extensions
{
    public static class TweeningExtensions
    {
        public static Tween DOContentSwapFading(
            GameObject toHide,
            GameObject toShow,
            NoValueTweenConfiguration noValueTweenConfiguration,
            float delayInBetween = 0f
        )
        {
            return DOContentSwapFading(
                new[] { toHide },
                new[] { toShow },
                noValueTweenConfiguration,
                delayInBetween
            );
        }

        public static Tween DOContentSwapFading(
            IReadOnlyList<GameObject> toHide,
            IReadOnlyList<GameObject> toShow,
            NoValueTweenConfiguration noValueTweenConfiguration,
            float delayInBetween = 0f
            )
        {
            return DOContentSwap(
                toHide,
                toShow,
                (s, g) => s.Join(g.DOFade(0f, noValueTweenConfiguration)),
                (s, g) => s.Join(g.DOFade(1f, noValueTweenConfiguration)),
                delayInBetween
            );
        }

        static Tween DOContentSwap(
            IReadOnlyList<GameObject> toHide,
            IReadOnlyList<GameObject> toShow,
            Action<Sequence, GameObject> addHideTweens,
            Action<Sequence, GameObject> addShowTweens,
            float delayInBetween = 0f
        )
        {
            Sequence sequence = DOTween.Sequence();

            Sequence showSequence = DOTween.Sequence();
            Sequence hideSequence = DOTween.Sequence();

            foreach (GameObject hide in toHide)
            {
                hideSequence.AppendGameObjectSetInteractable(hide, false);
                addHideTweens.Invoke(hideSequence, hide);
                hideSequence.AppendGameObjectSetActive(hide, false);
            }

            foreach (GameObject show in toShow)
            {
                showSequence.AppendGameObjectSetInteractable(show, true);
                showSequence.AppendGameObjectSetActive(show, true);
                addShowTweens.Invoke(showSequence, show);
                showSequence.AppendGameObjectSetInteractable(show, true);
            }

            sequence.Join(hideSequence);
            sequence.Append(showSequence.SetDelay(delayInBetween));

            return sequence;
        }
    }
}
