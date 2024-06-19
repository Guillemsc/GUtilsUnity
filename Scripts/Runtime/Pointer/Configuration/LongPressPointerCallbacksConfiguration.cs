using UnityEngine;

namespace GUtilsUnity.Pointer.Configuration
{
    [CreateAssetMenu(fileName = nameof(LongPressPointerCallbacksConfiguration),
        menuName = "PopcoreCore/Configuration/" + nameof(LongPressPointerCallbacksConfiguration))]
    public sealed class LongPressPointerCallbacksConfiguration : ScriptableObject
    {
        [Tooltip("Delay in seconds it takes for a long press to trigger")]
        [SerializeField, Min(0)] private float longPressActivationDelay = 0.7f;

        public float LongPressActivationDelay => longPressActivationDelay;
    }
}
