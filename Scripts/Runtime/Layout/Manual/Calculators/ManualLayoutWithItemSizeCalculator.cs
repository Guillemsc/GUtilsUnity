using System.Collections.Generic;
using UnityEngine;

namespace GUtilsUnity.Layout.Manual
{
    public static class ManualLayoutWithItemSizeCalculator
    {
        public static LayoutCalculationResultData Calculate(LayoutCalculationRequestData requestData)
        {
            if (requestData.Alignment != ManualLayoutAlignment.Center)
            {
                return CalculateLeftRightLayoutAlignment(requestData);
            }

            return CalculateCenterLayoutAlignment(requestData);
        }

        public static LayoutCalculationResultData CalculateLeftRightLayoutAlignment(LayoutCalculationRequestData requestData)
        {
            float directionMultiplier;

            switch(requestData.Alignment)
            {
                default:
                case ManualLayoutAlignment.LeftOrDown:
                {
                    directionMultiplier = 1f;
                    break;
                }

                case ManualLayoutAlignment.RightOrUp:
                {
                    directionMultiplier = -1f;
                    break;
                }
            }

            return CalculateFromStartPosition(
                requestData,
                requestData.StartingPosition,
                directionMultiplier
            );
        }

        public static LayoutCalculationResultData CalculateCenterLayoutAlignment(LayoutCalculationRequestData requestData)
        {
            float totalDistanceBetweenElementsToBacktrack = requestData.DistanceBetweenElements * (requestData.ElementsSizes.Count - 1);
            float totalMarginsDistanceToBacktrack = requestData.Margins * 2f;
            float elementsSizeToBacktrack = 0;

            for (int i = 0; i < requestData.ElementsSizes.Count; ++i)
            {
                float elementSize = requestData.ElementsSizes[i];

                elementsSizeToBacktrack += elementSize;
            }

            float totalDistanceToBacktrack = elementsSizeToBacktrack + totalDistanceBetweenElementsToBacktrack + totalMarginsDistanceToBacktrack;
            float startPosition = requestData.StartingPosition - (totalDistanceToBacktrack * 0.5f);

            return CalculateFromStartPosition(
                requestData,
                startPosition,
                1f
            );
        }

        public static LayoutCalculationResultData CalculateFromStartPosition(
            LayoutCalculationRequestData requestData,
            float startPosition,
            float direction
            )
        {
            List<float> finalPositions = new();

            float currentPosition = startPosition + (requestData.Margins * direction);

            for (int i = 0; i < requestData.ElementsSizes.Count; ++i)
            {
                float elementSize = requestData.ElementsSizes[i];

                currentPosition += elementSize * 0.5f * direction;

                finalPositions.Add(currentPosition);

                currentPosition += (elementSize * 0.5f) * direction;

                if (i != requestData.ElementsSizes.Count - 1)
                {
                    currentPosition += requestData.DistanceBetweenElements * direction;
                }
            }

            currentPosition += requestData.Margins * direction;

            float totalSize = Mathf.Abs(currentPosition - startPosition);

            return new LayoutCalculationResultData(
                finalPositions,
                totalSize
            );
        }
    }
}
