using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

namespace GUtilsUnity.Extensions.Tests
{
    [TestFixture]
    public class TestVector3Extensions
    {
        static IEnumerable<TestCaseData> ClampSizePivotSource()
        {
            //Fits and is kept at the same position
            yield return new TestCaseData(Vector3.zero, Vector3.one, Vector3.zero, Vector3.zero);
            //Fits and is displaced left
            yield return new TestCaseData(Vector3.zero, Vector3.one * 2, Vector3.zero, -Vector3.one);
            //Fits and is displaced left
            yield return new TestCaseData(Vector3.zero, Vector3.one * 2, Vector3.one, Vector3.one);
            //Does not fit and is displaced left to center it
            yield return new TestCaseData(Vector3.zero, Vector3.one * 3, Vector3.zero, -Vector3.one * 2);
            //Way left should be displaced right to half it's size
            yield return new TestCaseData(-Vector3.one * 50, Vector3.one, Vector3Extensions.HalfOne, -Vector3Extensions.HalfOne);
        }

        [Test, TestCaseSource("ClampSizePivotSource")]
        public void ClampSizePivot(Vector3 source, Vector3 size, Vector3 pivot, Vector3 result)
        {
            var clampedPosition = Vector3Extensions.ClampSizePivot(source, size, pivot, -Vector3.one, Vector3.one);
            Assert.That(clampedPosition, Is.EqualTo(result));
        }


        static IEnumerable<TestCaseData> GetPositionWithPivotOffsetSource()
        {
            yield return new TestCaseData(
                Vector3.zero, Vector3Extensions.HalfOne, Vector3.one, 2, 0
                )
                .Returns(-Vector3Extensions.HalfOne);

            yield return new TestCaseData(
                    Vector3.one * 50, Vector3Extensions.HalfOne, Vector3.one, 2, 0
                )
                .Returns(-Vector3Extensions.HalfOne + Vector3.one * 50);

            yield return new TestCaseData(
                Vector3.zero, Vector3Extensions.HalfOne, Vector3.one, 2, 1
                )
                .Returns(Vector3Extensions.HalfOne);

            yield return new TestCaseData(
                    Vector3.zero, Vector3Extensions.HalfOne, Vector3.one, 3, 1
                )
                .Returns(Vector3.zero);

            yield return new TestCaseData(
                    Vector3.zero, Vector3Extensions.HalfOne, Vector3.one, 3, 2
                )
                .Returns(Vector3.one);
        }

        [Test, TestCaseSource("GetPositionWithPivotOffsetSource")]
        public Vector3 GetPositionWithPivotOffset(Vector3 position, Vector3 pivot, Vector3 delta, int count, int index)
        {
            return Vector3Extensions.GetPositionWithPivotOffset(position, pivot, delta, count, index);
        }
    }
}
