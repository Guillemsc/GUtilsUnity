using System;
using System.Collections;
using NUnit.Framework;

namespace GUtilsUnity.Extensions.Tests
{
    public class TestRangeExtensions
    {
        [Test]
        public void Range_OnRangeFromStartWithEnd_ProvidesExpectedResults()
        {
            var values = RangeValues(..4);
            Assert.That(values, Is.EqualTo(new[] { 0, 1, 2, 3 }));
        }

        [Test]
        public void Range_OnRangeWithStartFromEnd_Fails()
        {
            Assert.Throws<NotSupportedException>(() => (^5..4).GetEnumerator());
        }

        [Test]
        public void Range_OnRangeWithEndFromEnd_Fails()
        {
            Assert.Throws<NotSupportedException>(() => (0..).GetEnumerator());
        }

        int[] RangeValues(Range range)
        {
            var values = new System.Collections.Generic.List<int>();

            foreach (var indices in range)
            {
                values.Add(indices);
            }

            return values.ToArray();
        }
    }
}
