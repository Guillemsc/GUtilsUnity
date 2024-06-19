using System;
using GUtilsUnity.Delegates.Generics;
using GUtilsUnity.Pointer.Enums;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GUtilsUnity.Pointer.Callbacks
{
    public class PointerCallbacks : MonoBehaviour, IPointerDownHandler, IPointerUpHandler,
        IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] bool _triggerPointerUpOnPointerExit = true;

        public PointerCallbacksPressState PressState { get; private set; } = PointerCallbacksPressState.Up;
        public PointerCallbacksPositionState PositionState { get; private set; } = PointerCallbacksPositionState.Out;

        public event GenericEvent<PointerCallbacks, PointerEventData> OnEnter;
        public event GenericEvent<PointerCallbacks, PointerEventData> OnExit;
        public event GenericEvent<PointerCallbacks, PointerEventData> OnDown;
        public event GenericEvent<PointerCallbacks, PointerEventData> OnUp;
        public event GenericEvent<PointerCallbacks, PointerEventData> OnClick;

        void OnApplicationFocus(bool hasFocus)
        {
            TrySetPressState(PointerCallbacksPressState.Up, new PointerEventData(EventSystem.current));
            TrySetPositionState(PointerCallbacksPositionState.Out, new PointerEventData(EventSystem.current));
        }

        public void OnPointerDown(PointerEventData pointerEventData)
        {
            TrySetPressState(PointerCallbacksPressState.Down, pointerEventData);
        }

        public void OnPointerUp(PointerEventData pointerEventData)
        {
            TrySetPressState(PointerCallbacksPressState.Up, pointerEventData);
            TrySetPositionState(PointerCallbacksPositionState.Out, pointerEventData);
        }

        public void OnPointerEnter(PointerEventData pointerEventData)
        {
            TrySetPositionState(PointerCallbacksPositionState.In, pointerEventData);
        }

        public void OnPointerExit(PointerEventData pointerEventData)
        {
            TrySetPositionState(PointerCallbacksPositionState.Out, pointerEventData);

            if (_triggerPointerUpOnPointerExit)
            {
                TrySetPressState(PointerCallbacksPressState.Up, pointerEventData);
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            OnClick?.Invoke(this, eventData);
        }

        void TrySetPressState(PointerCallbacksPressState pressState, PointerEventData pointerEventData)
        {
            switch (pressState)
            {
                case PointerCallbacksPressState.Up:
                {
                    if (PressState == PointerCallbacksPressState.Down)
                    {
                        PressState = pressState;
                        OnUp?.Invoke(this, pointerEventData);
                    }

                    break;
                }

                case PointerCallbacksPressState.Down:
                {
                    if (PressState == PointerCallbacksPressState.Up)
                    {
                        PressState = pressState;
                        OnDown?.Invoke(this, pointerEventData);
                    }

                    break;
                }

                default:
                {
                    throw new NotImplementedException(nameof(PointerCallbacksPressState));
                }
            }
        }

        void TrySetPositionState(PointerCallbacksPositionState positionState, PointerEventData pointerEventData)
        {
            switch (positionState)
            {
                case PointerCallbacksPositionState.In:
                {
                    if (PositionState == PointerCallbacksPositionState.Out)
                    {
                        PositionState = positionState;
                        OnEnter?.Invoke(this, pointerEventData);
                    }

                    break;
                }

                case PointerCallbacksPositionState.Out:
                {
                    if (PositionState == PointerCallbacksPositionState.In)
                    {
                        PositionState = positionState;
                        OnExit?.Invoke(this, pointerEventData);
                    }

                    break;
                }

                default:
                {
                    throw new NotImplementedException(nameof(PointerCallbacksPositionState));
                }
            }
        }
    }
}
