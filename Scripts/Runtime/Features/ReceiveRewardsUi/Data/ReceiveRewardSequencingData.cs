using GUtils.Tasks.Sequencing.Sequencer;

namespace GUtilsUnity.Features.ReceiveRewardsUi.Data
{
    public sealed class ReceiveRewardSequencingData
    {
        public ITaskSequencer MainSequencer { get; } = new TaskSequencer();
    }
}
