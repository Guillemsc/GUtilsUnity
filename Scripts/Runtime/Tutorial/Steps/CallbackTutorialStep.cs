using System;
using System.Threading;
using System.Threading.Tasks;
using GUtilsUnity.Tutorial.Events;

namespace GUtilsUnity.Tutorial.Steps
{
    public sealed class CallbackTutorialStep : TutorialStep
    {
        readonly Action _callback;

        public CallbackTutorialStep(
            Action callback
            )
        {
            _callback = callback;
        }

        public override Task Start(CancellationToken cancellationToken)
        {
            _callback?.Invoke();
            Complete();
            return Task.CompletedTask;
        }

        [Obsolete]
        public override void Receive(ITutorialEvent tutorialEvent)
        {

        }
    }
}
