
using PathFindingProject.Agent;

namespace PathFindingProject.Search.Framework {
    public interface IStepCostFunction {
        int Cost( string state, MoveToAction action, string stateDelta );
    }
}
