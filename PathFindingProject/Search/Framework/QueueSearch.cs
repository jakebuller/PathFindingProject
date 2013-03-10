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

		public Node PeekAtFrontier() {
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

		public void SetQueueSize( int size ) {
			Metrics.Set( QueueSizeMetric, size );
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
				if ( SearchUtils.IsGoalState( problem, root ) ) {
					return SearchUtils.ActionsFromNodes( root.GetPathFromRoot() );
				}
			}
			frontier.Add( root );
			SetQueueSize( frontier.Count );
			while( 0 != frontier.Count ) {//&& !CancelableThread.currIsCanceled() ) {
				Node nodeToExpand = PeekAtFrontier();
				SetQueueSize( frontier.Count );
				if( !CheckGoalBeforeAddingToFrontier ) {

					if( SearchUtils.IsGoalState( problem, nodeToExpand ) ) {
						SetPathCost( nodeToExpand.PathCost );
						
						return SearchUtils.ActionsFromNodes(
							nodeToExpand.GetPathFromRoot() 
						);
					}
				}

				foreach( Node fn in GetResultingNodesToAddToFrontier(
						nodeToExpand,
						problem 
					)
				) {
					if( CheckGoalBeforeAddingToFrontier ) {
						if( SearchUtils.IsGoalState( problem, fn ) ) {
							SetPathCost( fn.PathCost );
							return SearchUtils.ActionsFromNodes(
								fn.GetPathFromRoot()
							);
						}
					}
					frontier.Add( fn );
				}
				SetQueueSize( frontier.Count );
			}

			return new List<IAction>();
		}
	}
}
