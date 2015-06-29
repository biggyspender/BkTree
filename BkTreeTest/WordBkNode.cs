using BkTree;

namespace BkTreeTest
{
    public class WordBkNode : IBkNode<string>
    {
        public WordBkNode(string value)
        {
            Value = value;
        }

        public string Value { get; private set; }
    }
}