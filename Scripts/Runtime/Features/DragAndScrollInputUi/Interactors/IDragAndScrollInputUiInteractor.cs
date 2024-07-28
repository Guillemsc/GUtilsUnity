using GUtils.Events;
using GUtilsUnity.Features.InputUi.Data;
using GUtilsUnity.Pointer.Callbacks;

namespace GUtilsUnity.Features.InputUi.Interactors
{
    public interface IDragAndScrollInputUiInteractor
    {
        IListenEvent<DragInputData> DragEvent { get; }
        IListenEvent<ScrollPointerEventData> ScrollEvent { get; }
    }
}
