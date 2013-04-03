
using PathFindingProject.Agent;
using PathFindingProject.Search.Framework;

namespace PathFindingProject.Search.Domain {
	public class ResultFunction : IResultFunction {
		public string Result( string state, MoveToAction action ) {
			var moveToAction = action as MoveToAction;
			if( moveToAction == null ) {
				return state;
			}

			return moveToAction.TargetLocation;
		}
	}
}
