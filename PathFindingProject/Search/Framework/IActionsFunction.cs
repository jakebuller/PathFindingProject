using System.Collections.Generic;

using PathFindingProject.Agent;

namespace PathFindingProject.Search.Framework {
	public interface IActionsFunction {
		HashSet<IAction> GetActions( object s );
	}
}
