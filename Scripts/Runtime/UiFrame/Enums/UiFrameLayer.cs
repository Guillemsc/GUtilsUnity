using GUtilsUnity.Enums.Utils;

namespace GUtilsUnity.UiFrame.Layers
{
    /// Starting from the top, ordered from less to more priority.
    public enum UiFrameLayer
    {
        Default,
        Popup,
        LoadingScreen,
    }

    public sealed class UiFrameLayerInfo : EnumInfo<UiFrameLayer>
    {

    }
}
