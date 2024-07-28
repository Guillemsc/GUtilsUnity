using GUtils.Di.Builder;
using GUtils.Di.Installers;
using GUtilsUnity.Features.InputUi.Data;
using GUtilsUnity.Features.InputUi.Enums;
using GUtilsUnity.Features.InputUi.Interactors;
using GUtilsUnity.Pointer.Callbacks;
using GUtilsUnity.Pointer.Extensions;
using UnityEngine;

namespace GUtilsUnity.Features.InputUi.Installers
{
    public sealed class DragAndScrollInputUiInstaller : MonoBehaviour, IInstaller
    {
        [Header("References")]
        [SerializeField] DragPointerCallbacks _dragPointerCallbacks;
        [SerializeField] ScrollPointerCallbacks _scrollPointerCallbacks;

        public void Install(IDiContainerBuilder container)
        {
            container.Bind<EventsData>()
                .FromNew()
                .LinkDragPointerCallbacksOnBegin(
                    _dragPointerCallbacks,
                    (o) => (pointerEventData) => o.DragEvent.Raise(new DragInputData(DragType.Start, pointerEventData.position))
                )
                .LinkDragPointerCallbacksOnDragging(
                    _dragPointerCallbacks,
                    (o) => (pointerEventData) => o.DragEvent.Raise(new DragInputData(DragType.Update, pointerEventData.position))
                )
                .LinkDragPointerCallbacksOnEnd(
                    _dragPointerCallbacks,
                    (o) => (pointerEventData) => o.DragEvent.Raise(new DragInputData(DragType.End, pointerEventData.position))
                )
                .LinkScrollPointerCallbacksOnScrolling(
                    _scrollPointerCallbacks,
                    (o) => (value) => o.ScrollEvent.Raise(value)
                );

            container.Bind<IDragAndScrollInputUiInteractor>()
                .FromFunction(c => new DragAndScrollInputUiInteractor(
                    c.Resolve<EventsData>()
                ));
        }
    }
}
