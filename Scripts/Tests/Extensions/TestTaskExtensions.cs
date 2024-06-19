using System;
using System.Threading;
using System.Threading.Tasks;
using GUtilsUnity.Tasks.CompletionSources;
using NUnit.Framework;

namespace GUtilsUnity.Extensions.Tests
{
    public class TestTaskExtensions
    {
        [Test]
        public void WaitAsync_WithDefaultCancellationToken_ReturnsOriginalTaskToOptimizeGc()
        {
            var taskCompletionSource = new TaskCompletionSource<object>();
            var waitTask = taskCompletionSource.Task.AwaitAsync(CancellationToken.None);

            Assert.That(waitTask, Is.EqualTo(taskCompletionSource.Task));
        }

        [Test]
        public void WaitAsync_WithCompletedTask_ReturnsOriginalTaskToOptimizeGc()
        {
            using var cancellationTokenSource = new CancellationTokenSource();
            var taskCompletionSource = new TaskCompletionSource<object>();

            taskCompletionSource.SetResult(default);

            var waitTask = taskCompletionSource.Task.AwaitAsync(cancellationTokenSource.Token);

            Assert.That(waitTask, Is.EqualTo(taskCompletionSource.Task));
        }

        [Test]
        public void WaitAsync_WithCanceledTask_ReturnsOriginalTaskToOptimizeGc()
        {
            using var cancellationTokenSource = new CancellationTokenSource();
            var taskCompletionSource = new TaskCompletionSource<object>();

            taskCompletionSource.SetCanceled();

            var waitTask = taskCompletionSource.Task.AwaitAsync(cancellationTokenSource.Token);

            Assert.That(waitTask, Is.EqualTo(taskCompletionSource.Task));
        }

        [Test]
        public void WaitAsync_WithCancelledToken_ReturnsCompletedTaskToOptimizeGc()
        {
            using var cancellationTokenSource = new CancellationTokenSource();
            var taskCompletionSource = new TaskCompletionSource<object>();

            cancellationTokenSource.Cancel();

            var waitTask = taskCompletionSource.Task.AwaitAsync(cancellationTokenSource.Token);

            Assert.That(waitTask, Is.EqualTo(Task.CompletedTask));
        }

        [Test]
        public void WaitAsync_CancelTokenAfterWait_CompletesTask()
        {
            using var cancellationTokenSource = new CancellationTokenSource();
            var taskCompletionSource = new TaskCompletionSource<object>();

            var waitTask = taskCompletionSource.Task.AwaitAsync(cancellationTokenSource.Token);

            cancellationTokenSource.Cancel();

            Assert.That(waitTask.IsCompleted);
        }

        [Test]
        public void WaitAsync_CancelTaskAfterWait_CompletesTask()
        {
            using var cancellationTokenSource = new CancellationTokenSource();
            var taskCompletionSource = new TaskCompletionSource<object>();

            var waitTask = taskCompletionSource.Task.AwaitAsync(cancellationTokenSource.Token);

            taskCompletionSource.SetCanceled();

            Assert.That(waitTask.IsCompleted);
        }

        [Test]
        public void WaitAsync_CompleteTaskAfterWait_CompletesTask()
        {
            using var cancellationTokenSource = new CancellationTokenSource();
            var taskCompletionSource = new TaskCompletionSource<object>();

            var waitTask = taskCompletionSource.Task.AwaitAsync(cancellationTokenSource.Token);

            taskCompletionSource.SetResult(default);

            Assert.That(waitTask.IsCompleted);
        }

        [Test]
        public void WhenAnyWithCancelRest_CompletesWhenOneIsCompletedAndCancelsRest()
        {
            TaskCompletionSource taskCompletionSource1 = new();
            TaskCompletionSource taskCompletionSource2 = new();

            Task Task1(CancellationToken ct)
            {
                taskCompletionSource1.LinkCancellationToken(ct);
                return taskCompletionSource1.Task;
            }

            Task Task2(CancellationToken ct)
            {
                taskCompletionSource2.LinkCancellationToken(ct);
                return taskCompletionSource2.Task;
            }

            Func<CancellationToken, Task>[] taskFuncs =
            {
                Task1,
                Task2
            };

            var task = TaskExtensions.WhenAnyWithCancelRest(taskFuncs, CancellationToken.None);

            taskCompletionSource1.SetResult();

            Assert.That(task.IsCompletedSuccessfully);
            Assert.That(taskCompletionSource2.Task.IsCanceled);
        }
    }
}
