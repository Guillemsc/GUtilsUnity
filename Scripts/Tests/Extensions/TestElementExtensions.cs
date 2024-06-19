using NUnit.Framework;

namespace GUtilsUnity.Extensions.Test
{
    public class TestElementExtensions
    {
        [Test]
        [TestCase(arguments: new object[] { 3, 0f}, ExpectedResult = 0)]
        [TestCase(arguments: new object[] { 3, 1f}, ExpectedResult = 2)]
        [TestCase(arguments: new object[] { 3, 0.5f}, ExpectedResult = 1)]
        [TestCase(arguments: new object[] { 3, 0.4f}, ExpectedResult = 1)]
        [TestCase(arguments: new object[] { 3, 0.6f}, ExpectedResult = 1)]
        public int GetElementFromNormalizedPositionExcludingPadding(
            int elementCount,
            float normalizedPosition)
        {
            return ElementExtensions.GetElementFromNormalizedPosition(
                elementCount,
                normalizedPosition);
        }

        [Test]
        [TestCase(arguments: new object[] {5, 0}, ExpectedResult = 0f)]
        [TestCase(arguments: new object[] {5, 1}, ExpectedResult = 0.25f)]
        [TestCase(arguments: new object[] {5, 2}, ExpectedResult = 0.5f)]
        [TestCase(arguments: new object[] {5, 3}, ExpectedResult = 0.75f)]
        [TestCase(arguments: new object[] {5, 4}, ExpectedResult = 1f)]
        public float GetNormalizedPositionCenterOfElementExcludingPadding(
            int elementCount,
            int elementIndex)
        {
            return ElementExtensions.GetNormalizedPositionCenterOfElement(elementCount,
                elementIndex);
        }
    }
}
