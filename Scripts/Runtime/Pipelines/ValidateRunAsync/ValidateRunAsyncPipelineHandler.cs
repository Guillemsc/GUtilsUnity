using System;
using System.Threading;
using System.Threading.Tasks;

namespace GUtilsUnity.Runtime.Pipeline
{
    /// <inheritdoc />
    public class ValidateRunAsyncPipelineHandler : IValidateRunAsyncPipelineHandler
    {
        readonly Func<CancellationToken, Task<bool>> _shouldExecute;
        readonly Func<CancellationToken, Task> _execute;

        public ValidateRunAsyncPipelineHandler(Func<CancellationToken, Task<bool>> shouldExecute,
            Func<CancellationToken, Task> execute)
        {
            _shouldExecute = shouldExecute;
            _execute = execute;
        }

        public Task<bool> ShouldExecute(CancellationToken cancellationToken)
        {
            return _shouldExecute.Invoke(cancellationToken);
        }

        public Task Execute(CancellationToken cancellationToken)
        {
            return _execute.Invoke(cancellationToken);
        }
    }
}
