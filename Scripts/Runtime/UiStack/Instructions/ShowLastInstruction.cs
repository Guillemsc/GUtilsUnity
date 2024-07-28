using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using GUtils.Extensions;
using GUtilsUnity.Sequencing.Instructions;
using GUtilsUnity.UiStack.UseCases;
using GUtilsUnity.Extensions;

namespace GUtilsUnity.UiStack.Instructions
{
    public sealed class ShowLastInstruction : Instruction
    {
        readonly List<object> _viewStackQueue;
        readonly bool _behindForeground;
        readonly bool _instantly;
        readonly ShowUseCase _showUseCase;

        public ShowLastInstruction(
            List<object> viewStackQueue,
            bool behindForeground,
            bool instantly,
            ShowUseCase showUseCase
            )
        {
            _viewStackQueue = viewStackQueue;
            _behindForeground = behindForeground;
            _instantly = instantly;
            _showUseCase = showUseCase;
        }

        protected override Task OnExecute(CancellationToken cancellationToken)
        {
            bool couldPop = _viewStackQueue.TryPop(out object entryId);

            if (!couldPop)
            {
                UnityEngine.Debug.LogError($"Tried to Show last entry, but view stack queue is empty. " +
                                           $"Maybe you wanted to use HideAndPush at some point, instead of just Hide.");
                return Task.CompletedTask;
            }

            return _showUseCase.Execute(entryId, _behindForeground, _instantly, cancellationToken);
        }
    }
}
