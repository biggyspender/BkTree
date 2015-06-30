# BkTree

A C# port (and improvement) of https://github.com/jonahharris/node-bktree/ 

Usage
===

The tree stores instances of `IBkNode<T>` where a `T` can be compared to another `T` and a distance derived. For this example, we could use points on a 2-d plane, defined as such:

    public class Point2D : IEquatable<Point2D>
    {
        public int X { get; private set; }
        public int Y { get; private set; }
  
        public Point2D(int x, int y)
        {
            X = x;
            Y = y;
        }
        
        //equality and hashing members... 
    }
    
And a means of measuring the distance between 2 `Point2D` instances:

    public class PythagoreanDistanceMetric : IDistanceMetric<Point2D>
    {
        public int CalculateDistance(Point2D v1, Point2D v2)
        {
            return Convert.ToInt32(
                Math.Sqrt(Math.Pow(v1.X - v2.X, 2) +
                          Math.Pow(v1.Y - v2.Y, 2)
                    ));
        }
    }


Now we could make a type of `IBkNode<Point>` to store in the tree:

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

Now, let's create a "cloud" of points and wrap them in `Point2DBkNode`s

      var points = new[]
      {
          new Point2D(-4, -4),
          new Point2D(-4, 4),
          new Point2D(4, 4),
          new Point2D(8, 8),
          new Point2D(0, 8)
      };
      var nodes = points.Select((p, i) => new Point2DBkNode(p, i.ToString()));

and create a BkTree from these nodes, using a distance metric that calculates the distance between two points:

      var bkTree = nodes.ToBkTree(DistanceMetrics.Pythagorean);

Now, lets find all nodes within 6 units of [5,4]:

      var closestNodes = bkTree.Query(new Point2D(5,4), 6);

and make sure all is good:

      Assert.IsTrue(
        new HashSet<Point2D>(closestNodes.Select(n => n.Value))
          .SetEquals(new[]{
              new Point2D(4, 4),
              new Point2D(8, 8),
              new Point2D(0, 8)
          }));

or lets just find the closest node within a threshold:

    Point2DBkNode closest = bkTree.FindClosest(new Point2D(5, 4), 6);
    Assert.AreEqual(new Point2D(4,4), closest.Value);
