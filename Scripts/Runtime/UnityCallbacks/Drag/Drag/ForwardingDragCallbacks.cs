using UnityEngine.EventSystems;
using UnityEngine.Scripting.APIUpdating;

// TODO: Change namespace
namespace GUtilsUnity.Extensions.Drag
{
    [MovedFrom(true, "JigsawPuzzle.Features.Utils.Drag", sourceAssembly: "JigsawPuzzle.Utils", sourceClassName: "ForwardingDragCallbacks")]
    public class ForwardingDragCallbacks : DragCallbacks
    {
        public UnityEngine.MonoBehaviour ForwardObject;

        protected override void DoBegindDrag(PointerEventData eventData)
        {
            if (ForwardObject is IBeginDragHandler beginDragHandler)
            {
                beginDragHandler.OnBeginDrag(eventData);
            }
            base.DoBegindDrag(eventData);
        }

        protected override void DoEndDrag(PointerEventData eventData)
        {
            if (ForwardObject is IEndDragHandler endDragHandler)
            {
                endDragHandler.OnEndDrag(eventData);
            }
            base.DoEndDrag(eventData);
        }

        protected override void DoDrag(PointerEventData eventData)
        {
            if (ForwardObject is IDragHandler dragHandler)
            {
                dragHandler.OnDrag(eventData);
            }
            base.DoDrag(eventData);
        }
    }
}
