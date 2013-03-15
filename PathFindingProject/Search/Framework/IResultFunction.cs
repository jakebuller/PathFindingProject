
using PathFindingProject.Agent;

namespace PathFindingProject.Search.Framework {
	public interface IResultFunction {
		object Result( object state, IAction action );
	}
}
