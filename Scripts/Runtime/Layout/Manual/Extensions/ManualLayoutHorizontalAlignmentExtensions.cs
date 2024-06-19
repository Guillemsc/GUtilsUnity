namespace GUtilsUnity.Layout.Manual.Extensions
{
    public static class ManualLayoutHorizontalAlignmentExtensions
    {
        public static ManualLayoutAlignment ToGenericLayoutAlignment(this ManualLayoutHorizontalAlignment alignment)
        {
            switch (alignment)
            {
                case ManualLayoutHorizontalAlignment.Left:
                {
                    return ManualLayoutAlignment.LeftOrDown;
                }

                case ManualLayoutHorizontalAlignment.Right:
                {
                    return ManualLayoutAlignment.RightOrUp;
                }
            }

            return ManualLayoutAlignment.Center;
        }
    }
}
