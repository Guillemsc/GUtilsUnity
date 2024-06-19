using DG.Tweening;
using GUtilsUnity.Attributes.FindFirstAsset;
using GUtilsUnity.Features.ExtendableButtons.Configurations;
using GUtilsUnity.Extensions;

namespace GUtilsUnity.Features.ExtendableButtons.ButtonExtensions
{
    public sealed class BounceUntilInteractionButtonExtension : ButtonExtension
    {
        [FindFirstDefaultAsset] public BounceUntilInteractionButtonExtensionConfiguration Configuration;

        Sequence _animationTween;

        public override void WhenEnable()
        {
            StartBounce();
        }

        public override void WhenDown()
        {
            StopBounce();
        }

        public void StartBounce()
        {
            if (Configuration == null)
            {
                return;
            }

            _animationTween?.Kill();
            _animationTween = DOTween.Sequence();

            _animationTween.Append(transform
                .DOScale(Configuration.Scale, Configuration.AnimationTime)
                .SetMaxLoops()
                .SetEase(Configuration.AnimationCurve)
            );

            _animationTween.Play();
        }

        public void StopBounce()
        {
            _animationTween?.Kill();
        }
    }
}
