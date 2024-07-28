using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using GUtilsUnity.Delegates.Animation;
using GUtilsUnity.Tutorial.Events;
using GUtilsUnity.Tutorial.Steps;
using TaskExtensions = GUtils.Extensions.TaskExtensions;

namespace GUtilsUnity.Tutorial.Player
{
    public sealed class TutorialPlayer : ITutorialPlayer
    {
        IReadOnlyList<ITutorialStep> _tutorialSteps;

        int _runningTutorialStepIndex;
        ITutorialStep _currentTutorialStep;

        bool _stopped;

        CancellationTokenSource _currentTaskCancellationTokenSource;
        TaskCompletionSource<object> _playingTaskCompletitionSource;

        public TutorialPlayer(params ITutorialStep[] tutorialSteps)
        {
            _tutorialSteps = tutorialSteps;
        }

        public Task Play(params ITutorialStep[] tutorialSteps)
        {
            return Play((IReadOnlyList<ITutorialStep>)tutorialSteps);
        }

        public async Task Play(IReadOnlyList<ITutorialStep> tutorialSteps)
        {
            await Stop(true);

            _tutorialSteps = tutorialSteps;

            await PlaySteps();
        }

        public async Task Play()
        {
            await Stop(true);

            await PlaySteps();
        }

        public Task Stop(bool instantly)
        {
            _stopped = true;

            if (_currentTutorialStep == null)
            {
                return _playingTaskCompletitionSource != null ? _playingTaskCompletitionSource.Task : Task.CompletedTask;
            }

            ITutorialStep stepToFinish = _currentTutorialStep;
            _currentTutorialStep = null;

            _currentTaskCancellationTokenSource.Cancel();

            return Task.WhenAll(
                _playingTaskCompletitionSource.Task,
                FinishStep(stepToFinish, instantly)
                );
        }

        async Task PlaySteps()
        {
            _runningTutorialStepIndex = 0;
            _stopped = false;
            _currentTaskCancellationTokenSource = new CancellationTokenSource();

            _playingTaskCompletitionSource = new TaskCompletionSource<object>();

            foreach (ITutorialStep tutorialStep in _tutorialSteps)
            {
                tutorialStep.Reset();
            }

            while (_runningTutorialStepIndex < _tutorialSteps.Count)
            {
                if (_stopped)
                {
                    break;
                }

                _currentTutorialStep = _tutorialSteps[_runningTutorialStepIndex];

                await _currentTutorialStep.Start(_currentTaskCancellationTokenSource.Token);

                if (_stopped)
                {
                    break;
                }

                await TaskExtensions.AwaitUntil(() => _currentTutorialStep == null || _currentTutorialStep.Completed, _currentTaskCancellationTokenSource.Token);

                if (_stopped)
                {
                    break;
                }

                ITutorialStep stepToFinish = _currentTutorialStep;
                _currentTutorialStep = null;

                await FinishStep(stepToFinish, false);

                ++_runningTutorialStepIndex;
            }

            _currentTaskCancellationTokenSource.Dispose();
            _playingTaskCompletitionSource.SetResult(default);
        }

        async Task FinishStep(ITutorialStep stepToFinish, bool instantly)
        {
            List<Task> tasks = new List<Task>();

            foreach (TaskAnimationEvent whenFinish in stepToFinish.WhenFinishAnimated)
            {
                tasks.Add(whenFinish.Invoke(instantly, CancellationToken.None));
            }

            await Task.WhenAll(tasks);

            foreach (Action whenFinish in stepToFinish.WhenFinish)
            {
                whenFinish.Invoke();
            }
        }

        public void SendEvent(ITutorialEvent tutorialEvent)
        {
            _currentTutorialStep?.Receive(tutorialEvent);
        }
    }
}
