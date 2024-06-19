using System;
using System.Numerics;
using System.Collections.Generic;

namespace GUtilsUnity.LargeNumber
{
    public static class LargeNumberFormat
    {
        const string DefaultFormat = "0.##";
        static readonly int IntCharA = Convert.ToInt32('A');

        static readonly string[] Units =
        {
            "",
            "K",
            "M",
            "B",
            "T",
        };

        public static string ToLargeNumberString(int value) => ToLargeNumberString(value, Units);
        public static string ToLargeNumberString(long value) => ToLargeNumberString(value, Units);
        public static string ToLargeNumberString(double value) => ToLargeNumberString(value, Units);
        public static string ToLargeNumberString(BigInteger value) => ToLargeNumberString(value, Units);

        static string ToLargeNumberString(double value, IReadOnlyList<string> thousandUnits)
        {
            if (value == 0)
                return "0";
            bool isNegative;
            if (value < 0)
            {
                isNegative = true;
                value = -value;
            }
            else
            {
                isNegative = false;
            }

            var n = (int)Math.Log(value, 1000);
            var m = value / Math.Pow(1000, n);
            var unit = "";

            if (n < thousandUnits.Count)
            {
                unit = thousandUnits[n];
            }
            else
            {
                var unitInt = n - thousandUnits.Count;
                var secondUnit = unitInt % 26;
                var firstUnit = unitInt / 26;
                unit = Convert.ToChar((int)firstUnit + IntCharA) + Convert.ToChar((int)secondUnit + IntCharA).ToString();
            }


            // Math.Floor(m * 100) / 100) fixes rounding errors
            var number = (Math.Floor(m * 100) / 100);

            var format = "0";
            if (number < 100) format += ".#";
            if (number < 10) format += "#";

            var targetString = number.ToString(format) + unit;

            return isNegative ? $"-{targetString}" : targetString;
        }

        static string ToLargeNumberString(long value, IReadOnlyList<string> thousandUnits)
        {
            if (value == 0)
                return "0";
            bool isNegative;
            if (value < 0)
            {
                isNegative = true;
                value = -value;
            }
            else
            {
                isNegative = false;
            }

            var n = (long)Math.Log(value, 1000);
            var m = value / Math.Pow(1000, n);
            var unit = "";

            if (n < thousandUnits.Count)
            {
                unit = thousandUnits[(int)n];
            }
            else
            {
                var unitInt = n - thousandUnits.Count;
                var secondUnit = unitInt % 26;
                var firstUnit = unitInt / 26;
                unit = Convert.ToChar(firstUnit + IntCharA) + Convert.ToChar(secondUnit + IntCharA).ToString();
            }

            // Math.Floor(m * 100) / 100) fixes rounding errors
            var number = (Math.Floor(m * 100) / 100);
            var format = "0";
            if (number < 100) format += ".#";
            if (number < 10) format += "#";
            var targetString = number.ToString(format) + unit;
            return isNegative ? $"-{targetString}" : targetString;
        }

        static string ToLargeNumberString(BigInteger value, IReadOnlyList<string> thousandUnits, string format = DefaultFormat)
        {
            if (value == 0)
                return "0";
            bool isNegative;
            if (value < 0)
            {
                isNegative = true;
                value = -value;
            }
            else
            {
                isNegative = false;
            }

            var n = (int)BigInteger.Log(value, 1000);
            var m = value / BigInteger.Pow(1000, n);
            var unit = "";

            if (n < thousandUnits.Count)
            {
                unit = thousandUnits[n];
            }
            else
            {
                var unitInt = n - thousandUnits.Count;
                var secondUnit = unitInt % 26;
                var firstUnit = unitInt / 26;
                unit = Convert.ToChar(firstUnit + IntCharA) + Convert.ToChar(secondUnit + IntCharA).ToString();
            }

            var targetString = m.ToString(format) + unit;
            return isNegative ? $"-{targetString}" : targetString;
        }
    }
}
