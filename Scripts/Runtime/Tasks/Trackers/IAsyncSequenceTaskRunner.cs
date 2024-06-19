#nullable enable
using System;
using System.Threading;
using System.Threading.Tasks;
using GUtilsUnity.Events;

namespace GUtilsUnity.Tasks.Runners
{
    public interface IAsyncSequenceTaskRunner
    {
        IListenEvent<EventArgs> OnBeginRunning { get; }
        IListenEvent<EventArgs> OnEndRunning { get; }

        Task RunGetCompleteTask(Func<CancellationToken, Task> func);
        void Run(Func<CancellationToken, Task> func);
        void Cancel();
    }
}
