using NUnit.Framework;

namespace GUtilsUnity.Extensions.Tests
{
    [TestFixture]
    public class TestFloatExtensions
    {
        [Test]
        [TestCase(0f, ExpectedResult = -1)]
        [TestCase(0.5f, ExpectedResult = 0)]
        [TestCase(0.75f, ExpectedResult = 0.5f)]
        [TestCase(1f, ExpectedResult = 1)]
        public float FromNormalizedRangeToSignedRange(float value)
        {
            return value.FromNormalizedRangeToSignedRange();
        }

        [Test]
        [TestCase(0f, ExpectedResult = 1)]
        [TestCase(0.5f, ExpectedResult = 0)]
        [TestCase(0.75f, ExpectedResult = -0.5f)]
        [TestCase(1f, ExpectedResult = -1)]
        public float FromNormalizedRangeToInvertedSignedRange(float value)
        {
            return value.FromNormalizedRangeToInvertedSignedRange();
        }

        [Test]
        [TestCase(0f, ExpectedResult = 0)]
        [TestCase(0.25f, ExpectedResult = 0.5f)]
        [TestCase(0.5f, ExpectedResult = 1)]
        [TestCase(0.75f, ExpectedResult = 0.5f)]
        [TestCase(1f, ExpectedResult = -0f)]
        [TestCase(-0.25f, ExpectedResult = -0.5f)]
        [TestCase(-0.5f, ExpectedResult = -1)]
        [TestCase(-0.75f, ExpectedResult = -0.5f)]
        [TestCase(-1f, ExpectedResult = -0f)]
        public float FromNormalizedRangeToBouncingSignedRange(float value)
        {
            return value.FromNormalizedRangeToBouncingSignedRange();
        }
    }
}
