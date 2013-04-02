using System;

using PathFindingProject.Search.Framework;

namespace PathFindingProject.Search.Domain {
	public class DirectPathHeuristicFunction : IHeuristicFunction {

		private readonly Point m_rendevousPoint;

		public DirectPathHeuristicFunction( Point rendevousPoint ) {
			m_rendevousPoint = rendevousPoint;
		}

		public int Calculate( string state ) {
			var x = int.Parse( state.Split( ',' )[0] );
			var y = int.Parse( state.Split( ',' )[1] );
			return GetDistanceToRendevous( x, y );
		}

		private int GetDistanceToRendevous( int x, int y ) {
			int xDiff = Math.Abs( m_rendevousPoint.XCoord - x );
			int yDiff = Math.Abs( m_rendevousPoint.YCoord - y );

			return xDiff + yDiff;
		}
	}
}
