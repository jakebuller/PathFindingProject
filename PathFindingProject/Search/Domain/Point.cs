

namespace PathFindingProject.Search.Domain {
	public class Point {
		
		public int XCoord { get; private set; }
		public int YCoord { get; private set; }
		
		public Point( int x, int y ) {
			XCoord = x;
			YCoord = y;
		}
	}
}