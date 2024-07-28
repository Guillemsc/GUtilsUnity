using System.Threading;
using System.Threading.Tasks;
using GUtils.Tasks.Sequencing.Instructions;
using GUtilsUnity.UiStack.UseCases;

namespace GUtilsUnity.UiStack.Instructions
{
    public sealed class ShowInstruction : Instruction
    {
        readonly object _entryId;
        readonly bool _instantly;
        readonly ShowUseCase _showUseCase;

        public ShowInstruction(
            object entryId,
            bool instantly,
            ShowUseCase showUseCase
        )
        {
            _entryId = entryId;
            _instantly = instantly;
            _showUseCase = showUseCase;
        }

        protected override Task OnExecute(CancellationToken cancellationToken)
        {
            return _showUseCase.Execute(_entryId, false, _instantly, cancellationToken);
        }
    }
}
