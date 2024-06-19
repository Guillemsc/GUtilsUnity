using GUtilsUnity.NumericOperations;

namespace GUtilsUnity.Extensions
{
    public static class NumericOperationsExtensions
    {
        public static bool IsNegative<T>(this INumericOperations<T> numericOperations, T value)
        {
            return numericOperations.IsLessThan(value, numericOperations.Zero);
        }
    }
}
