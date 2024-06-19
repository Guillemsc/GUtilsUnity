using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using GUtilsUnity.Features.ReceiveRewardsUi.Data;

namespace GUtilsUnity.Features.ReceiveRewardsUi.Interactors
{
    public interface IReceiveRewardsUiInteractor
    {
        void SetPetitions(IReadOnlyList<ReceiveRewardPetitionData> petitions);
        Task PlayReceiveRewardsAnimation(CancellationToken cancellationToken);
    }
}
