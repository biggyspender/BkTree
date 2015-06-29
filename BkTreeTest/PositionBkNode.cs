using BkTree;

namespace BkTreeTest
{
    public class PositionBkNode : IBkNode<Point>
    {
        private readonly Point value;
        private readonly string description;
        public PositionBkNode(Point value, string description)
        {
            this.description = description;
            this.value = value;
        }
        public Point Value
        {
            get
            {
                return value;
            }
        }
        public string Description
        {
            get
            {
                return description;
            }
        }
    }
}