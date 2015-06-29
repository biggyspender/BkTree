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
                new Point(-4, -4),
                new Point(-4, 4),
                new Point(4, 4),
                new Point(8, 8),
                new Point(0, 8)
            };
            var nodes = points.Select((p, i) => new PositionBkNode(p, i.ToString()));

            var bkTree = nodes.ToBkTree(DistanceMetrics.Pythagorean);
            PositionBkNode closest = bkTree.FindClosest(new Point(-1, -1), 5);
            Assert.AreEqual(new Point(-4, -4), closest.Value);
        }
        [TestMethod]
        public void TestMethod2()
        {
            var points = new[]
            {
                new Point(-4, -4),
                new Point(-4, 4),
                new Point(4, 4),
                new Point(8, 8),
                new Point(0, 8)
            };
            var nodes = points.Select((p, i) => new PositionBkNode(p, i.ToString()));

            var bkTree = nodes.ToBkTree(DistanceMetrics.Pythagorean);
            PositionBkNode closest = bkTree.FindClosest(new Point(7, 8), 5);
            Assert.AreNotEqual(new Point(-4, -4), closest.Value);
        }
        [TestMethod]
        public void TestMethod3()
        {
            var points = new[]
            {
                new Point(-4, -4),
                new Point(-4, 4),
                new Point(4, 4),
                new Point(8, 8),
                new Point(0, 8)
            };
            var nodes = points.Select((p, i) => new PositionBkNode(p, i.ToString()));

            var bkTree = nodes.ToBkTree(DistanceMetrics.Pythagorean);
            PositionBkNode closest = bkTree.FindClosest(new Point(7, 8), 5);
            Assert.AreEqual(new Point(8,8), closest.Value);
        }
    }
}