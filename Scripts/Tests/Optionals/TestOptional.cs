#nullable enable

using GUtils.Optionals;
using NUnit.Framework;

namespace GUtilsUnity.Scripts.Optionals.Tests
{
    [TestFixture]
    public class TestOptional
    {
        [Test]
        public void Maybe_WithNullNullable_DoesNotHaveValue()
        {
            object? nullable = null;
            var optional = Optional<object>.Maybe(nullable);
            Assert.That(!optional.HasValue);
        }

        [Test]
        public void Maybe_WithNonNullNullable_HasValue()
        {
            object? nullable = new object();
            var optional = Optional<object>.Maybe(nullable);
            Assert.That(optional.HasValue);
        }
    }
}
