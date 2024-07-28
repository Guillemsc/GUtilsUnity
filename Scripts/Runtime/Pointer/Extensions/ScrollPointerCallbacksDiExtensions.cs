using System;
using GUtils.Di.Builder;
using GUtilsUnity.Pointer.Callbacks;

namespace GUtilsUnity.Pointer.Extensions
{
    public static class ScrollPointerCallbacksDiExtensions
    {
        public static IDiBindingActionBuilder<T> LinkScrollPointerCallbacksOnScrolling<T>(
            this IDiBindingActionBuilder<T> actionBuilder,
            ScrollPointerCallbacks dragPointerCallbacks,
            Func<T, Action<ScrollPointerEventData>> func
        )
        {
            Action<ScrollPointerEventData> action = null;

            void OnScrolling(ScrollPointerCallbacks scrollPointerCallbacks, ScrollPointerEventData scroll) => action?.Invoke(scroll);

            actionBuilder.WhenInit((c, o) =>
            {
                action = func.Invoke(o);
                dragPointerCallbacks.OnScrolling += OnScrolling;
            });

            actionBuilder.WhenDispose(() => { dragPointerCallbacks.OnScrolling -= OnScrolling; });

            actionBuilder.NonLazy();

            return actionBuilder;
        }
    }
}
