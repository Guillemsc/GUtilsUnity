using System;
using System.Threading;
using System.Threading.Tasks;
using GUtilsUnity.Delegates.Animation;
using GUtilsUnity.Tutorial.Events;

namespace GUtilsUnity.Tutorial.Steps
{
    public sealed class AsyncFinishCallbackTutorialStep : TutorialStep
    {
        readonly bool _autocomplete;
        readonly TaskAnimationEvent _onFinish;

        public AsyncFinishCallbackTutorialStep(bool autocomplete, TaskAnimationEvent onFinish)
        {
            _autocomplete = autocomplete;
            _onFinish = onFinish;
        }

        public override Task Start(CancellationToken cancellationToken)
        {
            AddWhenFinish(_onFinish);

            if (_autocomplete)
            {
                Complete();
            }

            return Task.CompletedTask;
        }

        [Obsolete]
        public override void Receive(ITutorialEvent tutorialEvent)
        {

        }
    }
}
