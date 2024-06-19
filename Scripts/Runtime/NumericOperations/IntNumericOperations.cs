using UnityEngine;

namespace GUtilsUnity.NumericOperations
{
    public sealed class IntNumericOperations : INumericOperations<int>
    {
        public int Zero => 0;

        public int One => 1;

        public int Addition(int first, int second) => first + second;

        public int Subtraction(int first, int second) => first - second;

        public int Multiplication(int first, int second) => first * second;

        public int Division(int first, int second) => first / second;

        public int Modulo(int first, int second) => first % second;

        public int Negation(int value) => -value;

        public int Absolute(int value) => Mathf.Abs(value);

        public int Min(int first, int second) => Mathf.Min(first, second);

        public int Max(int first, int second) => Mathf.Max(first, second);

        public bool IsZero(int value) => value == 0;

        public bool IsGreaterThan(int first, int second) => first > second;

        public bool IsGreaterThanOrEqual(int first, int second) => first >= second;

        public bool IsLessThan(int first, int second) => first < second;

        public bool IsLessThanOrEqual(int first, int second) => first <= second;

        public bool AreEqual(int first, int second) => first == second;
    }
}
