using System;
using GUtilsUnity.Extensions;
using UnityEngine;

namespace GUtilsUnity.NumericOperations
{
    public sealed class DoubleNumericOperations : INumericOperations<double>
    {
        public double Zero => 0f;
        public double One => 1f;
        public double Addition(double first, double second) => first + second;
        public double Subtraction(double first, double second) => first - second;
        public double Multiplication(double first, double second) => first * second;
        public double Division(double first, double second) => first / second;
        public double Modulo(double first, double second) => first % second;
        public double Negation(double value) => -value;
        public double Absolute(double value) => Math.Abs(value);
        public double Min(double first, double second) => Math.Min(first, second);
        public double Max(double first, double second) => Math.Max(first, second);
        public bool IsZero(double value) => value == 0;
        public bool IsGreaterThan(double first, double second) => first > second;
        public bool IsGreaterThanOrEqual(double first, double second) => first >= second;
        public bool IsLessThan(double first, double second) => first < second;
        public bool IsLessThanOrEqual(double first, double second) => first <= second;
        public bool AreEqual(double first, double second) => MathExtensions.AreEpsilonEquals(first, second);
    }
}
