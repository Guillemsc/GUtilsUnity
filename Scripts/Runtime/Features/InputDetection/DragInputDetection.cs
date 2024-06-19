using System;
using UnityEngine;

namespace GUtilsUnity.InputDetection
{
    public class DragInputDetection : MonoBehaviour
    {
        public event Action<DragInputEventData> OnDragBegin;
        public event Action<DragInputEventData> OnDrag;
        public event Action<DragInputEventData> OnDragEnd;

        bool _previousActive;
        Vector2 _previousPosition;

        bool IsActive(out Vector2 position)
        {
#if UNITY_EDITOR
            if (Input.GetMouseButton(0))
            {
                position = Input.mousePosition;
                return true;
            }
#endif
            if (Input.touchCount == 1)
            {
                position = Input.GetTouch(0).position;
                return true;
            }

            position = default;
            return false;
        }

        void Update()
        {
            if (!IsActive(out var position))
            {
                if (_previousActive)
                {
                    OnDragEnd?.Invoke(new DragInputEventData()
                    {
                        Position = _previousPosition,
                        PositionDelta = Vector2.zero
                    });
                }

                _previousPosition = Vector3.zero;
                _previousActive = false;
                return;
            }

            var positionDelta = position - _previousPosition;

            if (!_previousActive)
            {
                _previousActive = true;
                OnDragBegin?.Invoke(new DragInputEventData()
                {
                    Position = position,
                    PositionDelta = Vector2.zero
                });
            }

            OnDrag?.Invoke(new DragInputEventData()
            {
                Position = position,
                PositionDelta = positionDelta
            });

            _previousPosition = position;
        }
    }
}
