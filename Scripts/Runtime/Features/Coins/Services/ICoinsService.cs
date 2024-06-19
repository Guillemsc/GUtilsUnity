using GUtilsUnity.Events;
using UnityEngine;

namespace GUtilsUnity.Coins.Service
{
    public interface ICoinsService
    {
        IListenEvent<int> OnCoinAmountChanged { get; }
        int CoinAmount { get; }

        IAwardCoinHandle AwardCoins(int coinAmount);
        IAwardCoinHandle AwardCoins(int coinAmount, Vector2 screenPosition);

        bool TryPayCoins(int coinAmount, out IPayCoinHandle payCoinHandle);
        bool TryPayCoins(int coinAmount, Vector2 screenPosition, out IPayCoinHandle payCoinHandle);

        //void CompleteAllHandlesInstantly();
    }
}
