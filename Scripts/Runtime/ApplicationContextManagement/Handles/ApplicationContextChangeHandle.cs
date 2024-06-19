using System.Threading.Tasks;

namespace GUtilsUnity.ApplicationContextManagement
{
    public class ApplicationContextChangeHandle : IApplicationContextChangeHandle
    {
        readonly TaskCompletionSource<object> _preStepTaskCompletionSource = new();
        readonly TaskCompletionSource<object> _blockTaskCompletionSource = new();

        Task _fullTask;

        public bool HasFinishedPreStep => _preStepTaskCompletionSource.Task.IsCompleted;
        public bool IsUnblocked => _blockTaskCompletionSource.Task.IsCompleted;
        public bool HasFinishedPreStepAndIsUnblocked => HasFinishedPreStep && IsUnblocked;
        public bool IsComplete => _fullTask.IsCompleted;

        public void SetFullTask(Task task)
        {
            _fullTask = task;
        }

        public bool TryFinishPreStep()
        {
            return _preStepTaskCompletionSource.TrySetResult(default);
        }

        public Task WaitFinishPreStepAsync()
        {
            return _preStepTaskCompletionSource.Task;
        }

        public Task WaitFinishBlockAsync()
        {
            return _blockTaskCompletionSource.Task;
        }

        public Task WaitFinishPreStepAndBlock()
        {
            return Task.WhenAll(_preStepTaskCompletionSource.Task, _blockTaskCompletionSource.Task);
        }

        public IApplicationContextChangeHandle AllowComplete()
        {
            _ = _blockTaskCompletionSource.TrySetResult(default);
            return this;
        }

        public Task WaitCompleteAsync()
        {
            return _fullTask;
        }
    }
}
