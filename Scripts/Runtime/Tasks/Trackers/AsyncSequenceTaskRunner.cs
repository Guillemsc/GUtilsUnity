#nullable enable
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using GUtils.Extensions;
using GUtilsUnity.Events;
using GUtilsUnity.Tasks.CompletionSources;
using GUtilsUnity.Events.Extensions;
using GUtilsUnity.Extensions;

namespace GUtilsUnity.Tasks.Runners
{
    public sealed class AsyncSequenceTaskRunner : IAsyncSequenceTaskRunner
    {
        readonly Queue<(TaskCompletionSource?, Func<CancellationToken, Task>)> _taskQueue = new();
        readonly Event<EventArgs> _onBeginRunning = new();
        readonly Event<EventArgs> _onEndRunning = new();

        CancellationTokenSource? _cancellationTokenSource;

        public IListenEvent<EventArgs> OnBeginRunning => _onBeginRunning;
        public IListenEvent<EventArgs> OnEndRunning => _onEndRunning;
        public bool IsRunning { get; private set; }


        public void Run(Func<CancellationToken, Task> func)
        {
            _taskQueue.Enqueue((null, func));

            TryRunInstructions();
        }

        public Task RunGetCompleteTask(Func<CancellationToken, Task> func)
        {
            TaskCompletionSource taskCompletionSource = new();
            _taskQueue.Enqueue((taskCompletionSource, func));

            TryRunInstructions();
            return taskCompletionSource.Task;
        }

        public void Cancel()
        {
            if (!IsRunning)
            {
                return;
            }

            IsRunning = false;

            var taskQueueCache = _taskQueue.ToArray();
            _taskQueue.Clear();

            _cancellationTokenSource!.Cancel();
            _cancellationTokenSource.Dispose();
            _cancellationTokenSource = null;

            foreach (var taskElement in taskQueueCache)
            {
                taskElement.Item1?.SetCanceled();
            }
        }

        void TryRunInstructions()
        {
            if (_taskQueue.Count == 0 || IsRunning)
            {
                return;
            }

            RunInstructionsAsync().RunAsync();
        }

        async Task RunInstructionsAsync()
        {
            IsRunning = true;

            _cancellationTokenSource = new CancellationTokenSource();
            CancellationToken cancellationToken = _cancellationTokenSource.Token;

            _onBeginRunning.Raise();

            while (_taskQueue.Count > 0)
            {
                (TaskCompletionSource? taskCompletionSource, Func<CancellationToken, Task> func) = _taskQueue.Peek();

                await func.Invoke(cancellationToken).AsCancellable();

                if (cancellationToken.IsCancellationRequested)
                {
                    //Return here because disposal is done on the Cancel method
                    return;
                }

                taskCompletionSource?.SetResult();
                _taskQueue.Dequeue();
            }

            _cancellationTokenSource.Dispose();
            _cancellationTokenSource = null;

            IsRunning = false;

            _onEndRunning.Raise();
        }
    }
}
