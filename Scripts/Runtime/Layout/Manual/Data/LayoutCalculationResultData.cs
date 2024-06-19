using System.Collections.Generic;

namespace GUtilsUnity.Layout.Manual
{
    public readonly struct LayoutCalculationResultData
    {
        public IReadOnlyList<float> Positions { get; }
        public float TotalSize { get; }

        public LayoutCalculationResultData(IReadOnlyList<float> positions, float totalSize)
        {
            Positions = positions;
            TotalSize = totalSize;
        }
    }
}
