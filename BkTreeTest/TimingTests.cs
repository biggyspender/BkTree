using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using BkTree;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BkTreeTest
{
    [TestClass]
    public class TimingTests
    {
        [TestMethod]
        public void TestFlipBits()
        {
            var v1 = GetRandom();
            const int numBits = 37;
            var v2 = FlipBits(v1, numBits);
            Assert.AreEqual(numBits,DistanceMetrics.Hamming.CalculateDistance(v1,v2));
        }
        [TestMethod]
        public void LoadAFew()
        {
            int count = 1000000;
            var nodes = Enumerable
                .Range(0, count)
                .AsParallel()
                .Select(_ => GetRandom())
                .Select((n, i) => new HashBkNode(n, i.ToString()))
                .ToList();

            var stopwatch = Stopwatch.StartNew();

            var tree = nodes.ToBkTree(DistanceMetrics.Hamming);

            Trace.TraceInformation("Took {0:##.###}ms to insert {1} hashes",stopwatch.Elapsed.TotalMilliseconds,count);

            const int numToSearch = 1000;
            var randomSelectionOfNodes = nodes.OrderBy(_ => ThreadSafeRandom.Rng.Next()).Take(numToSearch).ToList();

            var corruptedNodes =
                randomSelectionOfNodes.Select(
                    n =>
                    {
                        var numBits = Convert.ToInt32(Math.Truncate(Math.Pow(ThreadSafeRandom.Rng.NextDouble(), 20d)*12d));
                        var corruptedValue = FlipBits(n.Value, numBits);
                        return new HashBkNode(
                            corruptedValue,
                            n.Description);
                    });
            stopwatch = Stopwatch.StartNew();

            var matches =
                corruptedNodes.AsParallel().Select(node => new {searchNode = node, foundNode = tree.FindClosest(node.Value, 15, true)})
                    .ToList();
            Trace.TraceInformation("Took {0:##.###}ms to search {1} hashes", stopwatch.Elapsed.TotalMilliseconds, numToSearch);
            var badMatchCount =
                matches.Count(
                    match => match.foundNode == null || match.foundNode.Description != match.searchNode.Description);
            Trace.TraceInformation("bad match count : {0}",badMatchCount);
            Assert.IsFalse(matches.Any(match => match.foundNode.Description != match.searchNode.Description));


        }

        private readonly ThreadLocal<byte[]> buffers=new ThreadLocal<byte[]>(()=>new byte[8]);

        private ulong GetRandom()
        {
            var buf = buffers.Value;
            ThreadSafeRandom.Rng.NextBytes(buf);
            return BitConverter.ToUInt64(buf, 0);

        }

        private ulong FlipBits(ulong val,int numBits)
        {
            return Enumerable.Range(0, 64)
                .OrderBy(_ => ThreadSafeRandom.Rng.Next())
                .Take(numBits)
                .Aggregate(val, (acc, bitNum) => acc ^ (1UL << bitNum));
        }
    }
}