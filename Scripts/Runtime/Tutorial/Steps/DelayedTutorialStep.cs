using System;
using System.Threading;
using System.Threading.Tasks;
using GUtilsUnity.Time.Timers;
using GUtilsUnity.Tutorial.Events;
using GUtilsUnity.Tutorial.Player;

namespace GUtilsUnity.Tutorial.Steps
{
    public sealed class DelayedTutorialStep : TutorialStep
    {
        readonly TimeSpan _timeSpan;
        readonly ITutorialPlayer _tutorialPlayer;

        public DelayedTutorialStep(
            TimeSpan timeSpan,
            params ITutorialStep[] tutorialSteps
        )
        {
            _timeSpan = timeSpan;
            _tutorialPlayer = new TutorialPlayer(tutorialSteps);
        }

        public override async Task Start(CancellationToken cancellationToken)
        {
            await ScaledUnityTimer.Await(_timeSpan, cancellationToken);

            if (cancellationToken.IsCancellationRequested)
            {
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
