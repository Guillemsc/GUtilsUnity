using System;
using System.Threading;
using System.Threading.Tasks;
using GUtilsUnity.Tutorial.Events;
using GUtilsUnity.Tutorial.Player;

namespace GUtilsUnity.Tutorial.Steps
{
    public sealed class ConditionalTutorialStep : TutorialStep
    {
        readonly Func<bool> _conditionFunc;
        readonly ITutorialPlayer _tutorialPlayer;

        public ConditionalTutorialStep(
            Func<bool> conditionFunc,
            params ITutorialStep[] tutorialSteps
            )
        {
            _conditionFunc = conditionFunc;
            _tutorialPlayer = new TutorialPlayer(tutorialSteps);
        }

        public override async Task Start(CancellationToken cancellationToken)
        {
            bool canPlay = _conditionFunc.Invoke();

            if (!canPlay)
            {
                Complete();
                return;
            }

            cancellationToken.Register(() =>
            {
                AddWhenFinish(Stop);
            });

            await _tutorialPlayer.Play();

            Complete();
        }

        [Obsolete]
        public override void Receive(ITutorialEvent tutorialEvent)
        {
            _tutorialPlayer.SendEvent(tutorialEvent);
        }

        Task Stop(bool instantly, CancellationToken cancellationToken)
        {
            return _tutorialPlayer.Stop(instantly);
        }
    }
}
