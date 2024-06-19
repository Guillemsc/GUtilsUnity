using DG.Tweening;
using GUtilsUnity.Attributes.FindFirstAsset;
using GUtilsUnity.Attributes.Self;
using GUtilsUnity.Features.ExtendableButtons.Configurations;
using GUtilsUnity.Extensions;
using UnityEngine;

namespace GUtilsUnity.Features.ExtendableButtons.ButtonExtensions
{
    public sealed class ScaleWhenUpDownButtonExtension : ButtonExtension
    {
        [Self] public Transform Transform;
        [FindFirstDefaultAsset] public ScaleWhenUpDownButtonExtensionConfiguration Configuration;

        Tween _animationTween;

        public override void WhenDown()
        {
            if (Configuration == null)
            {
                return;
            }

            GetRectTransform();

            Vector3 scale = Configuration.DownScale.ToVector3();

            _animationTween?.Kill();
            _animationTween = Transform.DOScale(scale, Configuration.AnimationTime)
                .SetEase(Configuration.AnimationCurve)
                .Play();
        }

        public override void WhenUp()
        {
            GetRectTransform();

            _animationTween?.Kill();
            _animationTween = Transform.DOScale(1.ToVector3(), Configuration.AnimationTime)
                .SetEase(Configuration.AnimationCurve)
                .Play();
        }

        void GetRectTransform()
        {
            if (Transform != null)
            {
                return;
            }

            Transform = gameObject.GetComponent<Transform>();
        }
    }
}
