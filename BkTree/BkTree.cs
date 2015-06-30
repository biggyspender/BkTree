using System;
using System.Collections.Generic;
using System.Linq;

namespace BkTree
{
    public class BkTree<TNode, TVal> where TNode : class, IBkNode<TVal>
    {
        private readonly IDictionary<int, BkTree<TNode, TVal>> children;
        private readonly IDistanceMetric<TVal> distanceMetric;
        private TNode node;

        public BkTree(IDistanceMetric<TVal> distanceMetric)
        {
            this.distanceMetric = distanceMetric;
            children = new SortedList<int, BkTree<TNode, TVal>>();
        }

        public void AddTerm(TNode node)
        {
            if (this.node == null)
            {
                this.node = node;
                return;
            }
            var dist = distanceMetric.CalculateDistance(this.node.Value, node.Value);
            
            if (children.ContainsKey(dist))
            {
                children[dist].AddTerm(node);
            }
            else
            {
                var newTree = new BkTree<TNode, TVal>(distanceMetric);
                children[dist] = newTree;
                newTree.AddTerm(node);
            }
        }

        public TNode FindClosest(TVal query, int maxDist, bool progressive = false)
        {
            if (!progressive)
            {
                return Query(query, maxDist, 1).FirstOrDefault();
            }
            return
                RangeGenerators.RangeMinMaxInclusive(0, maxDist)
                    .SelectMany(dist => Query(query, dist, 1))
                    .FirstOrDefault();
        }

        public IList<TNode> Query(TVal query, int maxDist, int k = -1)
        {
            var tempResults = new List<Tuple<TNode, int>>();
            Query(query, maxDist, -1, tempResults);
            var comparer = Comparer<Tuple<TNode, int>>.Create((a, b) => a.Item2.CompareTo(b.Item2));
            tempResults.Sort(comparer);
            var results = tempResults.Select(r => r.Item1);
            var len = (k >= 0) ? Math.Min(k, tempResults.Count) : tempResults.Count;
            return results.Take(len).ToList();
        }

        private void Query(TVal query, int maxDist, int d, IList<Tuple<TNode, int>> results)
        {
            var dist = distanceMetric.CalculateDistance(node.Value, query);
            if (dist <= maxDist)
            {
                results.Add(Tuple.Create(node, dist));
            }
            d = d < 0 ? dist : d;
            var min = (dist - maxDist);
            var max = (dist + maxDist);


            ////CODE HOTSPOT
            ////replaced with faster code below
            //var childrenToQuery =
            //    RangeGenerators.RangeMinMaxInclusive(min, max)
            //        .Where(i => children.ContainsKey(i))
            //        .Select(i => children[i]);
            //foreach (var v in childrenToQuery)
            //{
            //    v.Query(query, maxDist, d, results);
            //}


            //PERFORMANCE ENHANCEMENT
            //children is a sorted list, so we can just iterate, skipping everything that is out of range
            //this is much faster than testing for keys in an unsorted dictionary, especially
            //if we avoid the overhead of LINQ. as with the previous (commented) code above.
            //by using the right container (SortedList) instead of Dictionary, we get a search speed boost
            //of ~500%
            foreach (var kvp in children)
            {
                //don't convert this to LINQ, it's too slow right here
                if (kvp.Key < min) continue;
                if (kvp.Key > max) break;
                kvp.Value.Query(query, maxDist, d, results);
            }


        }
    }
}