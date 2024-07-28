using GUtils.Events;
using GUtilsUnity.Features.InputUi.Data;
using GUtilsUnity.Pointer.Callbacks;

namespace GUtilsUnity.Features.InputUi.Interactors
{
    public sealed class DragAndScrollInputUiInteractor : IDragAndScrollInputUiInteractor
    {
        readonly EventsData _inputData;

        public IListenEvent<DragInputData> DragEvent => _inputData.DragEvent;
        public IListenEvent<ScrollPointerEventData> ScrollEvent => _inputData.ScrollEvent;

        public DragAndScrollInputUiInteractor(EventsData inputData)
        {
            _inputData = inputData;
        }
    }
}
