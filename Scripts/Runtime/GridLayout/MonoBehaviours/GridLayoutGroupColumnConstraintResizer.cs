using GUtilsUnity.Extensions;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.UI;

namespace GUtilsUnity.GridLayoyut
{
    /// <summary>
    /// Resizes the cell size of a GridLayoutGroup based on a column constraint and a height factor.
    /// Calculates the width of each cell in a grid layout so
    /// that each column takes all available horizontal space in the parent container.
    /// </summary>
    [ExecuteInEditMode]
    public sealed class GridLayoutGroupColumnConstraintResizer : MonoBehaviour
    {
        [Tooltip("The GridLayoutGroup component to resize.")]
        public GridLayoutGroup GridLayoutGroup;

        [Tooltip("The factor to multiply the width of the cell size to determine the height.")]
        public float HeightFactor = 1f;

        Vector2 _previousSize;

        void Update()
        {
            Resize();
        }

        [ContextMenu("Resize")]
        public void Resize()
        {
            if (GridLayoutGroup == null)
            {
                return;
            }

            // It does not make sense to keep it as flexible, since we read the constraintCount
            // inside CalculateGridLayoutCellWidthWithColumnConstraintToTakeAllAvailableHorizontalSpace
            if (GridLayoutGroup.constraint == GridLayoutGroup.Constraint.Flexible)
            {
                GridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            }

            float width = GridLayoutGroup.CalculateGridLayoutCellWidthWithColumnConstraintToTakeAllAvailableHorizontalSpace();
            float height = width * HeightFactor;

            Vector2 newSize = new Vector2(width, height);

            if (newSize == _previousSize)
            {
                return;
            }

            _previousSize = newSize;

            GridLayoutGroup.cellSize = new Vector2(width, height);
        }
    }
}
