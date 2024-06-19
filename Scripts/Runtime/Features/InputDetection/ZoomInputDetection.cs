using System;
using UnityEngine;

namespace GUtilsUnity.InputDetection
{
    public class ZoomInputDetection : MonoBehaviour
    {
        public event Action<ZoomInputEventData> OnZoomBegin;
        public event Action<ZoomInputEventData> OnZoom;

        bool _previousActive;
        float _previousDistance;
        Vector2 _previousDragCenter;

        void Update()
        {
            if (Input.touches.Length != 2)
            {
                _previousDistance = 0f;
                _previousDragCenter = Vector3.zero;
                return;
            }

            var touch0 = Input.touches[0];
            var touch1 = Input.touches[1];

            var positionDelta = touch0.position - touch1.position;
            var distance = Vector2.Distance(touch0.position, touch1.position);
            var dragCenter = touch0.position - positionDelta / 2f;

            if (!_previousActive)
            {
                _previousDistance = distance;
                _previousDragCenter = dragCenter;

                OnZoomBegin?.Invoke(new ZoomInputEventData()
                {
                    CenterPosition = dragCenter,
                    CenterDelta = Vector2.zero,
                    ZoomDelta = 0f
                });
            }

            var distanceDelta = distance - _previousDistance;
            var dragCenterDelta = dragCenter - _previousDragCenter;

            OnZoom?.Invoke(new ZoomInputEventData()
            {
                CenterPosition = dragCenter,
                CenterDelta = dragCenterDelta,
                ZoomDelta = distanceDelta
            });

            _previousDistance = distance;
            _previousDragCenter = dragCenter;
        }
    }
}
