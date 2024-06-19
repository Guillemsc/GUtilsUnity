using System;
using System.Threading;
using System.Threading.Tasks;
using GUtilsUnity.Logging.Enums;
using GUtilsUnity.Logging.Loggers;

namespace GUtilsUnity.Coins.Service
{
    public sealed class PayCoinHandle : IPayCoinHandle
    {
        public int CoinsPaid { get; }
        public int CoinsAfterPay { get; }
        public int CoinsBeforePay => CoinsAfterPay - CoinsPaid;

        readonly Func<CancellationToken, Task> _awaitCompleteFunc;
        readonly Action _allowCompleteAction;

        bool _completed;

        public PayCoinHandle(
            Func<CancellationToken, Task> awaitCompleteFunc,
            Action allowCompleteAction,
            int coinsPaid,
            int coinsAfterPay)
        {
            _awaitCompleteFunc = awaitCompleteFunc;
            _allowCompleteAction = allowCompleteAction;
            CoinsPaid = coinsPaid;
            CoinsAfterPay = coinsAfterPay;
        }

        public Task AwaitComplete(CancellationToken cancellationToken)
            => _awaitCompleteFunc.Invoke(cancellationToken);

        public IPayCoinHandle AllowComplete()
        {
            if (_completed)
            {
                DebugOnlyUnityLogger.Instance.Log(LogType.Error, "PayCoinHandle already completed, ignoring this request");
                return this;
            }

            _completed = true;
            _allowCompleteAction.Invoke();
            return this;
        }
    }
}
