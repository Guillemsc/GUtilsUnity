using System.Threading;
using System.Threading.Tasks;

namespace GUtilsUnity.Runtime.Pipeline
{
    /// <summary>
    /// Represents a handler for validating and executing an asynchronous operation.
    /// </summary>
    public interface IValidateRunAsyncPipelineHandler
    {
        /// <summary>
        /// Determines whether the handler should be executed.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
        /// <returns>A task representing the asynchronous operation. The task will return <c>true</c>
        /// if the handler should be executed, otherwise it will return <c>false</c>.</returns>
        Task<bool> ShouldExecute(CancellationToken cancellationToken);

        /// <summary>
        /// Executes the validation.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task Execute(CancellationToken cancellationToken);
    }
}
