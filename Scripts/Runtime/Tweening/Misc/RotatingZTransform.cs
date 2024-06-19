using System;
using DG.Tweening;
using GUtilsUnity.Tweening.Configuration;
using GUtilsUnity.Extensions;
using GUtilsUnity.Tweening.Extensions;
using UnityEngine;

namespace GUtilsUnity.Tweening.Misc
{
    [Obsolete("This method is obsolete. Use TweenPlayer instead")]
    public sealed class RotatingZTransform : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] Transform _transform;

        [Header("Tweens")]
        [SerializeField] RotationFloatTweenConfiguration _tweenConfiguration;

        void Start()
        {
            _transform.DOLocalRotateZ(_tweenConfiguration).SetMaxLoops().Play();
        }
    }
}
