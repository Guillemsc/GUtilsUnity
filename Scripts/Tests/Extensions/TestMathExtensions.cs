using NUnit.Framework;

namespace GUtilsUnity.Extensions.Tests
{
    [TestFixture]
    public class TestMathExtensions
    {
        [Test]
        [TestCase(3, 0, 4,  ExpectedResult = 0.75f)]
        [TestCase(1, 0, 1,  ExpectedResult = 1)]
        [TestCase(0, 0, 0,  ExpectedResult = 0)]
        [TestCase(0, 2, 5,  ExpectedResult = 0)]
        [TestCase(10, 2, 5,  ExpectedResult = 1)]
        [TestCase(-3, -4, 0,  ExpectedResult = 0.25f)]
        [TestCase(-4, -5, -1,  ExpectedResult = 0.25f)]
        public float GetNormalizedFactor_Returns_CorrectValue(float current, float min, float max)
        {
            return MathExtensions.GetNormalizedFactor(current, min, max);
        }
    }
}
