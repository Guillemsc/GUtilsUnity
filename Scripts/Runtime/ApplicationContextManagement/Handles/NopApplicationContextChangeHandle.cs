using System.Threading.Tasks;

namespace GUtilsUnity.ApplicationContextManagement
{
    public sealed class NopApplicationContextChangeHandle : IApplicationContextChangeHandle
    {
        public static readonly NopApplicationContextChangeHandle Instance = new();

        public bool HasFinishedPreStep => true;
        public bool IsUnblocked => true;
        public bool HasFinishedPreStepAndIsUnblocked => true;
        public bool IsComplete => true;

        NopApplicationContextChangeHandle()
        {

        }

        public Task WaitFinishPreStepAsync() => Task.CompletedTask;

        public Task WaitFinishBlockAsync() => Task.CompletedTask;

        public Task WaitFinishPreStepAndBlock() => Task.CompletedTask;
        public Task WaitCompleteAsync() => Task.CompletedTask;
        public IApplicationContextChangeHandle AllowComplete() => this;
    }
}
