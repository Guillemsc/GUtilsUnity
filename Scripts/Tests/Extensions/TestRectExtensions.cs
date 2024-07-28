using System.Collections.Generic;
using GUtils.Directions;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools.Utils;

namespace GUtilsUnity.Extensions.Test
{
    [TestFixture]
    public class TestRectExtensions
    {
        public static IEnumerable<TestCaseData> GetOffsetToBePlacedOutsideRectTestCases
        {
            get
            {
                yield return new TestCaseData(
                    new Rect(Vector2.one, Vector2.one),
                    new Rect(Vector2.one, Vector2.one),
                    CardinalDirection.Up,
                    0f,
                    new Vector2(0, 1)
                );
                yield return new TestCaseData(
                    new Rect(Vector2.one, Vector2.one),
                    new Rect(Vector2.one, Vector2.one),
                    CardinalDirection.Up,
                    2f,
                    new Vector2(0, 3)
                );
                yield return new TestCaseData(
                    new Rect(Vector2.one, Vector2.one),
                    new Rect(Vector2.one, Vector2.one),
                    CardinalDirection.Down,
                    0f,
                    new Vector2(0, -1)
                );
                yield return new TestCaseData(
                    new Rect(Vector2.one, Vector2.one),
                    new Rect(Vector2.one, Vector2.one),
                    CardinalDirection.Down,
                    2f,
                    new Vector2(0, -3)
                );
                yield return new TestCaseData(
                    new Rect(Vector2.one, Vector2.one),
                    new Rect(Vector2.one, Vector2.one),
                    CardinalDirection.Left,
                    2f,
                    new Vector2(-3, 0)
                );
                yield return new TestCaseData(
                    new Rect(Vector2.one, Vector2.one),
                    new Rect(Vector2.one, Vector2.one),
                    CardinalDirection.Right,
                    2f,
                    new Vector2(3, 0)
                );
                yield return new TestCaseData(
                    new Rect(Vector2.one, Vector2.one),
                    new Rect(new Vector2(-10, 0), Vector2.one),
                    CardinalDirection.Right,
                    2f,
                    new Vector2(14, 0)
                );
            }
        }

        [Test]
        [TestCaseSource(nameof(GetOffsetToBePlacedOutsideRectTestCases))]
        public void GetOffsetToBePlacedOutsideRect(
            Rect referenceRect,
            Rect toMoveRect,
            CardinalDirection cardinalDirection,
            float distance,
            Vector2 expected)
        {
            var actual = referenceRect.GetOffsetToBePlacedOutsideRect(toMoveRect, cardinalDirection, distance);

            Assert.That(actual, Is.EqualTo(expected).Using(Vector2EqualityComparer.Instance));
        }
    }
}
