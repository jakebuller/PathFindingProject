
using PathFindingProject.Agent;

namespace PathFindingProject.Search.Framework {
	public interface IStepCostFunction {
		double Cost( string state, IAction action, string stateDelta );
	}
}
