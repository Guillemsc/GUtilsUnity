using System;
using GUtilsUnity.Extensions;
using UnityEngine;

namespace GUtilsUnity.InputDetection
{
    public class ZoomDragInputDetection : MonoBehaviour
    {
        public event Action<float> OnZoom;
        public event Action<Vector2> OnDrag;

        float _previousDistance;
        Vector2 _previousDragCenter;

        void Update()
        {
            if (Input.touchCount <= 1)
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

            if (_previousDistance == 0)
            {
                _previousDistance = distance;
                _previousDragCenter = dragCenter;
            }

            var distanceDelta = distance - _previousDistance;
            var dragCenterDelta = dragCenter - _previousDragCenter;

            if (GUtils.Extensions.MathExtensions.IsEpsilonEqualsZero(distanceDelta))
            {
                OnZoom?.Invoke(distanceDelta);
            }

            if (Vector2Extensions.IsMagnitudeEpsilonEqualsZero(dragCenterDelta))
            {
                OnDrag?.Invoke(dragCenterDelta);
            }

            _previousDistance = distance;
            _previousDragCenter = dragCenter;
        }
    }
}
