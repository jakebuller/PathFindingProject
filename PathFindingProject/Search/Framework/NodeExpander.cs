using System.Collections.Generic;

using PathFindingProject.Search.Framework;

namespace PathFindingProject.Search.Framework {

	public class NodeExpander {

		private const string NodesExpandedMetric = "nodesExpanded";

		public NodeExpander() {			

		}			

		public IEnumerable<Node> ExpandNode( Node node, Problem problem ) {
			var childNodes = new List<Node>();
			var actionsFunction = problem.ActionsFunction;
			var resultFunction = problem.ResultFunction;
			var stepCostFunction = problem.StepCostFunction;

            foreach( var action in actionsFunction.GetActions( node.State ) ) {
                string successorState = resultFunction.Result(
                    node.State,
                    action
                );

                double stepCost = stepCostFunction.Cost(
                    node.State,
                    action,
                    successorState
                );

                childNodes.Add(
                    new Node( successorState, node, action, stepCost )
                );
            }
			
			return childNodes;
		}
	}
}
