using GUtilsUnity.UiFrame.Layers;
using GUtilsUnity.UiStack.Builder;
using GUtilsUnity.UiStack.Entries;

namespace GUtilsUnity.UiStack.Services
{
    public sealed class NopUiStackService : IUiStackService
    {
        public static readonly NopUiStackService Instance = new();

        NopUiStackService()
        {

        }

        public void Register(UiStackEntry entry) { }
        public void Register(UiFrameLayer layer, UiStackEntry entry) { }
        public void Unregister(UiStackEntry entry) { }
        public void SetNotInteractableNow<T>(T instance) { }
        public IUiStackSequenceBuilder New() => NopUiStackSequenceBuilder.Instance;
    }
}
