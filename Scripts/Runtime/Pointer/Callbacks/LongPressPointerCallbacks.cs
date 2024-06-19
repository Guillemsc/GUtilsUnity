using GUtilsUnity.Delegates.Generics;
using GUtilsUnity.Pointer.Configuration;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GUtilsUnity.Pointer.Callbacks
{
    public sealed class LongPressPointerCallbacks : MonoBehaviour
    {
        [Header("Setup")]
        [SerializeField] PointerCallbacks _pointerCallbacks;

        [Header("Configuration")]
        [SerializeField] LongPressPointerCallbacksConfiguration _pointerCallbacksConfiguration;

        PointerEventData _lastOnDownPointerEventData;
        float _nextLongPressActivationTime = float.PositiveInfinity;
        bool _canLongPressUp;

        public event GenericEvent<LongPressPointerCallbacks, PointerEventData> OnLongPressDown;
        public event GenericEvent<LongPressPointerCallbacks, PointerEventData> OnLongPressUp;

        void Awake()
        {
            RegisterPointerCallbacks();
        }

        void OnDestroy()
        {
            UnregisterPointerCallbacks();
        }

        void Update()
        {
            TryActivateLongPressDown();
        }

        void RegisterPointerCallbacks()
        {
            if (_pointerCallbacks == null)
            {
                UnityEngine.Debug.LogError($"Tried to register from {nameof(PointerCallbacks)}, but it was null, " +
                    $"at {nameof(LongPressPointerCallbacks)}", this);
                return;
            }

            _pointerCallbacks.OnDown += OnDown;
            _pointerCallbacks.OnUp += OnUp;
        }

        void UnregisterPointerCallbacks()
        {
            if (_pointerCallbacks == null)
            {
                UnityEngine.Debug.LogError($"Tried to unregister from {nameof(PointerCallbacks)}, but it was null, " +
                    $"at {nameof(LongPressPointerCallbacks)}", this);
                return;
            }

            _pointerCallbacks.OnDown -= OnDown;
            _pointerCallbacks.OnUp -= OnUp;
        }

        void OnDown(PointerCallbacks owner, PointerEventData data)
        {
            _lastOnDownPointerEventData = data;

            _nextLongPressActivationTime = UnityEngine.Time.unscaledTime + _pointerCallbacksConfiguration.LongPressActivationDelay;
        }

        void OnUp(PointerCallbacks owner, PointerEventData data)
        {
            _nextLongPressActivationTime = float.PositiveInfinity;

            TryActivateLongPressUp();
        }

        void TryActivateLongPressDown()
        {
            if (UnityEngine.Time.unscaledTime < _nextLongPressActivationTime)
            {
                return;
            }

            _nextLongPressActivationTime = float.PositiveInfinity;

            _canLongPressUp = true;

            OnLongPressDown?.Invoke(this, _lastOnDownPointerEventData);
        }

        void TryActivateLongPressUp()
        {
            if (!_canLongPressUp)
            {
                return;
            }

            _canLongPressUp = false;

            OnLongPressUp?.Invoke(this, _lastOnDownPointerEventData);
        }
    }
}
