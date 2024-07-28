using GUtils.Extensions;
using GUtils.Tasks.CompletionSources;
using GUtils.Tasks.Runners;
using NUnit.Framework;
using GUtilsUnity.Extensions;

namespace GUtilsUnity.Tasks.Test
{
    [TestFixture]
    public class TestAsyncSequenceTaskRunner
    {
        [Test]
        public void RunWithTask_RunsTasksOneAfterTheOther_WhenPlaying()
        {
            AsyncSequenceTaskRunner runner = new();

            TaskCompletionSource source1 = new();
            TaskCompletionSource source2 = new();

            var task1 = runner.RunGetCompleteTask(ct => source1.Task);
            var task2 = runner.RunGetCompleteTask(ct => source2.Task);

            Assert.True(runner.IsRunning);
            Assert.False(task1.IsCompleted);
            Assert.False(task2.IsCompleted);

            source1.SetResult();

            Assert.True(runner.IsRunning);
            Assert.True(task1.IsCompleted);
            Assert.False(task2.IsCompleted);

            source2.SetResult();

            Assert.False(runner.IsRunning);
            Assert.True(task1.IsCompleted);
            Assert.True(task2.IsCompleted);
        }

        [Test]
        public void RunWithTask_CancelsTasks_AfterCanceling()
        {
            AsyncSequenceTaskRunner runner = new();

            TaskCompletionSource source1 = new();
            TaskCompletionSource source2 = new();

            var task1 = runner.RunGetCompleteTask(ct =>
            {
                source1.LinkCancellationToken(ct);
                return source1.Task;
            });

            var task2 = runner.RunGetCompleteTask(ct =>
            {
                source2.LinkCancellationToken(ct);
                return source2.Task;
            });

            Assert.True(runner.IsRunning);
            Assert.False(task1.IsCompleted);
            Assert.False(task2.IsCompleted);

            runner.Cancel();

            Assert.False(runner.IsRunning);
            Assert.True(task1.IsCompleted);
            Assert.True(task2.IsCompleted);
            Assert.True(task1.IsCanceled);
            Assert.True(task2.IsCanceled);
            Assert.True(source1.Task.IsCanceled);
            Assert.False(source2.Task.IsCanceled);
        }
    }
}
