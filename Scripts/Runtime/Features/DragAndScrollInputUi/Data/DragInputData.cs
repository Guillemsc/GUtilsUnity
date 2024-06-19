using GUtilsUnity.Features.InputUi.Enums;
using UnityEngine;

namespace GUtilsUnity.Features.InputUi.Data
{
    public readonly struct DragInputData
    {
        public DragType DragType { get; }
        public Vector2 Position { get; }

        public DragInputData(DragType dragType, Vector2 position)
        {
            DragType = dragType;
            Position = position;
        }
    }
}
