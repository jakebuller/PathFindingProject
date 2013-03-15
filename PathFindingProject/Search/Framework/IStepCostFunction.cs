
using PathFindingProject.Agent;

namespace PathFindingProject.Search.Framework {
	public interface IStepCostFunction {
		double Cost( object state, IAction action, object stateDelta );
	}
}
