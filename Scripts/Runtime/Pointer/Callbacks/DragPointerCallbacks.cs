using GUtilsUnity.Delegates.Generics;
using GUtilsUnity.Pointer.Enums;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GUtilsUnity.Pointer.Callbacks
{
    public class DragPointerCallbacks : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerUpHandler, IPointerDownHandler
    {
        [SerializeField] bool _restartDraggingIfPossible = true;

        int _touchCount;

        DragPointerCallbacksEvents _state = DragPointerCallbacksEvents.End;

        public event GenericEvent<DragPointerCallbacks, PointerEventData> OnBegin;
        public event GenericEvent<DragPointerCallbacks, PointerEventData> OnDragging;
        public event GenericEvent<DragPointerCallbacks, PointerEventData> OnEnd;

        void OnApplicationFocus(bool hasFocus)
        {
            FroceStopDragging();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            --_touchCount;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            ++_touchCount;

            if (_touchCount <= 1)
            {
                return;
            }

            FroceStopDragging();
        }

        public void OnBeginDrag(PointerEventData pointerEventData)
        {
            TrySetState(DragPointerCallbacksEvents.Begin, pointerEventData);
        }

        public void OnDrag(PointerEventData pointerEventData)
        {
            if (_restartDraggingIfPossible)
            {
                TrySetState(DragPointerCallbacksEvents.Begin, pointerEventData);
            }

            TrySetState(DragPointerCallbacksEvents.Dragging, pointerEventData);
        }

        public void OnEndDrag(PointerEventData pointerEventData)
        {
            TrySetState(DragPointerCallbacksEvents.End, pointerEventData);
        }

        void TrySetState(DragPointerCallbacksEvents state, PointerEventData pointerEventData)
        {
            switch (state)
            {
                case DragPointerCallbacksEvents.Begin:
                    {
                        if (_touchCount > 1)
                        {
                            break;
                        }

                        if (_state == DragPointerCallbacksEvents.End)
                        {
                            _state = state;

                            OnBegin?.Invoke(this, pointerEventData);
                        }
                    }
                    break;

                case DragPointerCallbacksEvents.Dragging:
                    {
                        if (_touchCount > 1)
                        {
                            break;
                        }

                        if (_state == DragPointerCallbacksEvents.Begin || _state == DragPointerCallbacksEvents.Dragging)
                        {
                            _state = state;

                            OnDragging?.Invoke(this, pointerEventData);
                        }
                    }
                    break;

                case DragPointerCallbacksEvents.End:
                    {
                        if (_touchCount > 0)
                        {
                            break;
                        }

                        if (_state == DragPointerCallbacksEvents.Dragging)
                        {
                            _state = state;
                            OnEnd?.Invoke(this, pointerEventData);
                        }
                    }
                    break;
            }
        }

        void FroceStopDragging()
        {
            if (_state == DragPointerCallbacksEvents.End)
            {
                return;
            }

            _state = DragPointerCallbacksEvents.End;
            OnEnd?.Invoke(this, new PointerEventData(EventSystem.current));
        }
    }
}
