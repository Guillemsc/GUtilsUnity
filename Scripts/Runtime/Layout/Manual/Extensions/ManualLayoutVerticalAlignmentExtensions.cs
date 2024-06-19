namespace GUtilsUnity.Layout.Manual.Extensions
{
    public static class ManualLayoutVerticalAlignmentExtensions
    {
        public static ManualLayoutAlignment ToGenericLayoutAlignment(this ManualLayoutVerticalAlignment alignment)
        {
            switch (alignment)
            {
                case ManualLayoutVerticalAlignment.Down:
                {
                    return ManualLayoutAlignment.LeftOrDown;
                }

                case ManualLayoutVerticalAlignment.Up:
                {
                    return ManualLayoutAlignment.RightOrUp;
                }
            }

            return ManualLayoutAlignment.Center;
        }
    }
}
