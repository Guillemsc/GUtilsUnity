using System;
using DG.Tweening;
using GUtilsUnity.Tweening.Configuration;
using GUtilsUnity.Extensions;
using GUtilsUnity.Tweening.Extensions;
using UnityEngine;

namespace GUtilsUnity.Tweening.Misc
{
    [Obsolete("This method is obsolete. Use TweenPlayer instead")]
    public sealed class PulseScalingTransform : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] Transform _transform;

        [Header("Tweens")]
        [SerializeField] Vector3TweenConfiguration _tweenConfiguration;

        void Start()
        {
            Sequence sequence = DOTween.Sequence();
            sequence.Append(_transform.DOScale(Vector3.one, 0f));
            sequence.Append(_transform.DOScale(_tweenConfiguration).SetMaxLoops());
            sequence.Play();
        }
    }
}
