using System.Threading;
using System.Threading.Tasks;

namespace GUtilsUnity.Sequencing.Instructions
{
    public abstract class Instruction : IInstruction
    {
        public Task Execute(CancellationToken cancellationToken)
        {
            if(cancellationToken.IsCancellationRequested)
            {
                return Task.CompletedTask;
            }

            return OnExecute(cancellationToken);
        }

        protected abstract Task OnExecute(CancellationToken cancellationToken);
    }
}
