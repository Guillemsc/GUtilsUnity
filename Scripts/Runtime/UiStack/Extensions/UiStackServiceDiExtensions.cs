using GUtilsUnity.Di.Builder;
using GUtilsUnity.Di.Delegates;
using GUtilsUnity.UiFrame.Layers;
using GUtilsUnity.UiStack.Entries;
using GUtilsUnity.UiStack.Services;
using GUtilsUnity.Visibility.Visibles;
using UnityEngine;

namespace GUtilsUnity.UiStack.Extensions
{
    public static class UiStackServiceDiExtensions
    {
        static IDiBindingActionBuilder<T> LinkToUiStackService<T>(
            this IDiBindingActionBuilder<T> actionBuilder,
            Transform transform,
            bool isPopup,
            UiFrameLayer layer
        )
            where T : IVisible, IUiStackElement
        {
            UiStackEntry uiStackEntry = null;

            actionBuilder.WhenInit((c, o) =>
            {
                IUiStackService uiStack = c.Resolve<IUiStackService>();

                uiStackEntry = new UiStackEntry(
                    o,
                    transform,
                    o,
                    isPopup
                );

                uiStack.Register(layer, uiStackEntry);
            });

            actionBuilder.WhenDispose((c, o) =>
            {
                IUiStackService uiStack = c.Resolve<IUiStackService>();

                uiStack.Unregister(uiStackEntry);
            });

            actionBuilder.NonLazy();

            return actionBuilder;
        }

        static IDiBindingActionBuilder<T> LinkToUiStackService<T>(
            this IDiBindingActionBuilder<T> actionBuilder,
            BindingResolverDelegate<IVisible> bindingResolverDelegate,
            Transform transform,
            bool isPopup,
            UiFrameLayer layer
            )
            where T : IUiStackElement
        {
            UiStackEntry uiStackEntry = null;

            actionBuilder.WhenInit((c, o) =>
            {
                IUiStackService uiStack = c.Resolve<IUiStackService>();

                var visible = bindingResolverDelegate.Invoke(c);

                uiStackEntry = new UiStackEntry(
                    o,
                    transform,
                    visible,
                    isPopup
                );

                uiStack.Register(layer, uiStackEntry);
            });

            actionBuilder.WhenDispose((c, o) =>
            {
                IUiStackService uiStack = c.Resolve<IUiStackService>();

                uiStack.Unregister(uiStackEntry);
            });

            actionBuilder.NonLazy();

            return actionBuilder;
        }

        public static IDiBindingActionBuilder<T> LinkUiToUiStackService<T>(
            this IDiBindingActionBuilder<T> actionBuilder,
            BindingResolverDelegate<IVisible> bindingResolverDelegate,
            Transform transform
        )
            where T : IUiStackElement
        {
            return actionBuilder.LinkToUiStackService(
                bindingResolverDelegate,
                transform,
                false,
                UiFrameLayer.Default
            );
        }

        public static IDiBindingActionBuilder<T> LinkUiToUiStackService<T>(
            this IDiBindingActionBuilder<T> actionBuilder,
            Transform transform
        )
            where T : IVisible, IUiStackElement
        {
            return actionBuilder.LinkToUiStackService(
                transform,
                false,
                UiFrameLayer.Default
            );
        }

        public static IDiBindingActionBuilder<T> LinkPopupToUiStackService<T>(
            this IDiBindingActionBuilder<T> actionBuilder,
            Transform transform
        )
            where T : IVisible, IUiStackElement
        {
            return actionBuilder.LinkToUiStackService(
                transform,
                true,
                UiFrameLayer.Popup
            );
        }

        public static IDiBindingActionBuilder<T> LinkPopupToUiStackService<T>(
            this IDiBindingActionBuilder<T> actionBuilder,
            BindingResolverDelegate<IVisible> bindingResolverDelegate,
            Transform transform
        )
            where T : IUiStackElement
        {
            return actionBuilder.LinkToUiStackService(
                bindingResolverDelegate,
                transform,
                true,
                UiFrameLayer.Popup
            );
        }

        public static IDiBindingActionBuilder<T> LinkLoadingScreenToUiStackService<T>(
            this IDiBindingActionBuilder<T> actionBuilder,
            Transform transform
        )
            where T : IVisible, IUiStackElement
        {
            return actionBuilder.LinkToUiStackService(
                transform,
                false,
                UiFrameLayer.LoadingScreen
            );
        }

        public static IDiBindingActionBuilder<T> LinkLoadingScreenToUiStackService<T>(
            this IDiBindingActionBuilder<T> actionBuilder,
            BindingResolverDelegate<IVisible> bindingResolverDelegate,
            Transform transform
        )
            where T : IUiStackElement
        {
            return actionBuilder.LinkToUiStackService(
                bindingResolverDelegate,
                transform,
                false,
                UiFrameLayer.LoadingScreen
            );
        }
    }
}
