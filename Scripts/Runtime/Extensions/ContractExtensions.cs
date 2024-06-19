using System;

namespace GUtilsUnity.Extensions
{
    public static class ContractExtensions
    {
        public static void ThrowIfNegative(int value)
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value), value, "Value can't be negative");
            }
        }
    }
}
