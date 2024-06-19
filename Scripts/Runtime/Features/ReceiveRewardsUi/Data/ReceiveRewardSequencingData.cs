using GUtilsUnity.Sequencing.Sequencer;

namespace GUtilsUnity.Features.ReceiveRewardsUi.Data
{
    public sealed class ReceiveRewardSequencingData
    {
        public ISequencer MainSequencer { get; } = new Sequencer();
    }
}
