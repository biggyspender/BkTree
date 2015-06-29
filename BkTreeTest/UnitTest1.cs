﻿using System.Collections.Generic;
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


        //https://gist.github.com/mikedugan/8233069
        [TestMethod]
        public void EqualStringsNoEdits()
        {
            Assert.AreEqual(0, DistanceMetrics.DamerauLevenshtein.CalculateDistance("test", "test"));
        }

        [TestMethod]
        public void Additions()
        {
            Assert.AreEqual(1, DistanceMetrics.DamerauLevenshtein.CalculateDistance("test", "tests"));
            Assert.AreEqual(1, DistanceMetrics.DamerauLevenshtein.CalculateDistance("test", "stest"));
            Assert.AreEqual(2, DistanceMetrics.DamerauLevenshtein.CalculateDistance("test", "mytest"));
            Assert.AreEqual(7, DistanceMetrics.DamerauLevenshtein.CalculateDistance("test", "mycrazytest"));
        }

        [TestMethod]
        public void AdditionsPrependAndAppend()
        {
            Assert.AreEqual(9, DistanceMetrics.DamerauLevenshtein.CalculateDistance("test", "mytestiscrazy"));
        }

        [TestMethod]
        public void AdditionOfRepeatedCharacters()
        {
            Assert.AreEqual(1, DistanceMetrics.DamerauLevenshtein.CalculateDistance("test", "teest"));
        }

        [TestMethod]
        public void Deletion()
        {
            Assert.AreEqual(1, DistanceMetrics.DamerauLevenshtein.CalculateDistance("test", "tst"));
        }

        [TestMethod]
        public void Transposition()
        {
            Assert.AreEqual(1, DistanceMetrics.DamerauLevenshtein.CalculateDistance("test", "tset"));
        }

        [TestMethod]
        public void AdditionWithTransposition()
        {
            Assert.AreEqual(2, DistanceMetrics.DamerauLevenshtein.CalculateDistance("test", "tsets"));
        }

        [TestMethod]
        public void TranspositionOfRepeatedCharacters()
        {
            Assert.AreEqual(1, DistanceMetrics.DamerauLevenshtein.CalculateDistance("banana", "banaan"));
            Assert.AreEqual(1, DistanceMetrics.DamerauLevenshtein.CalculateDistance("banana", "abnana"));
            Assert.AreEqual(2, DistanceMetrics.DamerauLevenshtein.CalculateDistance("banana", "baanaa"));
        }

        [TestMethod]
        public void EmptyStringsNoEdits()
        {
            Assert.AreEqual(0, DistanceMetrics.DamerauLevenshtein.CalculateDistance("", ""));
        }


    }
}