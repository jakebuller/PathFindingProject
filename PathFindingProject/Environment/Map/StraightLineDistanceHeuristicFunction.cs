using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PathFindingProject.Search.Domain;

namespace PathFindingProject.Environment.Map {
	public class StraightLineDistanceHeuristicFunction : AdaptableHeuristicFunction {

		public StraightLineDistanceHeuristicFunction( object goal, IMap map ) {
			m_goal = goal;
			m_map = map;
		}

		public override double Calculate( string state ) {
			double result = 0.0;
			Point pt1 = m_map.GetPosition( ( string )state );
			Point pt2 = m_map.GetPosition( ( string )m_goal );
			if( pt1 != null && pt2 != null ) {
				result = pt1.DistanceTo( pt2 );
			}
			return result;
		}
	}
}

