using System;
using System.Threading;
using System.Threading.Tasks;
using GUtilsUnity.Tasks.CompletionSources;
using NUnit.Framework;

namespace GUtilsUnity.Events.Test
{
    [TestFixture]
    public class TestSuccessiveAsyncEvent
    {
        [Test]
        public void Raise_WithTwoListeners_CallsListenersInOrderAndWaitsComplete()
        {
            var successiveAsyncEvent = new SuccessiveAsyncEvent<int>();

            bool first = false, second = false;
            var taskCompletionSource = new TaskCompletionSource();

            successiveAsyncEvent.AddListener((x, ct) =>
            {
                first = true;
                return taskCompletionSource.Task;
            });

            successiveAsyncEvent.AddListener((x, ct) =>
            {
                second = true;
                return Task.CompletedTask;
            });

            var task = successiveAsyncEvent.Raise(42, CancellationToken.None);

            Assert.That(first);
            Assert.That(!second);
            Assert.That(!task.IsCompleted);


            taskCompletionSource.SetResult();

            Assert.That(first);
            Assert.That(second);
            Assert.That(task.IsCompleted);
        }
    }
}
