using System;
using System.Threading;
using System.Threading.Tasks;
using GUtilsUnity.Logging.Enums;
using GUtilsUnity.Logging.Loggers;

namespace GUtilsUnity.Coins.Service
{
    public sealed class AwardCoinHandle : IAwardCoinHandle
    {
        public int CoinsAwarded { get; }
        public int CoinsBeforeAward => CoinsAfterAward - CoinsAwarded;
        public int CoinsAfterAward { get; }

        readonly Func<CancellationToken, Task> _awaitReachDisplayFunc;
        readonly Func<CancellationToken, Task> _awaitCompleteFunc;
        readonly Action _allowCompleteAction;

        bool _completed;

        public AwardCoinHandle(
            Func<CancellationToken, Task> awaitReachDisplayFunc,
            Func<CancellationToken, Task> awaitCompleteFunc,
            Action allowCompleteAction,
            int coinsAwarded,
            int coinsAfterAward)
        {
            _awaitReachDisplayFunc = awaitReachDisplayFunc;
            _awaitCompleteFunc = awaitCompleteFunc;
            _allowCompleteAction = allowCompleteAction;
            CoinsAwarded = coinsAwarded;
            CoinsAfterAward = coinsAfterAward;
        }

        public Task AwaitCoinsReachDisplay(CancellationToken cancellationToken)
        {
            return _awaitReachDisplayFunc.Invoke(cancellationToken);
        }

        public Task AwaitComplete(CancellationToken cancellationToken)
        {
            return _awaitCompleteFunc.Invoke(cancellationToken);
        }

        public IAwardCoinHandle AllowComplete()
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
