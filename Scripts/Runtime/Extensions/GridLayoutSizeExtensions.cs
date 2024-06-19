using GUtilsUnity.Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace GUtilsUnity.Extensions
{
    public static class GridLayoutSizeExtensions
    {
        /// <summary>
        /// Calculates the width of each cell in a grid layout so
        /// that each column takes all available horizontal space in the parent container.
        /// </summary>
        /// <param name="gridLayout">The instance of the GridLayoutGroup that this method is being called on.</param>
        /// <returns>The width of each cell in the grid.</returns>
        public static float CalculateGridLayoutCellWidthWithColumnConstraintToTakeAllAvailableHorizontalSpace(
            this GridLayoutGroup gridLayout
            )
        {
            RectTransform parentTransform = (RectTransform)gridLayout.transform.parent;
            float parentWidth = parentTransform.rect.width;

            int gridLayoutConstraintCount = gridLayout.constraintCount;

            float spacingSize = gridLayout.padding.left +
                gridLayout.padding.right +
                gridLayout.spacing.x * (gridLayoutConstraintCount - 1);

            float remainingSize = parentWidth - spacingSize;

            float cellWidth = MathExtensions.Divide(remainingSize, gridLayoutConstraintCount);

            return cellWidth;
        }
    }
}
