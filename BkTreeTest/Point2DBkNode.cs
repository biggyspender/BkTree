using BkTree;

namespace BkTreeTest
{
    public class Point2DBkNode : IBkNode<Point2D>
    {
        private readonly Point2D value;
        private readonly string description;
        public Point2DBkNode(Point2D value, string description)
        {
            this.description = description;
            this.value = value;
        }
        public Point2D Value
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