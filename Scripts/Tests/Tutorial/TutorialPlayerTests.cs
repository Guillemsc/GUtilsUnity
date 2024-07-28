using System.Collections;
using System.Threading;
using System.Threading.Tasks;
using GUtilsUnity.Time.Timers;
using GUtilsUnity.Tutorial.Player;
using GUtilsUnity.Tutorial.Steps;
using NUnit.Framework;
using GUtilsUnity.Extensions;
using UnityEngine.TestTools;
using GUtils.Extensions;

namespace GUtilsUnity.Tutorial
{
    public sealed class TutorialPlayerTests
    {
        [UnityTest]
        public IEnumerator TutorialPlayerTests_StepsExecution()
        {
            int count = 0;

            void AddCount()
            {
                ++count;
            }

            ITutorialPlayer tutorialPlayer = new TutorialPlayer(
                new FinishCallbackTutorialStep(AddCount),
                new FinishCallbackTutorialStep(AddCount),
                new FinishCallbackTutorialStep(AddCount),
                new FinishCallbackTutorialStep(AddCount)
                );

            yield return tutorialPlayer.Play().ToCoroutine();

            Assert.True(count == 4);
        }

        [UnityTest]
        public IEnumerator TutorialPlayerTests_AsyncFinishStepsExecution()
        {
            int count = 0;

            async Task AddCount(bool instantly, CancellationToken cancellationToken)
            {
                await StopwatchTimer.Await(0.3f.ToSeconds(), cancellationToken);

                ++count;
            }

            ITutorialPlayer tutorialPlayer = new TutorialPlayer(
                new AsyncFinishCallbackTutorialStep(true, AddCount),
                new AsyncFinishCallbackTutorialStep(true, AddCount),
                new AsyncFinishCallbackTutorialStep(true, AddCount),
                new AsyncFinishCallbackTutorialStep(true, AddCount)
            );

            yield return tutorialPlayer.Play().ToCoroutine();

            Assert.True(count == 4);
        }

        [UnityTest]
        public IEnumerator TutorialPlayerTests_StopFinishingAsyncFinishStepsExecution()
        {
            int count = 0;

            ITutorialPlayer tutorialPlayer = null;

            async Task AddCount(bool instantly, CancellationToken cancellationToken)
            {
                tutorialPlayer.Stop(false).RunAsync();

                await StopwatchTimer.Await(0.3f.ToSeconds(), cancellationToken);

                ++count;
            }

            tutorialPlayer = new TutorialPlayer(
                new AsyncFinishCallbackTutorialStep(true, AddCount),
                new AsyncFinishCallbackTutorialStep(true, AddCount)
            );

            yield return tutorialPlayer.Play().ToCoroutine();

            Assert.True(count == 1);
        }

        [UnityTest]
        public IEnumerator TutorialPlayerTests_StopAsyncFinishStepsExecution()
        {
            int count = 0;

            async Task AddCount(bool instantly, CancellationToken cancellationToken)
            {
                await StopwatchTimer.Await(0.3f.ToSeconds(), cancellationToken);

                ++count;
            }

            ITutorialPlayer tutorialPlayer = new TutorialPlayer(
                new AsyncFinishCallbackTutorialStep(false, AddCount),
                new AsyncFinishCallbackTutorialStep(false, AddCount)
            );

            tutorialPlayer.Play().RunAsync();

            yield return tutorialPlayer.Stop(true).ToCoroutine();

            Assert.True(count == 1);
        }

        [UnityTest]
        public IEnumerator TutorialPlayerTests_ConditionalStepExecution()
        {
            int count = 0;

            async Task AddCount(bool instantly, CancellationToken cancellationToken)
            {
                await StopwatchTimer.Await(0.3f.ToSeconds(), cancellationToken);

                ++count;
            }

            ITutorialPlayer tutorialPlayer = new TutorialPlayer(
                new ConditionalTutorialStep(
                    () => false,
                    new AsyncFinishCallbackTutorialStep(true, AddCount)
                    ),
                new ConditionalTutorialStep(
                    () => true,
                    new AsyncFinishCallbackTutorialStep(true, AddCount)
                )
            );

            yield return tutorialPlayer.Play().ToCoroutine();

            Assert.True(count == 1);
        }
    }
}
