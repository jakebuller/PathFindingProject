using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PathFindingProject.Agent;
using PathFindingProject.Search.Framework;

namespace PathFindingProject.Search.Domain {
	public class StringStateResultFunction : IResultFunction {
		public string Result( string state, IAction action ) {
			var moveToAction = action as MoveToAction;
			if( moveToAction == null ) {
				return state;
			}

			return moveToAction.TargetLocation;
		}
	}
}
