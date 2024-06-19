using GUtilsUnity.SceneManagement.Group.Style;
using UnityEditor;
using UnityEngine;

namespace GUtilsUnity.SceneManagement.Group.Helpers
{
    public class ReorderableHelper
    {
        int _draggedStartId = -1;
        int _draggedEndId = -1;

        public void CheckDraggingItem(
            int index,
            Event ev,
            Rect interactionRect,
            Rect secondaryInteractionRect
            )
        {
            if (ev.type == EventType.MouseDown)
            {
                if (interactionRect.Contains(ev.mousePosition))
                {
                    _draggedStartId = index;
                    ev.Use();
                }
            }

            // Draw rect if item is being dragged
            if (_draggedStartId == index && interactionRect != Rect.zero)
            {
                EditorGUI.DrawRect(secondaryInteractionRect, SceneGroupEditorStyle.ReorderRectColor);
            }

            // If hovering at the top while dragging one, check where
            // it should be dropped: top or bottom
            bool rectContainsMousePosition = secondaryInteractionRect.Contains(ev.mousePosition);

            if (!rectContainsMousePosition || _draggedStartId < 0)
            {
                return;
            }

            bool needsToChangeIndex = secondaryInteractionRect.Contains(ev.mousePosition);

            if (!needsToChangeIndex)
            {
                return;
            }

            _draggedEndId = index;
        }

        public bool ResolveDragging(Event e, out int startIndex, out int endIndex)
        {
            bool ret = false;

            startIndex = -1;
            endIndex = -1;

            if (_draggedStartId >= 0 && _draggedEndId >= 0)
            {
                if (_draggedEndId != _draggedStartId)
                {
                    startIndex = _draggedStartId;
                    endIndex = _draggedEndId;

                    _draggedStartId = _draggedEndId;

                    ret = true;
                }
            }

            if (_draggedStartId >= 0 || _draggedEndId >= 0)
            {
                if (e.type == EventType.MouseUp)
                {
                    _draggedStartId = -1;
                    _draggedEndId = -1;
                    e.Use();
                }
            }

            return ret;
        }
    }
}
