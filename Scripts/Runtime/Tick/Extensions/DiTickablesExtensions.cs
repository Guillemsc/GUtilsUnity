using System;
using GUtilsUnity.Di.Builder;
using GUtilsUnity.Tick.Enums;
using GUtilsUnity.Tick.Services;
using GUtilsUnity.Tick.Tickables;

namespace GUtilsUnity.Tick.Extensions
{
    public static class DiTickablesExtensions
    {
        public static IDiBindingActionBuilder<T> LinkToTickablesService<T>(
            this IDiBindingActionBuilder<T> actionBuilder,
            TickType tickType = TickType.Update
            )
            where T : ITickable
        {
            actionBuilder.WhenInit((c, o) =>
            {
                ITickablesService tickablesService = c.Resolve<ITickablesService>();

                tickablesService.Add(o, tickType);
            });

            actionBuilder.WhenDispose((c, o) =>
            {
                ITickablesService tickablesService = c.Resolve<ITickablesService>();

                tickablesService.RemoveNow(o, tickType);
            });

            actionBuilder.NonLazy();

            return actionBuilder;
        }

        public static IDiBindingActionBuilder<T> LinkToTickablesService<T>(
            this IDiBindingActionBuilder<T> actionBuilder,
            Func<T, Action> func,
            TickType tickType = TickType.Update
            )
        {
            CallbackTickable callbackTickable = null;

            actionBuilder.WhenInit((c, o) =>
            {
                Action action = func.Invoke(o);

                callbackTickable = new CallbackTickable(action);

                ITickablesService tickablesService = c.Resolve<ITickablesService>();

                tickablesService.Add(callbackTickable, tickType);
            });

            actionBuilder.WhenDispose((c, o) =>
            {
                ITickablesService tickablesService = c.Resolve<ITickablesService>();

                tickablesService.RemoveNow(callbackTickable, tickType);
            });

            actionBuilder.NonLazy();

            return actionBuilder;
        }

        public static IDiBindingActionBuilder<T> LinkConditionalInvokeToTickablesService<T>(
            this IDiBindingActionBuilder<T> builder,
            Func<bool> condition,
            Action<T> action,
            TickType tickType = TickType.Update
        )
        {
            ITickable tickable = default;

            builder.WhenInit((c, o) =>
            {
                tickable = new ConditionalInvokeTickable(
                    condition,
                    () => action.Invoke(o)
                );

                c.Resolve<ITickablesService>().Add(tickable, tickType);
            });

            builder.WhenDispose((c, o) =>
            {
                c.Resolve<ITickablesService>().Remove(tickable, tickType);
            });
            
            return builder;
        }
    }
}
