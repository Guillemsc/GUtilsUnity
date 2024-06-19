using System.Collections.Generic;
using GUtilsUnity.Extensions;
using NUnit.Framework;
using UnityEngine;

namespace GUtilsUnity.Tweening.Extensions.Test
{
    [TestFixture]
    public class TestTransformTweeningExtensions
    {
        static IEnumerable<object[]> CalculateAnticipationBezierControlPointsSource()
        {
            yield return new object[]
            {
                Vector3.zero,
                new Vector3(2, 0),
                Vector3.down,
                2f,
                60f/180f,
                2f,
                60f/180f,
                (
                    new Vector3(1, -TrigonometryExtensions.GetEquilateralTriangleHeight(2)),
                    new Vector3(1, -TrigonometryExtensions.GetEquilateralTriangleHeight(2))
                )
            };

            yield return new object[]
            {
                Vector3.zero,
                new Vector3(2, 0),
                Vector3.down,
                2f,
                -60f/180f,
                2f,
                -60/180f,
                (
                    new Vector3(1, TrigonometryExtensions.GetEquilateralTriangleHeight(2)),
                    new Vector3(1, TrigonometryExtensions.GetEquilateralTriangleHeight(2))
                )
            };

            yield return new object[]
            {
                Vector3.zero,
                Vector3.one,
                new Vector3(1, 0, -1),
                1f,
                0f,
                1f,
                0f,
                (
                    Vector3.one.normalized,
                    Vector3.one - Vector3.one.normalized
                )
            };
        }

        [Test]
        [TestCaseSource("CalculateAnticipationBezierControlPointsSource")]
        public void CalculateAnticipationBezierControlPoints(
            Vector3 from,
            Vector3 to,
            Vector3 rightVector,
            float intensityOrigin,
            float accentOrigin,
            float intensityDestiny,
            float accentDestiny,
            (Vector3, Vector3) controlPoints
            )
        {
            var result = TransformTweenExtensions.CalculateAnticipationBezierControlPoints(
                from,
                to,
                rightVector,
                intensityOrigin,
                accentOrigin,
                intensityDestiny,
                accentDestiny
            );

            Assert.That(result, Is.EqualTo(controlPoints));
        }
    }
}
