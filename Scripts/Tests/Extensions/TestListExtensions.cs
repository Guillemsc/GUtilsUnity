using System.Collections.Generic;
using NUnit.Framework;

namespace GUtilsUnity.Extensions.Tests
{
    public class TestListExtensions
    {
        [Test]
        [TestCase(5, new int[0], 0, new[] { 5 })]
        [TestCase(5, new int[] { 1, 2 }, 2, new[] { 1, 2, 5 })]
        [TestCase(5, new int[] { 1, 7 }, 1, new[] { 1, 5, 7 })]
        public void InsertByFunc_InProvidedLists_GeneratesExpectedResult(
            int element,
            int[] desired,
            int expectedIndex,
            int[] expectedList)
        {
            var list = new List<int>(desired);

            var index = list.InsertByFunc(
                element,
                (element, listElement) => element < listElement);

            Assert.That(list, Is.EqualTo(new List<int>(expectedList)));
            Assert.That(index, Is.EqualTo(expectedIndex));
        }
    }
}
