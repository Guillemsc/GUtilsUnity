using System;
using System.Threading;
using System.Threading.Tasks;
using GUtilsUnity.Tutorial.Events;

namespace GUtilsUnity.Tutorial.Steps
{
    public sealed class FinishCallbackTutorialStep : TutorialStep
    {
        readonly Action _onFinish;

        public FinishCallbackTutorialStep(Action onFinish)
        {
            _onFinish = onFinish;
        }

        public override Task Start(CancellationToken cancellationToken)
        {
            AddWhenFinish(_onFinish);

            Complete();

            return Task.CompletedTask;
        }

        [Obsolete]
        public override void Receive(ITutorialEvent tutorialEvent)
        {

        }
    }
}
