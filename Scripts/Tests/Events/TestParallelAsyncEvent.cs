using System.Threading;
using System.Threading.Tasks;
using GUtilsUnity.Tasks.CompletionSources;
using NUnit.Framework;

namespace GUtilsUnity.Events.Test
{
    [TestFixture]
    public class TestParallelAsyncEvent
    {
        [Test]
        public void Raise_WithTwoListeners_CallsAllListenersAndWaitsComplete()
        {
            var parallelAsyncEvent = new ParallelAsyncEvent<int>();

            bool first = false, second = false;
            var taskCompletionSource = new TaskCompletionSource();

            parallelAsyncEvent.AddListener((x, ct) =>
            {
                first = true;
                return taskCompletionSource.Task;
            });

            parallelAsyncEvent.AddListener((x, ct) =>
            {
                second = true;
                return Task.CompletedTask;
            });

            var task = parallelAsyncEvent.Raise(42, CancellationToken.None);

            Assert.That(first);
            Assert.That(second);
            Assert.That(!task.IsCompleted);

            taskCompletionSource.SetResult();

            Assert.That(task.IsCompleted);
        }
    }
}
