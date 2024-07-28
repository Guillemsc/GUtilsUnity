using GUtils.Events;
using GUtilsUnity.Features.InputUi.Data;
using GUtilsUnity.Pointer.Callbacks;

namespace GUtilsUnity.Features.InputUi.Interactors
{
    public sealed class NopDragAndScrollInputUiInteractor : IDragAndScrollInputUiInteractor
    {
        public static readonly NopDragAndScrollInputUiInteractor Instance = new();

        public IListenEvent<DragInputData> DragEvent => NopEvent<DragInputData>.Instance;
        public IListenEvent<ScrollPointerEventData> ScrollEvent => NopEvent<ScrollPointerEventData>.Instance;

        NopDragAndScrollInputUiInteractor()
        {

        }
    }
}
