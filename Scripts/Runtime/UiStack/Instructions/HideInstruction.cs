using System.Threading;
using System.Threading.Tasks;
using GUtils.Tasks.Sequencing.Instructions;
using GUtilsUnity.UiStack.UseCases;

namespace GUtilsUnity.UiStack.Instructions
{
    public sealed class HideInstruction : Instruction
    {
        readonly object _entryId;
        readonly bool _pushToViewQueue;
        readonly bool _instantly;
        readonly HideUseCase _hideUseCase;

        public HideInstruction(
            object entryId,
            bool pushToViewQueue,
            bool instantly,
            HideUseCase hideUseCase
            )
        {
            _entryId = entryId;
            _pushToViewQueue = pushToViewQueue;
            _instantly = instantly;
            _hideUseCase = hideUseCase;
        }

        protected override Task OnExecute(CancellationToken cancellationToken)
        {
            return _hideUseCase.Execute(_entryId, _pushToViewQueue, _instantly, cancellationToken);
        }
    }
}
