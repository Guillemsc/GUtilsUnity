using GUtils.Extensions;
using NUnit.Framework;

namespace GUtilsUnity.Extensions.Tests
{
    public class TestIntExtensions
    {
        [Test]
        [TestCase(0, "A")]
        [TestCase(4, "E")]
        [TestCase(26, "AA")]
        [TestCase(31, "AF")]
        [TestCase(52, "BA")]
        [TestCase(701, "ZZ")]
        [TestCase(702, "AAA")]
        [TestCase(703, "AAB")]
        [TestCase(18277, "ZZZ")]
        [TestCase(18278, "AAAA")]
        [TestCase(18279, "AAAB")]
        [TestCase(-5, "")]
        public void ToAlphabeticalId_GeneratesExpectedResult(int value, string expectedResult)
        {
            string result = value.ToAlphabeticalId();

            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        [TestCase(0, 0, 3, ExpectedResult = 1)]
        [TestCase(2, 0, 3, ExpectedResult = 0)]
        [TestCase(1, 1, 4, ExpectedResult = 2)]
        [TestCase(3, 1, 4, ExpectedResult = 1)]
        public int IncrementAndCycle_GeneratesExpectedResults(int value, int start, int end)
        {
            return value.IncrementAndCycle(start, end);
        }

        [Test]
        [TestCase(1, 0, 3, ExpectedResult = 0)]
        [TestCase(0, 0, 3, ExpectedResult = 2)]
        [TestCase(2, 1, 4, ExpectedResult = 1)]
        [TestCase(1, 1, 4, ExpectedResult = 3)]
        public int DecrementAndCycle_GeneratesExpectedResults(int value, int start, int end)
        {
            return value.DecrementAndCycle(start, end);
        }

        [Test]
        [TestCase(1, ExpectedResult = 2)]
        [TestCase(2, ExpectedResult = 4)]
        [TestCase(3, ExpectedResult = 4)]
        [TestCase(4, ExpectedResult = 8)]
        [TestCase(8, ExpectedResult = 16)]
        [TestCase(9, ExpectedResult = 16)]
        [TestCase(16, ExpectedResult = 32)]
        public int NextPowerOfTwo_ProvidesExpectedResults(int value)
        {
            return value.NextPowerOfTwo();
        }

        [Test]
        [TestCase(1, ExpectedResult = true)]
        [TestCase(2, ExpectedResult = true)]
        [TestCase(3, ExpectedResult = false)]
        [TestCase(4, ExpectedResult = true)]
        [TestCase(8, ExpectedResult = true)]
        [TestCase(9, ExpectedResult = false)]
        [TestCase(16, ExpectedResult = true)]
        public bool IsPowerOfTwo_ProvidesExpectedResults(int value)
        {
            return value.IsPowerOfTwo();
        }

        [TestCase(2, ExpectedResult = 1)]
        [TestCase(4, ExpectedResult = 2)]
        [TestCase(8, ExpectedResult = 4)]
        [TestCase(16, ExpectedResult = 8)]
        [TestCase(7, ExpectedResult = 4)]
        [TestCase(15, ExpectedResult = 8)]
        [TestCase(30, ExpectedResult = 16)]
        public int TestPreviousPowerOfTwo(int input)
        {
            return input.PreviousPowerOfTwo();
        }

        [Test]
        [TestCase(1)]
        [TestCase(0)]
        public void TestPreviousPowerOfTwoException(int input)
        {
            Assert.Throws<System.ArgumentException>(() => input.PreviousPowerOfTwo());
        }
    }
}
