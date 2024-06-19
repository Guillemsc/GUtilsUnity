using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using GUtilsUnity.Features.ReceiveRewardsUi.Data;

namespace GUtilsUnity.Features.ReceiveRewardsUi.Interactors
{
    public sealed class NopReceiveRewardsUiInteractor : IReceiveRewardsUiInteractor
    {
        public static readonly NopReceiveRewardsUiInteractor Instance = new();

        NopReceiveRewardsUiInteractor()
        {

        }

        public void SetPetitions(IReadOnlyList<ReceiveRewardPetitionData> petitions) { }
        public Task PlayReceiveRewardsAnimation(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
