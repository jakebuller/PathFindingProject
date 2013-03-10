using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PathFindingProject.Agent;

namespace PathFindingProject.Search.Framework {
	public abstract class QueueSearch : NodeExpander{

		public static string QueueSizeMetric = "queueSize";
		public static string MaxQueueSizeMetric = "maxQueueSize";
		public static string PathCostMetric = "pathCost";

		private List<Node> m_frontier;

		public bool CheckGoalBeforeAddingToFrontier { get; set; }

		public abstract IEnumerable<Node> GetResultingNodesToAddToFrontier(
			Node nodeToExpand,
			Problem p
		);

		public bool IsFailure( List<IAction> result ) {
			return 0 == result.Count;
		}

		public Node PopFromFrontier() {
			return m_frontier.First();
		}

		public bool RemoveFromFrontier( Node node ) {
			return m_frontier.Remove( node );
		}

		public override void ClearInstrumentation() {
			base.ClearInstrumentation();
			Metrics.Set( QueueSizeMetric, 0 );
			Metrics.Set( MaxQueueSizeMetric, 0 );
			Metrics.Set( PathCostMetric, 0 );
		}

		public int GetQueueSize() {
			int size;
			Metrics.TryGetInt( QueueSizeMetric, out size );
			return size;
		}

		public int GetMaxQueueSize() {
			int maxSize;
			Metrics.TryGetInt( MaxQueueSizeMetric, out maxSize );
			return maxSize;
		}

		public double GetPathCost() {
			double cost;
			Metrics.TryGetDouble( PathCostMetric, out cost );
			return cost;
		}

		public void SetPathCost( double cost ) {
			Metrics.Set( PathCostMetric, cost );
		}

		public IEnumerable<IAction> Search( 
			Problem problem,
			List<Node> frontier
		) {
			m_frontier = frontier;

			ClearInstrumentation();
			Node root = new Node( problem.InitialState );
			if ( CheckGoalBeforeAddingToFrontier ) {
				if ( SearchUtils.isGoalState(problem, root ) ) {
					return SearchUtils.actionsFromNodes(root.getPathFromRoot());
				}
			}
			frontier.Add( root );
			SetQueueSize( frontier.Count );
			while (!(frontier.isEmpty()) && !CancelableThread.currIsCanceled()) {
				// choose a leaf node and remove it from the frontier
				Node nodeToExpand = popNodeFromFrontier();
				setQueueSize(frontier.size());
				// Only need to check the nodeToExpand if have not already
				// checked before adding to the frontier
				if (!isCheckGoalBeforeAddingToFrontier()) {
					// if the node contains a goal state then return the
					// corresponding solution
					if (SearchUtils.isGoalState(problem, nodeToExpand)) {
						setPathCost(nodeToExpand.getPathCost());
						return SearchUtils.actionsFromNodes(nodeToExpand
								.getPathFromRoot());
					}
				}
				// expand the chosen node, adding the resulting nodes to the
				// frontier
				for (Node fn : getResultingNodesToAddToFrontier(nodeToExpand,
						problem)) {
					if (isCheckGoalBeforeAddingToFrontier()) {
						if (SearchUtils.isGoalState(problem, fn)) {
							setPathCost(fn.getPathCost());
							return SearchUtils.actionsFromNodes(fn
									.getPathFromRoot());
						}
					}
					frontier.insert(fn);
				}
				setQueueSize(frontier.size());
			}
			// if the frontier is empty then return failure
			return failure();
		}
	}
}
