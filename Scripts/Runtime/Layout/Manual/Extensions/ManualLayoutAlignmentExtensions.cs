namespace GUtilsUnity.Layout.Manual.Extensions
{
    public static class ManualLayoutAlignmentExtensions
    {
        public static ManualLayoutHorizontalAlignment ToHorizontal(this ManualLayoutAlignment alignment)
        {
            switch (alignment)
            {
                case ManualLayoutAlignment.LeftOrDown:
                {
                    return ManualLayoutHorizontalAlignment.Left;
                }

                case ManualLayoutAlignment.RightOrUp:
                {
                    return ManualLayoutHorizontalAlignment.Right;
                }
            }

            return ManualLayoutHorizontalAlignment.Center;
        }

        public static ManualLayoutVerticalAlignment ToVertical(this ManualLayoutAlignment alignment)
        {
            switch (alignment)
            {
                case ManualLayoutAlignment.LeftOrDown:
                {
                    return ManualLayoutVerticalAlignment.Down;
                }

                case ManualLayoutAlignment.RightOrUp:
                {
                    return ManualLayoutVerticalAlignment.Up;
                }
            }

            return ManualLayoutVerticalAlignment.Center;
        }
    }
}
