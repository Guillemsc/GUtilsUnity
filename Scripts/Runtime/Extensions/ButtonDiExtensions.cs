using System;
using GUtils.Di.Builder;
using GUtils.Di.Container;
using UnityEngine.UI;

namespace GUtilsUnity.Extensions
{
    public static class ButtonDiExtensions
    {
        public static IDiContainerBuilder LinkButtonClick(
            this IDiContainerBuilder builder,
            Button button,
            Action action
        )
        {
            builder.WhenInit(o =>
            {
                button.onClick.AddListener(action.Invoke);
            });

            builder.WhenDispose(() =>
            {
                button.onClick.RemoveListener(action.Invoke);
            });

            return builder;
        }

        [System.Obsolete("This method is obsolete. Use LinkButtonClick instead.")]
        public static IDiContainerBuilder LinkButton(
            this IDiContainerBuilder builder,
            Button button,
            Func<IDiResolveContainer, Action> func
        )
        {
            return builder.LinkButtonClick(button, func);
        }

        public static IDiContainerBuilder LinkButtonClick(
            this IDiContainerBuilder builder,
            Button button,
            Func<IDiResolveContainer, Action> func
        )
        {
            Action action = null;

            builder.WhenBuild(c =>
            {
                action = func.Invoke(c);

                button.onClick.AddListener(action.Invoke);
            });

            builder.WhenDispose(c =>
            {
                button.onClick.RemoveListener(action.Invoke);
            });

            return builder;
        }

        [System.Obsolete("This method is obsolete. Use LinkButtonClick instead.")]
        public static IDiBindingActionBuilder<T> LinkButton<T>(
            this IDiBindingActionBuilder<T> actionBuilder,
            Button button,
            Func<T, Action> func
        )
        {
            return actionBuilder.LinkButtonClick(button, func);
        }

        public static IDiBindingActionBuilder<T> LinkButtonClick<T>(
            this IDiBindingActionBuilder<T> actionBuilder,
            Button button,
            Func<T, Action> func
        )
        {
            Action action = null;

            actionBuilder.WhenInit(o =>
            {
                action = func.Invoke(o);

                button.onClick.AddListener(action.Invoke);
            });

            actionBuilder.WhenDispose(() =>
            {
                button.onClick.RemoveListener(action.Invoke);
            });

            actionBuilder.NonLazy();

            return actionBuilder;
        }

        [System.Obsolete("This method is obsolete. Use LinkButtonClickOnce instead.")]
        public static IDiBindingActionBuilder<T> LinkButtonOnce<T>(
            this IDiBindingActionBuilder<T> actionBuilder,
            Button button,
            Func<T, Action> func
        )
        {
            return actionBuilder.LinkButtonClickOnce(button, func);
        }

        public static IDiBindingActionBuilder<T> LinkButtonClickOnce<T>(
            this IDiBindingActionBuilder<T> actionBuilder,
            Button button,
            Func<T, Action> func
        )
        {
            Action action = null;
            bool clicked = false;

            void OnClick()
            {
                if (clicked)
                {
                    return;
                }

                clicked = true;

                action.Invoke();
            }

            actionBuilder.WhenInit((c, o) =>
            {
                action = func.Invoke(o);

                button.onClick.AddListener(OnClick);
            });

            actionBuilder.WhenDispose((c, o) =>
            {
                button.onClick.RemoveListener(OnClick);
            });

            actionBuilder.NonLazy();

            return actionBuilder;
        }
    }
}
