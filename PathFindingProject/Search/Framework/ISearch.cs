using System.Collections.Generic;

using PathFindingProject.Agent;

namespace PathFindingProject.Search.Framework {
	public interface ISearch {
		IEnumerable<MoveToAction> Search( Problem problem );
	}
}
