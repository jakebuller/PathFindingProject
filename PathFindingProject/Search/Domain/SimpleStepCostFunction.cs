
using PathFindingProject.Agent;
using PathFindingProject.Search.Framework;

namespace PathFindingProject.Search.Domain {
	public class SimpleStepCostFunction : IStepCostFunction {
		public int Cost( string state, MoveToAction action, string stateDelta ) {
			return 2;
		}
	}
}
