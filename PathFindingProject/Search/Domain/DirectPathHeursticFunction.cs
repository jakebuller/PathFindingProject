using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PathFindingProject.Search.Framework;

namespace PathFindingProject.Search.Domain {
	public class DirectPathHeursticFunction : IHeuristicFunction {

		private readonly Point m_rendevousPoint;

		public DirectPathHeursticFunction( Point rendevousPoint ) {
			m_rendevousPoint = rendevousPoint;
		}

		public double Calculate( object state ) {
			var map = (int[,])state;
			double sum = 0;
			for ( int x = 0; x < map.Length - 1; x++ ) {
				for ( int y = 0; y < map.Rank - 1; y++ ) {
					if ( map[x, y] == 2 ) {
						sum += GetDistanceToRendevous( x, y );
					}
				}
			}

			return sum;
		}

		private double GetDistanceToRendevous( int x, int y ) {
			int xDiff = Math.Abs( m_rendevousPoint.XCoord - x );
			int yDiff = Math.Abs( m_rendevousPoint.YCoord - y );

			return xDiff + yDiff;
		}
	}
}
