using NUnit.Framework;
using UnityEngine;

namespace GUtilsUnity.Directions.Tests
{
    [TestFixture]
    public class TestCardinalDirectionsExtensions
    {
        [Test]
        public void Raise_ToOrdinalDirection_ReturnsCorrectValue()
        {
            Assert.That(CardinalDirection.Up.ToOrdinalDirection() == OrdinalDirection.Up);
            Assert.That(CardinalDirection.Down.ToOrdinalDirection() == OrdinalDirection.Down);
            Assert.That(CardinalDirection.Left.ToOrdinalDirection() == OrdinalDirection.Left);
            Assert.That(CardinalDirection.Right.ToOrdinalDirection() == OrdinalDirection.Right);
        }

        [Test]
        public void Raise_Vector2ToCardinalDirection_ReturnsCorrectValue()
        {
            Assert.That(Vector2.up.ToCardinalDirection() == CardinalDirection.Up);
            Assert.That(Vector2.down.ToCardinalDirection() == CardinalDirection.Down);
            Assert.That(Vector2.left.ToCardinalDirection() == CardinalDirection.Left);
            Assert.That(Vector2.right.ToCardinalDirection() == CardinalDirection.Right);

            Assert.That((Vector2.up * 2).ToCardinalDirection() == CardinalDirection.Up);
            Assert.That((Vector2.down * 2).ToCardinalDirection() == CardinalDirection.Down);
            Assert.That((Vector2.left * 2).ToCardinalDirection() == CardinalDirection.Left);
            Assert.That((Vector2.right * 2).ToCardinalDirection() == CardinalDirection.Right);
        }

        [Test]
        public void Raise_Vector2IntToCardinalDirection_ReturnsCorrectValue()
        {
            Assert.That(Vector2Int.up.ToCardinalDirection() == CardinalDirection.Up);
            Assert.That(Vector2Int.down.ToCardinalDirection() == CardinalDirection.Down);
            Assert.That(Vector2Int.left.ToCardinalDirection() == CardinalDirection.Left);
            Assert.That(Vector2Int.right.ToCardinalDirection() == CardinalDirection.Right);

            Assert.That((Vector2Int.up * 2).ToCardinalDirection() == CardinalDirection.Up);
            Assert.That((Vector2Int.down * 2).ToCardinalDirection() == CardinalDirection.Down);
            Assert.That((Vector2Int.left * 2).ToCardinalDirection() == CardinalDirection.Left);
            Assert.That((Vector2Int.right * 2).ToCardinalDirection() == CardinalDirection.Right);
        }

        [Test]
        public void Raise_Rotate_ReturnsCorrectValue()
        {
            Assert.That(CardinalDirection.Up.Rotate(1) == CardinalDirection.Right);
            Assert.That(CardinalDirection.Up.Rotate(2) == CardinalDirection.Down);
            Assert.That(CardinalDirection.Up.Rotate(3) == CardinalDirection.Left);
            Assert.That(CardinalDirection.Up.Rotate(4) == CardinalDirection.Up);
            Assert.That(CardinalDirection.Up.Rotate(5) == CardinalDirection.Right);
        }
    }
}
