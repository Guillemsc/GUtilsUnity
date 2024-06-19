using System;

namespace GUtilsUnity.Extensions
{
    public static class DoubleExtensions
    {
        public static double GetDecimals(this double value)
        {
            var result = value % 1d;
            return result;
        }
    }
}
