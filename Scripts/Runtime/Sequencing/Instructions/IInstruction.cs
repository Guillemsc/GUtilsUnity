using System.Threading;
using System.Threading.Tasks;

namespace GUtilsUnity.Sequencing.Instructions
{
    public interface IInstruction
    {
        Task Execute(CancellationToken cancellationToken);
    }
}
