using GUtilsUnity.Haptics.TapticFeedback.Enums;

namespace GUtilsUnity.Haptics.TapticFeedback.Services
{
    public sealed class NopTapticsService : ITapticsService
    {
        public static readonly NopTapticsService Instance = new();

        NopTapticsService()
        {

        }

        public void Impact(TapticImpactStrenght strenght) { }
    }
}
