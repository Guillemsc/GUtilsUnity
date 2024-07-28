using GUtils.ActiveSources.Extensions;
using GUtils.ActiveSources.Id;
using NUnit.Framework;
using GUtilsUnity.ActiveSource.Extensions;

namespace GUtilsUnity.ActiveSource.Tests
{
    public sealed class TestIdActiveSource
    {
        [Test]
        public void BlockAll_BlocksAllObjects()
        {
            IdActiveSource<int> inputBlocking = new ();

            inputBlocking.Track(1);
            inputBlocking.Track(2);

            inputBlocking.SetActiveAll(this, false);

            Assert.That(inputBlocking.IsActive(1), Is.False);
            Assert.That(inputBlocking.IsActive(2), Is.False);
        }

        [Test]
        public void BlockAllOn2OwnersThenUnblockAll_KeepsItBlocked()
        {
            IdActiveSource<int> inputBlocking = new ();

            inputBlocking.Track(1);

            inputBlocking.DeactivateAll(this);
            inputBlocking.DeactivateAll(new object());
            inputBlocking.ActivateAll(this);

            Assert.That(inputBlocking.IsActive(1), Is.False);
        }

        [Test]
        public void BlockAllThenUnblockOne_UnblocksJustOne()
        {
            IdActiveSource<int> inputBlocking = new ();

            inputBlocking.Track(1);
            inputBlocking.Track(2);

            inputBlocking.DeactivateAll(this);
            inputBlocking.SetActive(this, 1, true);

            Assert.That(inputBlocking.IsActive(1), Is.True);
            Assert.That(inputBlocking.IsActive(2), Is.False);
        }

        [Test]
        public void BlockAllThenBlockThenUnblock_Unblocks()
        {
            IdActiveSource<int> inputBlocking = new ();

            inputBlocking.Track(1);

            inputBlocking.DeactivateAll(this);
            inputBlocking.SetActive(this, 1, false);
            inputBlocking.SetActive(this, 1, true);

            Assert.That(inputBlocking.IsActive(1), Is.True);
        }

        [Test]
        public void BlockOne_ShouldBlock()
        {
            IdActiveSource<int> inputBlocking = new ();

            inputBlocking.Track(1);

            inputBlocking.SetActive(this, 1, false);

            Assert.That(inputBlocking.IsActive(1), Is.False);
        }

        [Test]
        public void BlockAllThenUnblockAll_ShouldUnblock()
        {
             IdActiveSource<int> inputBlocking = new ();

            inputBlocking.Track(1);

            inputBlocking.DeactivateAll(this);
            inputBlocking.ActivateAll(this);

            Assert.That(inputBlocking.IsActive(1), Is.True);
        }
    }
}
