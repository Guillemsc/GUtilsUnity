using System;
using GUtils.Di.Builder;
using GUtilsUnity.Pointer.Callbacks;
using UnityEngine.EventSystems;

namespace GUtilsUnity.Pointer.Extensions
{
    public static class DragPointerCallbacksDiExtensions
    {
        public static IDiBindingActionBuilder<T> LinkDragPointerCallbacksOnBegin<T>(
            this IDiBindingActionBuilder<T> actionBuilder,
            DragPointerCallbacks dragPointerCallbacks,
            Func<T, Action<PointerEventData>> func
        )
        {
            Action<PointerEventData> action = null;

            void OnBegin(DragPointerCallbacks dragPointerCallbacks, PointerEventData pointerEventData) => action?.Invoke(pointerEventData);

            actionBuilder.WhenInit((c, o) =>
            {
                action = func.Invoke(o);
                dragPointerCallbacks.OnBegin += OnBegin;
            });

            actionBuilder.WhenDispose((c, o) => { dragPointerCallbacks.OnBegin -= OnBegin; });

            actionBuilder.NonLazy();

            return actionBuilder;
        }

        public static IDiBindingActionBuilder<T> LinkDragPointerCallbacksOnDragging<T>(
            this IDiBindingActionBuilder<T> actionBuilder,
            DragPointerCallbacks dragPointerCallbacks,
            Func<T, Action<PointerEventData>> func
        )
        {
            Action<PointerEventData> action = null;

            void OnDragging(DragPointerCallbacks dragPointerCallbacks, PointerEventData pointerEventData) => action?.Invoke(pointerEventData);

            actionBuilder.WhenInit((c, o) =>
            {
                action = func.Invoke(o);
                dragPointerCallbacks.OnDragging += OnDragging;
            });

            actionBuilder.WhenDispose((c, o) => { dragPointerCallbacks.OnBegin -= OnDragging; });

            actionBuilder.NonLazy();

            return actionBuilder;
        }

        public static IDiBindingActionBuilder<T> LinkDragPointerCallbacksOnEnd<T>(
            this IDiBindingActionBuilder<T> actionBuilder,
            DragPointerCallbacks dragPointerCallbacks,
            Func<T, Action<PointerEventData>> func
        )
        {
            Action<PointerEventData> action = null;

            void OnEnd(DragPointerCallbacks dragPointerCallbacks, PointerEventData pointerEventData) => action?.Invoke(pointerEventData);

            actionBuilder.WhenInit((c, o) =>
            {
                action = func.Invoke(o);
                dragPointerCallbacks.OnEnd += OnEnd;
            });

            actionBuilder.WhenDispose((c, o) => { dragPointerCallbacks.OnBegin -= OnEnd; });

            actionBuilder.NonLazy();

            return actionBuilder;
        }
    }
}
