using GUtilsUnity.Events;
using GUtilsUnity.Pointer.Callbacks;

namespace GUtilsUnity.Features.InputUi.Data
{
    public sealed class EventsData
    {
        public IEvent<DragInputData> DragEvent { get; } = new Event<DragInputData>();
        public IEvent<ScrollPointerEventData> ScrollEvent { get; } = new Event<ScrollPointerEventData>();
    }
}
