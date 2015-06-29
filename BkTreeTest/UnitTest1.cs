using System.Collections.Generic;
using System.Linq;
using BkTree;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BkTreeTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var points = new[]
            {
                new Point2D(-4, -4),
                new Point2D(-4, 4),
                new Point2D(4, 4),
                new Point2D(8, 8),
                new Point2D(0, 8)
            };
            var nodes = points.Select((p, i) => new Point2DBkNode(p, i.ToString()));

            var bkTree = nodes.ToBkTree(DistanceMetrics.Pythagorean);
            Point2DBkNode closest = bkTree.FindClosest(new Point2D(-1, -1), 5);
            Assert.AreEqual(new Point2D(-4, -4), closest.Value);
        }
        [TestMethod]
        public void TestMethod2()
        {
            var points = new[]
            {
                new Point2D(-4, -4),
                new Point2D(-4, 4),
                new Point2D(4, 4),
                new Point2D(8, 8),
                new Point2D(0, 8)
            };
            var nodes = points.Select((p, i) => new Point2DBkNode(p, i.ToString()));

            var bkTree = nodes.ToBkTree(DistanceMetrics.Pythagorean);
            Point2DBkNode closest = bkTree.FindClosest(new Point2D(7, 8), 5);
            Assert.AreNotEqual(new Point2D(-4, -4), closest.Value);
        }
        [TestMethod]
        public void TestMethod3()
        {
            var points = new[]
            {
                new Point2D(-4, -4),
                new Point2D(-4, 4),
                new Point2D(4, 4),
                new Point2D(8, 8),
                new Point2D(0, 8)
            };
            var nodes = points.Select((p, i) => new Point2DBkNode(p, i.ToString()));

            var bkTree = nodes.ToBkTree(DistanceMetrics.Pythagorean);
            Point2DBkNode closest = bkTree.FindClosest(new Point2D(5, 4), 6);
            Assert.AreEqual(new Point2D(4,4), closest.Value);
        }
        [TestMethod]
        public void TestMethod4()
        {
            var points = new[]
            {
                new Point2D(-4, -4),
                new Point2D(-4, 4),
                new Point2D(4, 4),
                new Point2D(8, 8),
                new Point2D(0, 8)
            };
            var nodes = points.Select((p, i) => new Point2DBkNode(p, i.ToString()));

            var bkTree = nodes.ToBkTree(DistanceMetrics.Pythagorean);
            var closestNodes = bkTree.Query(new Point2D(5,4), 6);

            Assert.IsTrue(new HashSet<Point2D>(closestNodes.Select(n => n.Value)).SetEquals(new[]
            {
                new Point2D(4, 4),
                new Point2D(8, 8),
                new Point2D(0, 8)
            }));

        }

    }
}