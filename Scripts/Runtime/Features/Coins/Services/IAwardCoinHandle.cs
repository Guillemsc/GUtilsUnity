using System.Threading;
using System.Threading.Tasks;

namespace GUtilsUnity.Coins.Service
{
    public interface IAwardCoinHandle
    {
        int CoinsAwarded { get; }
        int CoinsBeforeAward { get; }
        int CoinsAfterAward { get; }

        Task AwaitCoinsReachDisplay(CancellationToken cancellationToken);
        Task AwaitComplete(CancellationToken cancellationToken);
        IAwardCoinHandle AllowComplete();
    }
}
