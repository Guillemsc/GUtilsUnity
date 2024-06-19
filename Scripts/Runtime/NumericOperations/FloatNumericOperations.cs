using GUtilsUnity.Extensions;
using UnityEngine;

namespace GUtilsUnity.NumericOperations
{
    public sealed class FloatNumericOperations : INumericOperations<float>
    {
        public float Zero => 0f;

        public float One => 1f;

        public float Addition(float first, float second) => first + second;

        public float Subtraction(float first, float second) => first - second;

        public float Multiplication(float first, float second) => first * second;

        public float Division(float first, float second) => first / second;

        public float Modulo(float first, float second) => first % second;

        public float Negation(float value) => -value;

        public float Absolute(float value) => Mathf.Abs(value);

        public float Min(float first, float second) => Mathf.Min(first, second);

        public float Max(float first, float second) => Mathf.Max(first, second);

        public bool IsZero(float value) => value == 0;

        public bool IsGreaterThan(float first, float second) => first > second;

        public bool IsGreaterThanOrEqual(float first, float second) => first >= second;

        public bool IsLessThan(float first, float second) => first < second;

        public bool IsLessThanOrEqual(float first, float second) => first <= second;

        public bool AreEqual(float first, float second) => MathExtensions.AreEpsilonEquals(first, second);
    }
}
