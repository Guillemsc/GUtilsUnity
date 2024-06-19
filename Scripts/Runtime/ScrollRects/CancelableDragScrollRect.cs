using GUtilsUnity.Extensions;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GUtilsUnity.ScrollRects
{
    public sealed class CancelableDragScrollRect : ScrollRect
    {
        bool _dragCanceled;

        void Update()
        {
            if (Input.GetMouseButtonUp(0))
            {
                ResumeDrag();
            }
        }

        public override void OnBeginDrag(PointerEventData eventData)
        {
            if (_dragCanceled)
            {
                return;
            }

            base.OnBeginDrag(eventData);
        }

        public override void OnDrag(PointerEventData eventData)
        {
            if (_dragCanceled)
            {
                return;
            }

            base.OnDrag(eventData);
        }

        public void CancelDrag()
        {
            if (_dragCanceled)
            {
                return;
            }

            _dragCanceled = true;

            gameObject.SetInteractable(false);

            PointerEventData forceEndDragData = new(EventSystem.current)
            {
                button = PointerEventData.InputButton.Left
            };

            OnEndDrag(forceEndDragData);
        }

        void ResumeDrag()
        {
            if (!_dragCanceled)
            {
                return;
            }

            _dragCanceled = false;

            gameObject.SetInteractable(true);
        }

        public void ResetDrag()
        {
            _dragCanceled = false;
            gameObject.SetInteractable(true);
        }
    }
}
