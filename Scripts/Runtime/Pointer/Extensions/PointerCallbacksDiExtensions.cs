using System;
using GUtilsUnity.Di.Builder;
using GUtilsUnity.Pointer.Callbacks;
using UnityEngine.EventSystems;

namespace GUtilsUnity.Pointer.Extensions
{
    public static class PointerCallbacksDiExtensions
    {
        public static IDiBindingActionBuilder<T> LinkPointerCallbacksOnClick<T>(
            this IDiBindingActionBuilder<T> actionBuilder,
            PointerCallbacks pointerCallbacks,
            Func<T, Action<PointerEventData>> func
        )
        {
            Action<PointerEventData> action = null;

            void OnClick(PointerCallbacks _, PointerEventData pointerEventData)
                => action?.Invoke(pointerEventData);

            actionBuilder.WhenInit(o =>
            {
                action = func.Invoke(o);
                pointerCallbacks.OnClick += OnClick;
            });

            actionBuilder.WhenDispose(() => { pointerCallbacks.OnClick -= OnClick; });

            actionBuilder.NonLazy();

            return actionBuilder;
        }

        public static IDiBindingActionBuilder<T> LinkPointerCallbacksOnDown<T>(
            this IDiBindingActionBuilder<T> actionBuilder,
            PointerCallbacks pointerCallbacks,
            Func<T, Action<PointerEventData>> func
        )
        {
            Action<PointerEventData> action = null;

            void OnDown(PointerCallbacks _, PointerEventData pointerEventData)
                => action?.Invoke(pointerEventData);

            actionBuilder.WhenInit(o =>
            {
                action = func.Invoke(o);
                pointerCallbacks.OnDown += OnDown;
            });

            actionBuilder.WhenDispose(o => { pointerCallbacks.OnDown -= OnDown; });

            actionBuilder.NonLazy();

            return actionBuilder;
        }

        public static IDiBindingActionBuilder<T> LinkPointerCallbacksOnDown<T>(
            this IDiBindingActionBuilder<T> actionBuilder,
            PointerCallbacks pointerCallbacks,
            Func<T, Action> func
        )
        {
            Action action = null;

            void OnDown(PointerCallbacks _, PointerEventData pointerEventData)
                => action?.Invoke();

            actionBuilder.WhenInit(o =>
            {
                action = func.Invoke(o);
                pointerCallbacks.OnDown += OnDown;
            });

            actionBuilder.WhenDispose(o =>
            {
                pointerCallbacks.OnDown -= OnDown;
            });

            actionBuilder.NonLazy();

            return actionBuilder;
        }

        public static IDiBindingActionBuilder<T> LinkPointerCallbacksOnUp<T>(
            this IDiBindingActionBuilder<T> actionBuilder,
            PointerCallbacks pointerCallbacks,
            Func<T, Action<PointerEventData>> func
        )
        {
            Action<PointerEventData> action = null;

            void OnUp(PointerCallbacks _, PointerEventData pointerEventData)
                => action?.Invoke(pointerEventData);

            actionBuilder.WhenInit(o =>
            {
                action = func.Invoke(o);
                pointerCallbacks.OnUp += OnUp;
            });

            actionBuilder.WhenDispose(() => { pointerCallbacks.OnUp -= OnUp; });

            actionBuilder.NonLazy();

            return actionBuilder;
        }

        public static IDiBindingActionBuilder<T> LinkPointerCallbacksOnUp<T>(
            this IDiBindingActionBuilder<T> actionBuilder,
            PointerCallbacks pointerCallbacks,
            Func<T, Action> func
        )
        {
            Action action = null;

            void OnUp(PointerCallbacks _, PointerEventData pointerEventData)
                => action?.Invoke();

            actionBuilder.WhenInit(o =>
            {
                action = func.Invoke(o);
                pointerCallbacks.OnUp += OnUp;
            });

            actionBuilder.WhenDispose(o =>
            {
                pointerCallbacks.OnUp -= OnUp;
            });

            actionBuilder.NonLazy();

            return actionBuilder;
        }

        public static IDiBindingActionBuilder<T> LinkPointerCallbacksOnExit<T>(
            this IDiBindingActionBuilder<T> actionBuilder,
            PointerCallbacks pointerCallbacks,
            Func<T, Action<PointerEventData>> func
        )
        {
            Action<PointerEventData> action = null;

            void OnExit(PointerCallbacks _, PointerEventData pointerEventData)
                => action?.Invoke(pointerEventData);

            actionBuilder.WhenInit(o =>
            {
                action = func.Invoke(o);
                pointerCallbacks.OnExit += OnExit;
            });

            actionBuilder.WhenDispose(() => { pointerCallbacks.OnExit -= OnExit; });

            actionBuilder.NonLazy();

            return actionBuilder;
        }

        public static IDiBindingActionBuilder<T> LinkPointerCallbacksOnEnter<T>(
            this IDiBindingActionBuilder<T> actionBuilder,
            PointerCallbacks pointerCallbacks,
            Func<T, Action<PointerEventData>> func
        )
        {
            Action<PointerEventData> action = null;

            void OnEnter(PointerCallbacks _, PointerEventData pointerEventData)
                => action?.Invoke(pointerEventData);

            actionBuilder.WhenInit(o =>
            {
                action = func.Invoke(o);
                pointerCallbacks.OnEnter += OnEnter;
            });

            actionBuilder.WhenDispose(() => { pointerCallbacks.OnEnter -= OnEnter; });

            actionBuilder.NonLazy();

            return actionBuilder;
        }
    }
}
