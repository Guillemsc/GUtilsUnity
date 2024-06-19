using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Scripting.APIUpdating;

// TODO: Change namespace
namespace GUtilsUnity.Extensions.Drag
{
    [MovedFrom(true, "JigsawPuzzle.Features.Utils.Drag", sourceAssembly: "JigsawPuzzle.Utils", sourceClassName: "DragCallbacks")]
    public class DragCallbacks : MonoBehaviour, IBeginDragHandler , IEndDragHandler, IDragHandler
    {
        public event Action<PointerEventData> OnBeginDrag;
        public event Action<PointerEventData> OnDrag;
        public event Action<PointerEventData> OnEndDrag;

        void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
        {
            DoBegindDrag(eventData);
        }

        protected virtual void DoBegindDrag(PointerEventData eventData)
        {
            OnBeginDrag?.Invoke(eventData);
        }

        void IEndDragHandler.OnEndDrag(PointerEventData eventData)
        {
            DoEndDrag(eventData);
        }

        protected virtual void DoEndDrag(PointerEventData eventData)
        {
            OnEndDrag?.Invoke(eventData);
        }

        void IDragHandler.OnDrag(PointerEventData eventData)
        {
            DoDrag(eventData);
        }

        protected virtual void DoDrag(PointerEventData eventData)
        {
            OnDrag?.Invoke(eventData);
        }
    }
}
