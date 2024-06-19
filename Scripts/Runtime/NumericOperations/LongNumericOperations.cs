using System;

namespace GUtilsUnity.NumericOperations
{
    public sealed class LongNumericOperations : INumericOperations<long>
    {
        public long Zero => 0;

        public long One => 1;

        public long Addition(long first, long second) => first + second;

        public long Subtraction(long first, long second) => first - second;

        public long Multiplication(long first, long second) => first * second;

        public long Division(long first, long second) => first / second;

        public long Modulo(long first, long second) => first % second;

        public long Negation(long value) => -value;

        public long Absolute(long value) => Math.Abs(value);

        public long Min(long first, long second) => Math.Min(first, second);

        public long Max(long first, long second) => Math.Max(first, second);

        public bool IsZero(long value) => value == 0;

        public bool IsGreaterThan(long first, long second) => first > second;

        public bool IsGreaterThanOrEqual(long first, long second) => first >= second;

        public bool IsLessThan(long first, long second) => first < second;

        public bool IsLessThanOrEqual(long first, long second) => first <= second;

        public bool AreEqual(long first, long second) => first == second;
    }
}
