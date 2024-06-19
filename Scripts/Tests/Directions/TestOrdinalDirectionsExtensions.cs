using NUnit.Framework;
using UnityEngine;

namespace GUtilsUnity.Directions.Tests
{
    [TestFixture]
    public class TestOrdinalDirectionsExtensions
    {
        [Test]
        public void Raise_ToCardinalDirection_ReturnsCorrectValue()
        {
            Assert.That(OrdinalDirection.Up.ToCardinalDirection() == CardinalDirection.Up);
            Assert.That(OrdinalDirection.Down.ToCardinalDirection() == CardinalDirection.Down);
            Assert.That(OrdinalDirection.Left.ToCardinalDirection() == CardinalDirection.Left);
            Assert.That(OrdinalDirection.Right.ToCardinalDirection() == CardinalDirection.Right);

            Assert.That(OrdinalDirection.DownRight.ToCardinalDirection() == CardinalDirection.Right);
            Assert.That(OrdinalDirection.DownLeft.ToCardinalDirection() == CardinalDirection.Down);
            Assert.That(OrdinalDirection.UpRight.ToCardinalDirection() == CardinalDirection.Up);
            Assert.That(OrdinalDirection.UpLeft.ToCardinalDirection() == CardinalDirection.Left);
        }
    }
}
