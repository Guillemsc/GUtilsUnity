using GUtilsUnity.Haptics.TapticFeedback.Enums;

namespace GUtilsUnity.Haptics.TapticFeedback.Services
{
    /// <summary>
    /// Service for managing taptics, which is the vibration feedback produced by a device.
    /// </summary>
    public interface ITapticsService
    {
        /// <summary>
        /// Triggers a haptic impact with the specified strength.
        /// </summary>
        /// <param name="strength">The strength of the haptic impact.</param>
        void Impact(TapticImpactStrenght strength);
    }
}
