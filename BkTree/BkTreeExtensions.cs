using System.Collections.Generic;

namespace BkTree
{
    public static class BkTreeExtensions
    {
        public static BkTree<TNode, TValue> ToBkTree<TNode, TValue>(this IEnumerable<TNode> nodes,
            IDistanceMetric<TValue> distanceMetric) where TNode : class, IBkNode<TValue>
        {
            var tree = new BkTree<TNode, TValue>(distanceMetric);
            foreach (var node in nodes)
            {
                tree.AddTerm(node);
            }
            return tree;
        }
    }
}