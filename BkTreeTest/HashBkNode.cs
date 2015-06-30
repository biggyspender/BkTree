using BkTree;

namespace BkTreeTest
{
    public class HashBkNode:IBkNode<ulong>
    {
        public HashBkNode(ulong value, string description)
        {
            Value = value;
            Description = description;
        }

        public ulong Value { get; private set; }
        public string Description { get; private set; }
    }
}