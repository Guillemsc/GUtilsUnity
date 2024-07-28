using GUtils.Extensions;
using NUnit.Framework;

namespace GUtilsUnity.Extensions.Test
{
    [TestFixture]
    public class TestDoubleExtensions
    {
        [Test]
        [TestCase(0.0d, 0.0d)]
        [TestCase(0.2d, 0.2d)]
        [TestCase(-0.2d, -0.2d)]
        public void GetDecimals_OnValues_ReturnsExpected(double value, double expected)
        {
            var result = value.GetDecimals();
            var equal = GUtils.Extensions.MathExtensions.AreEpsilonEquals(result, expected);
            Assert.That(equal);
        }
    }
}
