using System.Collections.Generic;

namespace GUtilsUnity.Layout.Manual
{
    public readonly struct LayoutCalculationRequestData
    {
        public float StartingPosition { get; }
        public IReadOnlyList<float> ElementsSizes { get; }
        public float DistanceBetweenElements { get; }
        public float Margins { get; }
        public ManualLayoutAlignment Alignment { get; }

        public LayoutCalculationRequestData(
            float startingPosition,
            IReadOnlyList<float> elementsSizes,
            float distanceBetweenElements,
            float margins,
            ManualLayoutAlignment alignment
            )
        {
            StartingPosition = startingPosition;
            ElementsSizes = elementsSizes;
            DistanceBetweenElements = distanceBetweenElements;
            Margins = margins;
            Alignment = alignment;
        }
    }
}
