using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PathFindingProject.Search.Framework;

namespace PathFindingProject.Search.Domain {
	public class GoalTest : IGoalTest {
		private readonly Point m_rendevous;

		public GoalTest( Point goal ) {
			m_rendevous = goal;
		}

		public bool IsGoalState( string state ) {
			int xCoord;
			if( !int.TryParse( state.Split( ',' )[0], out xCoord ) ) {
				return false;
			}

			int yCoord;
			if( !int.TryParse( state.Split( ',' )[1], out yCoord ) ) {
				return false;
			}

			var statePoint = new Point( xCoord, yCoord );
			return m_rendevous.Equals( statePoint );
		}

	}
}
