using GUtilsUnity.Delegates.Generics;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GUtilsUnity.Pointer.Callbacks
{
    public sealed class ScrollPointerCallbacks : MonoBehaviour, IScrollHandler, IPointerUpHandler, IPointerDownHandler
    {
        [SerializeField, Min(0)] float _scaleWhenPointerScrolling = 0.01f;

        public event GenericEvent<ScrollPointerCallbacks, ScrollPointerEventData> OnScrolling;

        int _touchCount;
        bool _scrollingWithPointers;
        float _lastMagnitudeScrollingWithPointers;

        void OnApplicationFocus(bool hasFocus)
        {
            ForceStopScrolling();
        }

        void Update()
        {
            UpdatePointersScroll();
        }

        public void OnScroll(PointerEventData eventData)
        {
            OnScrolling?.Invoke(this, new ScrollPointerEventData()
            {
                ScrollMagnitude = eventData.scrollDelta.y,
                ScrollScreenPosition = eventData.position
            });
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            --_touchCount;

            TryStopScrollingWithPointers();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            ++_touchCount;

            TryStartScrollingWithPointers();
        }

        void TryStartScrollingWithPointers()
        {
            if (_scrollingWithPointers)
            {
                return;
            }

            if (!IsCorrectTouches())
            {
                return;
            }

            _scrollingWithPointers = true;

            TryGetCurrentScrollValue(out _lastMagnitudeScrollingWithPointers, out _);
        }

        void TryStopScrollingWithPointers()
        {
            if (!_scrollingWithPointers)
            {
                return;
            }

            if (IsCorrectTouches())
            {
                return;
            }

            _scrollingWithPointers = false;
            _lastMagnitudeScrollingWithPointers = 0f;
        }

        void ForceStopScrolling()
        {
            _scrollingWithPointers = false;
            _lastMagnitudeScrollingWithPointers = 0f;
            _touchCount = 0;
        }

        void UpdatePointersScroll()
        {
            if (!_scrollingWithPointers)
            {
                return;
            }

            if (!IsCorrectTouches())
            {
                return;
            }

            if (!TryGetCurrentScrollValue(out float currentMagnitude, out var position))
            {
                return;
            }

            float difference = currentMagnitude - _lastMagnitudeScrollingWithPointers;

            if (difference == 0)
            {
                return;
            }

            float scaledDifference = difference * _scaleWhenPointerScrolling;

            OnScrolling?.Invoke(this, new ScrollPointerEventData()
            {
                ScrollMagnitude = scaledDifference,
                ScrollScreenPosition = position
            });

            _lastMagnitudeScrollingWithPointers = currentMagnitude;
        }

        bool TryGetCurrentScrollValue(out float scroll, out Vector2 centerPosition)
        {
            if (!IsCorrectTouches() || Input.touchCount < 2)
            {
                scroll = default;
                centerPosition = default;
                return false;
            }

            Touch touch1 = Input.GetTouch(0);
            Touch touch2 = Input.GetTouch(1);

            var delta = touch1.position - touch2.position;
            scroll = delta.magnitude;
            centerPosition = touch2.position + delta / 2f;
            return true;
        }

        bool IsCorrectTouches() => _touchCount == 2;
    }
}
