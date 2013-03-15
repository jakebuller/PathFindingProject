using System.Collections.Generic;

using PathFindingProject.Agent;

namespace PathFindingProject.Search.Framework {
	public interface ISearch {
		IEnumerable<IAction> Search( Problem problem );
	}
}
