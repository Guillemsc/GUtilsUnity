using System;
using GUtilsUnity.Coins.Service;
using GUtilsUnity.Di.Builder;

namespace GUtilsUnity.Coins.Extensions
{
    public static class CoinsServiceDiContainerExtensions
    {
        public static IDiBindingActionBuilder<T> LinkToCoinsServiceChanged<T>(
            this IDiBindingActionBuilder<T> actionBuilder,
            Func<T, Action<int>> func
            )
        {
            Action<int> action = null;

            actionBuilder.WhenInit((c, o) =>
            {
                action = func.Invoke(o);

                ICoinsService coinsService = c.Resolve<ICoinsService>();
                coinsService.OnCoinAmountChanged.AddListener(action);
            });

            actionBuilder.WhenDispose((c, o) =>
            {
                ICoinsService coinsService = c.Resolve<ICoinsService>();
                coinsService.OnCoinAmountChanged.RemoveListener(action);
            });

            actionBuilder.NonLazy();

            return actionBuilder;
        }
    }
}
