using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PathFindingProject.Search.Framework;

namespace PathFindingProject.Search.Domain {
	public class StringStateGoalTest : IGoalTest {
		private readonly Point m_rendevous;

		public StringStateGoalTest( Point goal ) {
			m_rendevous = goal;
		}

		public bool IsGoalState( object state ) {
			// state should always be a string of the form x,y
			var strState = ( string )state;
			int xCoord;
			if( !int.TryParse( strState.Split( ',' )[0], out xCoord ) ) {
				return false;
			}

			int yCoord;
			if( !int.TryParse( strState.Split( ',' )[1], out yCoord ) ) {
				return false;
			}

			var statePoint = new Point( xCoord, yCoord );
			return m_rendevous.Equals( statePoint );
		}

	}
}
