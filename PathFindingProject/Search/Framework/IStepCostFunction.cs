
using PathFindingProject.Agent;

namespace PathFindingProject.Search.Framework {
	public interface IStepCostFunction {
		int Cost( string state, IAction action, string stateDelta );
	}
}
