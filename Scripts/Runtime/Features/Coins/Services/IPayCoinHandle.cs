using System.Threading;
using System.Threading.Tasks;

namespace GUtilsUnity.Coins.Service
{
    public interface IPayCoinHandle
    {
        int CoinsPaid { get; }
        int CoinsAfterPay { get; }
        int CoinsBeforePay { get; }

        Task AwaitComplete(CancellationToken cancellationToken);
        IPayCoinHandle AllowComplete();
    }
}
