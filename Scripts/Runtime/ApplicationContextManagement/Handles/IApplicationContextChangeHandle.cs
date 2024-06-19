using System.Threading.Tasks;

namespace GUtilsUnity.ApplicationContextManagement
{
    public interface IApplicationContextChangeHandle
    {
        bool HasFinishedPreStep { get; }
        bool IsUnblocked { get; }
        bool HasFinishedPreStepAndIsUnblocked { get; }
        bool IsComplete { get; }

        Task WaitFinishPreStepAsync();
        Task WaitFinishBlockAsync();
        Task WaitFinishPreStepAndBlock();
        Task WaitCompleteAsync();

        IApplicationContextChangeHandle AllowComplete();
    }
}
