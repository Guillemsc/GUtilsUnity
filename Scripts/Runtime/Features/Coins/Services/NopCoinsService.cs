using GUtilsUnity.Events;
using UnityEngine;

namespace GUtilsUnity.Coins.Service
{
    public sealed class NopCoinsService : ICoinsService
    {
        public static readonly NopCoinsService Instance = new();

        public IListenEvent<int> OnCoinAmountChanged => NopEvent<int>.Instance;
        public int CoinAmount { get; }

        NopCoinsService()
        {

        }

        public IAwardCoinHandle AwardCoins(int coinAmount) => NopAwardCoinHandle.Instance;
        public IAwardCoinHandle AwardCoins(int coinAmount, Vector2 screenPosition) => NopAwardCoinHandle.Instance;

        public bool TryPayCoins(int coinAmount, out IPayCoinHandle payCoinHandle)
        {
            payCoinHandle = default;
            return false;
        }

        public bool TryPayCoins(int coinAmount, Vector2 screenPosition, out IPayCoinHandle payCoinHandle)
        {
            payCoinHandle = default;
            return false;
        }
    }
}
