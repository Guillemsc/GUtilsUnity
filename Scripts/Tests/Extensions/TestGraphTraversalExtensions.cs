using System;
using System.Collections.Generic;
using GUtils.Extensions;
using NUnit.Framework;

namespace GUtilsUnity.Extensions.Test
{
    public class TestGraphTraversalExtensions
    {
        [Test]
        public void GetNodesWithinDistance_BiggerThanMax_GetsAllElements()
        {
            var siblings = new Dictionary<int, int[]>()
            {
                { 0, new[] { 1, 2 } },
                { 1, new[] { 3 } },
                { 2, new[] { 4 } },
                { 3, new[] { 4 } },
                { 4, Array.Empty<int>() }
            };

            var result = GraphTraversalExtensions.GetNodesWithinDistance<int>(
                0,
                4,
                x => siblings[x],
                false);

            Assert.That(result, Is.EquivalentTo(new []
            {
                new GraphTraversalExtensions.DepthDistanceElement<int>(1, 1),
                new GraphTraversalExtensions.DepthDistanceElement<int>(2, 1),
                new GraphTraversalExtensions.DepthDistanceElement<int>(3, 2),
                new GraphTraversalExtensions.DepthDistanceElement<int>(4, 2)
            }));
        }

        [Test]
        public void GetNodesWithinDistance_LowerThanMax_GetsAllElements()
        {
            var siblings = new Dictionary<int, int[]>()
            {
                { 0, new[] { 1, 2 } },
                { 1, new[] { 3 } },
                { 2, new[] { 4 } },
                { 3, new[] { 4 } },
                { 4, Array.Empty<int>() }
            };

            var result = GraphTraversalExtensions.GetNodesWithinDistance<int>(
                0,
                1,
                x => siblings[x],
                false);

            Assert.That(result, Is.EquivalentTo(new []
            {
                new GraphTraversalExtensions.DepthDistanceElement<int>(1, 1),
                new GraphTraversalExtensions.DepthDistanceElement<int>(2, 1)
            }));
        }
    }
}
