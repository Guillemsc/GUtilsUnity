using System.Numerics;
using UnityEngine;

namespace GUtilsUnity.NumericOperations
{
    public sealed class BigIntNumericOperations : INumericOperations<BigInteger>
    {
        public BigInteger Zero => BigInteger.Zero;

        public BigInteger One => BigInteger.One;

        public BigInteger Addition(BigInteger first, BigInteger second) => first + second;

        public BigInteger Subtraction(BigInteger first, BigInteger second) => first - second;

        public BigInteger Multiplication(BigInteger first, BigInteger second) => first * second;

        public BigInteger Division(BigInteger first, BigInteger second) => first / second;

        public BigInteger Modulo(BigInteger first, BigInteger second) => first % second;

        public BigInteger Negation(BigInteger value) => -value;

        public BigInteger Absolute(BigInteger value) => BigInteger.Abs(value);

        public BigInteger Min(BigInteger first, BigInteger second) => BigInteger.Min(first, second);

        public BigInteger Max(BigInteger first, BigInteger second) => BigInteger.Max(first, second);

        public bool IsZero(BigInteger value) => value == BigInteger.Zero;

        public bool IsGreaterThan(BigInteger first, BigInteger second) => first > second;

        public bool IsGreaterThanOrEqual(BigInteger first, BigInteger second) => first >= second;

        public bool IsLessThan(BigInteger first, BigInteger second) => first < second;

        public bool IsLessThanOrEqual(BigInteger first, BigInteger second) => first <= second;

        public bool AreEqual(BigInteger first, BigInteger second) => first == second;
    }
}
