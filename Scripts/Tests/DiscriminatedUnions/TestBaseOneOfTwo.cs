using GUtilsUnity.Types;
using NUnit.Framework;

namespace GUtilsUnity.DiscriminatedUnions.Tests
{
    [TestFixture]
    public class TestBaseOneOfTwo
    {
        [Test]
        public void Raise_BaseOneOf_EqualsWorks()
        {
            OneOf<Yes, No> yes1 = Yes.Instance;
            OneOf<Yes, No> yes2 = Yes.Instance;

            Assert.That(yes1 == yes2);
        }

        [Test]
        public void Raise_BaseOneOf_MatchWorks()
        {
            OneOf<Yes, No> yes = Yes.Instance;
            bool result1 = yes.Match(
                _ => true,
                _ => false
            );

            OneOf<Yes, No> no = No.Instance;
            bool result2 = no.Match(
                _ => true,
                _ => false
            );

            Assert.That(result1);
            Assert.That(!result2);
        }
    }
}
