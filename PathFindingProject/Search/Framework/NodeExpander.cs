using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFindingProject.Search.Framework {

	public class NodeExpander {

		private const string NodesExpandedMetric = "nodesExpanded";
		private readonly Metrics m_metrics;

		public NodeExpander() {
			m_metrics = new Metrics();
			ClearInstrumentation();
		}

		public Metrics Metrics {
			get {
				return m_metrics;
			}
		}

		public virtual void ClearInstrumentation() {
			m_metrics.Set( NodesExpandedMetric, 0 );
		}

		public int GetNumNodesExpanded() {
			int numNodesExpanded = 0;
			m_metrics.TryGetInt( NodesExpandedMetric, out numNodesExpanded );

			return numNodesExpanded;
		}

		public IEnumerable<Node> ExpandNode( Node node, Problem problem ) {
			var childNodes = new List<Node>();
			var actionsFunction = problem.ActionsFunction;
			var resultFunction = problem.ResultFunction;
			var stepCostFunction = problem.StepCostFunction;

			foreach (var action in actionsFunction.GetActions( node.State ) ) {
				object successorState = resultFunction.Result( 
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

			int numNodesExpanded;
			m_metrics.TryGetInt( NodesExpandedMetric, out numNodesExpanded );
			m_metrics.Set( NodesExpandedMetric, numNodesExpanded + 1 );

			return childNodes;
		}
	}
}
