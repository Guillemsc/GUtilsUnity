#nullable enable

using GUtils.Optionals;
using NUnit.Framework;

namespace GUtilsUnity.Extensions.Test
{
    public class TestOptionalExtensions
    {
        [Test]
        public void None_OnReferenceToNullable_IsNull()
        {
            var result = Optional<object>.None.ToNullableReference();
            Assert.That(result, Is.EqualTo(null));
        }

        [Test]
        public void Some_OnReferenceToNullable_IsNotNull()
        {
            var someReference = new object();
            var result = Optional<object>.Some(someReference).ToNullableReference();
            Assert.That(result, Is.EqualTo(someReference));
        }

        [Test]
        public void Some_OnValueToNullable_IsNotNull()
        {
            var someValue = 1;
            var result = Optional<int>.Some(someValue).ToNullableValue();
            Assert.That(result, Is.EqualTo(1));
        }

        [Test]
        public void None_OnValueToNullable_IsNotNull()
        {
            var result = Optional<int>.None.ToNullableValue();
            Assert.That(result, Is.EqualTo(null));
        }
    }
}
