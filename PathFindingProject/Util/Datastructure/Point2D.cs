using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFindingProject.Util.Datastructure
{
    public class Point2D
    {
        private double x;
        private double y;

        public Point2D(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        /**
         * Returns the X coordinate of this <code>Point2D</code> in
         * <code>double</code> precision.
         * 
         * @return the X coordinate of this <code>Point2D</code>.
         */
        public double getX()
        {
            return x;
        }

        /**
         * Returns the Y coordinate of this <code>Point2D</code> in
         * <code>double</code> precision.
         * 
         * @return the Y coordinate of this <code>Point2D</code>.
         */
        public double getY()
        {
            return y;
        }

        /**
         * Returns the Euclidean distance between a specified point and this point.
         * 
         * @return the Euclidean distance between a specified point and this point.
         */
        public double distance(Point2D pt)
        {
            double result = (pt.getX() - x) * (pt.getX() - x);
            result += (pt.getY() - y) * (pt.getY() - y);
            return Math.Sqrt(result);
        }
    }
}
