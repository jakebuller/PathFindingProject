using System;

namespace PathFindingProject.Search.Domain {
	public class Point {
		
		public int XCoord { get; private set; }
		public int YCoord { get; private set; }
		
		public Point( int x, int y ) {
			XCoord = x;
			YCoord = y;
		}

		public double DistanceTo( Point other ) {
			double result = 
				Math.Pow( other.XCoord - XCoord, 2 ) + 
				Math.Pow(other.YCoord - YCoord, 2 );

            return Math.Sqrt(result);
		}
	}
}