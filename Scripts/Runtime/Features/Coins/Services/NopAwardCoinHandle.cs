using System.Threading;
using System.Threading.Tasks;

namespace GUtilsUnity.Coins.Service
{
    public class NopAwardCoinHandle : IAwardCoinHandle
    {
        public static readonly NopAwardCoinHandle Instance = new();

        public int CoinsAwarded => 0;
        public int CoinsBeforeAward => 0;
        public int CoinsAfterAward => 0;

        NopAwardCoinHandle()
        {
        }

        public Task AwaitCoinsReachDisplay(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task AwaitComplete(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public IAwardCoinHandle AllowComplete()
        {
            return this;
        }
    }
}
