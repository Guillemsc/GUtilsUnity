using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GUtilsUnity.Extensions;

namespace GUtilsUnity.Runtime.Pipeline
{
    /// <summary>
    /// Provides a utility method to run a pipeline of <see cref="IValidateRunAsyncPipelineHandler"/> instances.
    /// </summary>
    public static class ValidateRunAsyncPipeline
    {
        /// <summary>
        /// Runs a pipeline of <see cref="IValidateRunAsyncPipelineHandler"/> instances.
        /// </summary>
        /// <param name="validateRunAsyncHandlers">The collection of <see cref="IValidateRunAsyncPipelineHandler"/>
        /// instances to run.</param>
        /// <param name="cancellationToken">The cancellation token to cancel the pipeline execution.</param>
        /// <returns>A task representing the asynchronous operation. The task will return <c>true</c> if all handlers
        /// in the pipeline were executed successfully, otherwise it will return <c>false</c>.</returns>
        public static async Task<bool> Run(
            IEnumerable<IValidateRunAsyncPipelineHandler> validateRunAsyncHandlers,
            CancellationToken cancellationToken
            )
        {
            var readOnlyCollectionHandlers = validateRunAsyncHandlers.ToReadOnlyCollection();

            foreach (var validateRunAsyncPipelineHandler in readOnlyCollectionHandlers)
            {
                if (!await validateRunAsyncPipelineHandler.ShouldExecute(cancellationToken))
                {
                    return false;
                }
            }

            foreach (var validateRunAsyncPipelineHandler in readOnlyCollectionHandlers)
            {
                await validateRunAsyncPipelineHandler.Execute(cancellationToken);
            }

            return true;
        }
    }
}
