using GUtils.DiscriminatedUnions;
using GUtils.Types;
using NUnit.Framework;

namespace GUtilsUnity.DiscriminatedUnions.Tests
{
    [TestFixture]
    public class TestOneOfTwo
    {
        [Test]
        public void Raise_OneOf_EqualsWorks()
        {
            OneOf<Yes, No> yes1 = OneOf<Yes, No>.Of(Yes.Instance);
            OneOf<Yes, No> yes2 = OneOf<Yes, No>.Of(Yes.Instance);

            Assert.That(yes1 == yes2);
        }

        [Test]
        public void Raise_OneOf_MatchWorks()
        {
            OneOf<Yes, No> yes = OneOf<Yes, No>.Of(Yes.Instance);
            bool result1 = yes.Match(
                _ => true,
                _ => false
            );

            OneOf<Yes, No> no = OneOf<Yes, No>.Of(No.Instance);
            bool result2 = no.Match(
                _ => true,
                _ => false
            );

            Assert.That(result1);
            Assert.That(!result2);
        }
    }
}
